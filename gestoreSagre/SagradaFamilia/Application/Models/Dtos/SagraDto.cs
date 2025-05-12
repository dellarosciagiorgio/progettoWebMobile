using Models.Entities;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos
{
    public class SagraDto
    {
        public int IdSagra { get; set; }
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public string Luogo { get; set; }
        public List<FeedbackDto>? Feedbacks { get; set; } = new List<FeedbackDto>();
        public List<EventoDto>? Eventi { get; set; } = new List<EventoDto>();
        public int IdOrganizzatore { get; set; }
    }
}