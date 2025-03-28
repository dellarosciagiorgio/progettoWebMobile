using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class AddFeedbackRequest : BaseRequest
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public int IdFeedback { get; set; }
        public int Rating { get; set; }
        public int IdSagra { get; set; }

    }
}