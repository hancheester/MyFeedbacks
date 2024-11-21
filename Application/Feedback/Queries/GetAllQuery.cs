using MediatR;
using MyFeedbacks.Data;
using Radzen;

namespace MyFeedbacks.Application.Feedback.Queries
{
    public class GetAllQuery : IRequest<IEnumerable<Models.FeedbackDb.Feedback>>
    {
        public Query Query { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Models.FeedbackDb.Feedback>>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public GetAllQueryHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.FeedbackDb.Feedback>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request.Query);
        }
    }
}
