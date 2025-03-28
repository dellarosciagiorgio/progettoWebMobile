using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Functions.StaticFunctions;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/")]
    public class AcquirenteController : Controller
    {
        private readonly IAcquirenteService _acquirenteService;
        public AcquirenteController(IAcquirenteService acquirenteService)
        {
            _acquirenteService = acquirenteService;
        }

        [HttpPost]
        [Route("acquirente")]
        public async Task<IActionResult> Add(AddAcquirenteRequest request)
        {
            var result = await _acquirenteService.AddAcquirenteAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(AcquirenteMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("acquirenti")]
        //[Authorize(policy: "IS_ADM")]
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
        [Route("acquirente")]
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> Edit(EditAcquirenteRequest request)
        {

            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);
            var result = await _acquirenteService.EditAcquirenteAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(AcquirenteMapper.ToDto(result))
             );
        }
    }
}