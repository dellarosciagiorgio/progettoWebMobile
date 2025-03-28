using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class EditEventoRequest : BaseRequest
    {
        public int IdEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
    }
}