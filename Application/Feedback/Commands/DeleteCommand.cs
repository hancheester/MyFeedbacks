using MediatR;
using MyFeedbacks.Data;

namespace MyFeedbacks.Application.Feedback.Commands
{
    public class DeleteCommand : IRequest<Models.FeedbackDb.Feedback>
    {
        public int Id { get; set; }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Models.FeedbackDb.Feedback>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public DeleteCommandHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<Models.FeedbackDb.Feedback> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
