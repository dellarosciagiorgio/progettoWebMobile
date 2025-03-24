using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BigliettoController : Controller
    {
        private readonly IBigliettoService _bigliettoService;
        public BigliettoController(IBigliettoService bigliettoService)
        {
            _bigliettoService = bigliettoService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBigliettoRequest request)
        {
            var result = await _bigliettoService.AddBigliettiAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    result.Select(s =>
                    BigliettoMapper.ToDto(s)
                    ).ToList()
                    )
             );
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBiglietti( int id)
        {
            var result = await _bigliettoService.GetBigliettiByAcquirenteAsync(id);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    result.Select(s =>
                    BigliettoMapper.ToDto(s)
                    ).ToList()
                    )
             );
        }
    }
}
