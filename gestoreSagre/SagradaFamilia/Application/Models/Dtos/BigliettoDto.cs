using Models.Entities;

namespace Application.Models.Dtos
{
    public class BigliettoDto
    {
        public int IdBiglietto { get; set; }
        public int IdTipoBiglietto { get; set; }
        public string Nominativo { get; set; }
        public int IdAcquirente { get; set; }
        public double Prezzo { get; set; }
        public int IdEvento { get; set; }
    }
}