using MediatR;
using MyFeedbacks.Data;

namespace MyFeedbacks.Application.Feedback.Commands
{
    public class ResetCommand : IRequest
    {
    }

    public class ResetCommandHandler : IRequestHandler<ResetCommand>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public ResetCommandHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public Task Handle(ResetCommand request, CancellationToken cancellationToken)
        {
            _repository.Reset();
            return Task.CompletedTask;
        }
    }
}
