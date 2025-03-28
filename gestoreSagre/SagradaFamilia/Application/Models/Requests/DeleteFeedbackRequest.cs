using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class DeleteFeedbackRequest : BaseRequest
    {
        public int IdFeedback { get; set; }
    }
}