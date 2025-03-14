namespace Application.Models.Request
{
    public class EditAcquirenteRequest
    {
        public int IdAcquirente { get; set; }
        public string Mail { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}