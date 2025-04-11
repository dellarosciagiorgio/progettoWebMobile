using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class AddSagraRequest : BaseRequest
    {
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
    }
}