namespace Application.Models.Request
{
    public class AddStockRequest
    {
        public double Quantita { get; set; }
        public int IdEvento { get; set; }
        public int IdTipo { get; set; }
        public double Prezzo { get; set; }

    }
}