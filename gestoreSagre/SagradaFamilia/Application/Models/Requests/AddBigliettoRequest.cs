namespace Application.Models.Request
{
    public class AddBigliettoRequest
    {
        public int IdTipoBiglietto { get; set; }
        public List<string> Nominativo { get; set; } = new List<string>();
        public int IdAcquirente { get; set; }
    }
}