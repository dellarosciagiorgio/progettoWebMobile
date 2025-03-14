namespace Application.Models.Request
{
    public class AddEventoRequest
    {
        public int IdSagra { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
    }
}