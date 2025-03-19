using Application.Abstraction.Services;
using Application.Factories;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Application.Mapper;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SagraController : Controller
    {
        private readonly ISagraService _sagraService;
        public SagraController(ISagraService sagraService)
        {
            _sagraService = sagraService;
        }

        [HttpPost]
        [Route("sagra")]
        public async Task<IActionResult> Add(AddSagraRequest request)
        {
            var result = await _sagraService.AddSagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(SagraMapper.ToDto(result))
             );
        }

        [HttpPut]
        [Route("sagra")]
        public async Task<IActionResult> Edit(EditSagraRequest request)
        {
            var result = await _sagraService.EditSagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(SagraMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("sagre")]
        public async Task<IActionResult> GetSagre()
        {
            var result = await _sagraService.GetSagreAsync();
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   SagraMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpDelete]
        [Route("sagra")]
        public async Task<IActionResult> DeleteSagra(DeleteSagraRequest request)
        {
            await _sagraService.DeleteSagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    $"La sagra con identificativo: {request.IdSagra} è stata eliminata")
            );
        }
    }
}
