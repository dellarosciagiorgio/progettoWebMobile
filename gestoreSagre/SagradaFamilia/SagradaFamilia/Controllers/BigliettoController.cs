using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Functions.StaticFunctions;

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
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> Add(AddBigliettoRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

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
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> GetBiglietti()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized("Utente non autorizzato");
            }
            var result = await _bigliettoService.GetBigliettiByAcquirenteAsync(int.Parse(userId));
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
