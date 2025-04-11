using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class EditEventoRequest : BaseRequest
    {
        public int IdEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
    }
}