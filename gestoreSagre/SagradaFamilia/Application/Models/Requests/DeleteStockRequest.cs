using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class DeleteStockRequest : BaseRequest
    {
        public int IdStock { get; set; }
    }
}