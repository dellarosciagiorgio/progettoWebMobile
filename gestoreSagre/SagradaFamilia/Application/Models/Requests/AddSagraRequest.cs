using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class AddSagraRequest : BaseRequest
    {
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
    }
}