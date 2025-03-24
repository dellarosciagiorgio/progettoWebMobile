namespace Application.Models.Requests
{
    public class EditFeedbackRequest
    {
        public int IdFeedback { get; set; }
        public string? Titolo { get; set; }
        public string? Descrizione { get; set; }
    }
}