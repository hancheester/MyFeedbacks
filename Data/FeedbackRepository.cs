using Microsoft.EntityFrameworkCore;
using MyFeedbacks.Models.FeedbackDb;
using Radzen;
using System.Linq.Dynamic.Core;

namespace MyFeedbacks.Data
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        private readonly FeedbackDbContext _dbContext;

        public FeedbackRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync(Query query = null)
        {
            var items = _dbContext.Feedbacks.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            return await Task.FromResult(items);
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _dbContext.Feedbacks.AsNoTracking().Where(i => i.FeedbackId == id).FirstOrDefaultAsync();
        }

        public async Task<Feedback> AddAsync(Feedback feedback)
        {
            var existingItem = _dbContext.Feedbacks
                .Where(i => i.FeedbackId == feedback.FeedbackId)
                .FirstOrDefault();

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                _dbContext.Feedbacks.Add(feedback);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                _dbContext.Entry(feedback).State = EntityState.Detached;
                throw;
            }

            return feedback;
        }

        public async Task<Feedback> UpdateAsync(int id, Feedback feedback)
        {
            var existingItem = await _dbContext.Feedbacks
                .Where(i => i.FeedbackId == id)
                .FirstOrDefaultAsync();

            if (existingItem == null)
            {
                throw new Exception("Item no longer available");
            }

            try
            {
                _dbContext.Entry(existingItem).CurrentValues.SetValues(feedback);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                _dbContext.Entry(feedback).State = EntityState.Detached;
                throw;
            }

            return feedback;
        }

        public async Task<Feedback> DeleteAsync(int id)
        {
            var existingItem = await _dbContext.Feedbacks
                .Where(i => i.FeedbackId == id)
                .FirstOrDefaultAsync();

            if (existingItem == null)
            {
                throw new Exception("Item no longer available");
            }

            try
            {
                _dbContext.Feedbacks.Remove(existingItem);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                _dbContext.Entry(existingItem).State = EntityState.Unchanged;
                throw;
            }

            return existingItem;
        }

        public void Reset()
        {
            _dbContext.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
        }

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }
    }
}
