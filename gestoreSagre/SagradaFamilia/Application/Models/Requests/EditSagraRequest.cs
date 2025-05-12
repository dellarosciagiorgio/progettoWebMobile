using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class EditSagraRequest : BaseRequest
    {
        public int IdSagra { get; set; }
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public string Luogo { get; set; }
    }
}