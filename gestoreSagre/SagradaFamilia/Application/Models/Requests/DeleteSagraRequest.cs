using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class DeleteSagraRequest : BaseRequest
    {
        public int IdSagra { get; set; }
    }
}