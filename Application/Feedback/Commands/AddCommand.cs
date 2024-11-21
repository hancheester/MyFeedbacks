using MediatR;
using MyFeedbacks.Data;

namespace MyFeedbacks.Application.Feedback.Commands
{
    public class AddCommand : IRequest<Models.FeedbackDb.Feedback>
    {
        public Models.FeedbackDb.Feedback Feedback { get; set; }
    }

    public class AddCommandHandler : IRequestHandler<AddCommand, Models.FeedbackDb.Feedback>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public AddCommandHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<Models.FeedbackDb.Feedback> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Feedback);
        }
    }
}
