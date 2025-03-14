using Models.Entities;

namespace Application.Models.Dtos
{
    public class SagraDto
    {
        public int IdSagra { get; set; }
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public List<int> IdFeedbacks { get; set; }
        public List<int> IdEventi { get; set; }
        public int IdOrganizzatore { get; set; }
    }
}