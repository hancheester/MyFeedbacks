using MyFeedbacks.Models.FeedbackDb;
using Radzen;

namespace MyFeedbacks.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<Feedback> AddAsync(T entity);
        Task<Feedback> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Query query = null);
        Task<Feedback> GetByIdAsync(int id);
        Task<Feedback> UpdateAsync(int id, T entity);
        void Reset();
    }
}