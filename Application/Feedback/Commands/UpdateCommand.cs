using MediatR;
using MyFeedbacks.Data;

namespace MyFeedbacks.Application.Feedback.Commands
{
    public class UpdateCommand : IRequest<Models.FeedbackDb.Feedback>
    {
        public int Id { get; set; }
        public Models.FeedbackDb.Feedback Feedback { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Models.FeedbackDb.Feedback>
    {
        private readonly IRepository<Models.FeedbackDb.Feedback> _repository;

        public UpdateCommandHandler(IRepository<Models.FeedbackDb.Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<Models.FeedbackDb.Feedback> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.Id, request.Feedback);
        }
    }
}
