using MediatR;
using MyFeedbacks.Data;

namespace MyFeedbacks.Application.Feedback.Queries
{
    public class GetByIdQuery : IRequest<Models.FeedbackDb.Feedback>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Models.FeedbackDb.Feedback>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public GetByIdQueryHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<Models.FeedbackDb.Feedback> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
