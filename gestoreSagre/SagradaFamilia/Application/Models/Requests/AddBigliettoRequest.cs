using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class AddBigliettoRequest : BaseRequest
    {
        public int IdTipoBiglietto { get; set; }
        public List<string> Nominativo { get; set; } = new List<string>();
    }
}