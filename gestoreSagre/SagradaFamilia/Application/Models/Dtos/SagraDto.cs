using Models.Entities;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos
{
    public class SagraDto
    {
        public int IdSagra { get; set; }
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public List<int> IdFeedbacks { get; set; } = new List<int>();
        public List<int> IdEventi { get; set; } = new List<int>();
        public int IdOrganizzatore { get; set; }
    }
}