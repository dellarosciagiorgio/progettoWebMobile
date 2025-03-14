using Models.Entities;

namespace Application.Models.Dtos
{
    public class AcquirenteDto
    {
        public int IdAcquirente { get; set; }
        public string Mail { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public List<int> IdBigliettiComprati { get; set; }
        public List<int> IdFeedBacks { get; set; }
    }
}