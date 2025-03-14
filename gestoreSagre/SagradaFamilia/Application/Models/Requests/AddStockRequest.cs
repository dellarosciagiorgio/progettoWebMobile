namespace Application.Models.Request
{
    public class AddStockRequest
    {
        public double Quantita { get; set; }
        public int IdEvento { get; set; }
        public string Tipo { get; set; }
        public double Prezzo { get; set; }
        public string Descrizione { get; set; }

    }
}