using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class EditAcquirenteRequest : BaseRequest
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}