using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/")]
    public class StockController : Controller
    {

        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [Route("stock")]
        public async Task<IActionResult> Add(AddStockRequest request)
        {
            var result = await _stockService.AddStockAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(StockMapper.ToDto(result))
             );
        }

        [HttpPut]
        [Route("stock")]
        public async Task<IActionResult> Edit(EditQuantitaStockRequest request)
        {
            var result = await _stockService.EditQuantitaStockAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(StockMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("stocks/{id:int}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var result = await _stockService.GetStocksByEventoAsync(id);
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   StockMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpGet]
        [Route("stocks")]
        public async Task<IActionResult> GetStock()
        {
            var result = await _stockService.GetStocksAsync();
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   StockMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }
    }
}
