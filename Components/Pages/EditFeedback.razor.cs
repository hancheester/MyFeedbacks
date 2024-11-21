using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyFeedbacks.Application.Feedback.Commands;
using MyFeedbacks.Application.Feedback.Queries;
using Radzen;

namespace MyFeedbacks.Components.Pages
{
    public partial class EditFeedback
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Parameter]
        public int FeedbackId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            feedback = await _mediator.Send(new GetByIdQuery { Id = FeedbackId });
        }

        protected bool errorVisible;
        protected MyFeedbacks.Models.FeedbackDb.Feedback feedback;

        protected async Task FormSubmit()
        {
            try
            {
                await _mediator.Send(new UpdateCommand { Id = FeedbackId, Feedback = feedback });
                DialogService.Close(feedback);
            }
            catch (Exception ex)
            {
                hasChanges = ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
                canEdit = !(ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException);
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;
      
        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            await _mediator.Send(new ResetCommand());
            hasChanges = false;
            canEdit = true;

            feedback = await _mediator.Send(new GetByIdQuery { Id = FeedbackId });
        }
    }
}