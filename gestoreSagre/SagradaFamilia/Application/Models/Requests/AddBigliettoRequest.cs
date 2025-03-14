namespace Application.Models.Request
{
    public class AddBigliettoRequest
    {
        public int IdTipoBiglietto { get; set; }
        public string Nominativo { get; set; }
        public int IdAcquirente { get; set; }
    }
}