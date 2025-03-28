using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class DeleteEventoRequest : BaseRequest
    {
        public int IdEvento { get; set; }
    }
}