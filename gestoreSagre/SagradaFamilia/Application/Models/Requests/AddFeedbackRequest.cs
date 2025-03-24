namespace Application.Models.Requests
{
    public class AddFeedbackRequest
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public int IdAcquirente { get; set; }
        public int IdFeedback { get; set; }
        public int Rating { get; set; }
        public int IdSagra { get; set; }

    }
}