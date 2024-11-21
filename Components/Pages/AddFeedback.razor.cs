using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyFeedbacks.Application.Feedback.Commands;
using Radzen;

namespace MyFeedbacks.Components.Pages
{
    public partial class AddFeedback
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            feedback = new MyFeedbacks.Models.FeedbackDb.Feedback();
        }
        protected bool errorVisible;
        protected MyFeedbacks.Models.FeedbackDb.Feedback feedback;

        protected async Task FormSubmit()
        {
            try
            {
                await _mediator.Send(new AddCommand { Feedback = feedback });
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
    }
}