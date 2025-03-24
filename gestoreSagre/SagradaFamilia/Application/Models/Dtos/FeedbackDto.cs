using Models.Entities;

namespace Application.Models.Dtos
{
    public class FeedbackDto
    {
        public int IdFeedback { get; set; }
        public int IdAcquirente { get; set; } 
        public string? Titolo { get; set; }
        public string? Descrizione { get; set; }
        public int Rating { get; set; }
    }
}