using Models.Entities;

namespace Application.Models.Dtos
{
    public class StockDto
    {
        public int IdStock { get; set; }
        public int IdTipoBiglietto { get; set; }
        public double Prezzo { get; set; }
        public string Descrizione { get; set; }
        public string Tipo { get; set; }
        public double Quantita { get; set; }
        public int IdEvento { get; set; }
    }
}