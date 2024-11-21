using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyFeedbacks.Application.Feedback.Commands;
using MyFeedbacks.Application.Feedback.Queries;
using Radzen;
using Radzen.Blazor;

namespace MyFeedbacks.Components.Pages
{
    public partial class Feedbacks
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected IEnumerable<MyFeedbacks.Models.FeedbackDb.Feedback> feedbacks;

        protected RadzenDataGrid<MyFeedbacks.Models.FeedbackDb.Feedback> grid;
        
        protected override async Task OnInitializedAsync()
        {
            feedbacks = await _mediator.Send(new GetAllQuery());
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddFeedback>("Add Feedback", null);
            await grid.Reload();
        }

        protected async Task EditRow(MyFeedbacks.Models.FeedbackDb.Feedback args)
        {
            await DialogService.OpenAsync<EditFeedback>("Edit Feedback", new Dictionary<string, object> { {"FeedbackId", args.FeedbackId} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, MyFeedbacks.Models.FeedbackDb.Feedback feedback)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await _mediator.Send(new DeleteCommand { Id = feedback.FeedbackId });

                    if (deleteResult != null)
                    {
                        await grid.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete Feedback"
                });
            }
        }
    }
}