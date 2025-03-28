using Application.Abstraction.Requests;

namespace Application.Models.Request
{
    public class EditQuantitaStockRequest : BaseRequest
    {
        public int IdStock { get; set; }
        public int Quantita { get; set; }
    }
}