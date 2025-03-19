using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/")]
    public class EventoController : Controller
    {
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpPost]
        [Route("evento")]
        public async Task<IActionResult> Add(AddEventoRequest request)
        {
            var result = await _eventoService.AddEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(EventoMapper.ToDto(result))
             );
        }

        [HttpPut]
        [Route("evento")]
        public async Task<IActionResult> Edit(EditEventoRequest request)
        {
            var result = await _eventoService.EditEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(EventoMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("eventi/{id:int}")]
        public async Task<IActionResult> GetEventi(int id)
        {
            var result = await _eventoService.GetEventiBySagraAsync(id);
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   EventoMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpGet]
        [Route("eventi")]
        public async Task<IActionResult> GetEventi()
        {
            var result = await _eventoService.GetEventiAsync();
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   EventoMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpDelete]
        [Route("evento")]
        public async Task<IActionResult> DeleteSagra(DeleteEventoRequest request)
        {
            await _eventoService.DeleteEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    $"L'evento con identificativo: {request.IdEvento} è stato eliminato")
            );
        }
    }
}
