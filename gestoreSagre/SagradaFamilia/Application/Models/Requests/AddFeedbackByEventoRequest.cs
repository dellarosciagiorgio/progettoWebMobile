using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class AddFeedbackByEventoRequest : BaseAddFeedbackrequests
    {
        public int IdEvento { get; set; }

    }
}