using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AcquirenteController : Controller
    {
        private readonly IAcquirenteService _acquirenteService;
        public AcquirenteController(IAcquirenteService acquirenteService)
        {
            _acquirenteService = acquirenteService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(AddAcquirenteRequest request)
        {
            var result = await _acquirenteService.AddAcquirenteAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(AcquirenteMapper.ToDto(result))
             );
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _acquirenteService.GetAcquirentiAsync();
            return Ok(
                ResponseFactory
                .WithSuccess(
                    result.Select(s =>
                    AcquirenteMapper.ToDto(s)
                    ).ToList()
                    )
             );
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit(EditAcquirenteRequest request)
        {
            var result = await _acquirenteService.EditAcquirenteAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(AcquirenteMapper.ToDto(result))
             );
        }



    }
}
