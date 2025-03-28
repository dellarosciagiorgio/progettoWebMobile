using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class EditAcquirenteRequest : BaseRequest
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}