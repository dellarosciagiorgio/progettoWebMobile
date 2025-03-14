namespace Application.Models.Request
{
    public class EditEventoRequest
    {
        public int IdEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
    }
}