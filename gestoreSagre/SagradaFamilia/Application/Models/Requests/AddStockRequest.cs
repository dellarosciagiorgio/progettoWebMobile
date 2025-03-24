namespace Application.Models.Request
{
    public class AddStockRequest
    {
        public int Quantita { get; set; }
        public int IdEventoEsistente { get; set; }
        public string? Tipo { get; set; }
        public double Prezzo { get; set; }
        public string? Descrizione { get; set; }
    }
}