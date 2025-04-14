using Application.Models.Requests;
using Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IStockService
    {
        Task<List<StockBiglietto>> GetStocksAsync();
        Task<List<StockBiglietto>> GetStocksByEventoAsync(int idEvento);
        Task<StockBiglietto> AddStockAsync(AddStockRequest request);
        Task<StockBiglietto> EditQuantitaStockAsync(EditQuantitaStockRequest request);
        Task DeleteStockAsync(DeleteStockRequest request);
    }
}
