using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class AddFeedbackBySagraRequest : BaseAddFeedbackrequests
    {
         public int IdSagra { get; set; }
    }
}