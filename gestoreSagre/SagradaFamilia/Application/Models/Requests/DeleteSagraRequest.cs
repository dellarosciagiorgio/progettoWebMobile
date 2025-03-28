using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class DeleteSagraRequest : BaseRequest
    {
        public int IdSagra { get; set; }
    }
}