using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class DeleteStockRequest : BaseRequest
    {
        public int IdStock { get; set; }
    }
}