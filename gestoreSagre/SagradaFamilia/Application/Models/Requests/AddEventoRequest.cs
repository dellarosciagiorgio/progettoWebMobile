using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class AddEventoRequest : BaseRequest
    {
        public int IdSagra { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
    }
}