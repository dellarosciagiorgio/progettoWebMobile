using Application.Abstraction.Requests;
using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Requests;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Functions.StaticFunctions;

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
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> Add(AddEventoRequest request)
        {

            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _eventoService.AddEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(EventoMapper.ToDto(result))
             );
        }

        [HttpPut]
        [Route("evento")]
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> Edit(EditEventoRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _eventoService.EditEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(EventoMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("eventi/{idSagra:int}")]
        public async Task<IActionResult> GetEventi(int idSagra)
        {
            var result = await _eventoService.GetEventiBySagraAsync(idSagra);
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
        [Route("eventi/")]
        [Authorize(policy: "ANY_AUTH_USER")]
        public async Task<IActionResult> GetEventiByTime(SomethingByUserRequest request, [FromQuery] bool checkByFuture)
        {
            var userId = User.FindFirst("sub")?.Value;
            var role = User.FindFirst("Ruolo")?.Value;
            CheckUser(userId, request);
            var ruoloEnum = GetRuoloByString(role);
            var result = await _eventoService.GetEventiAsync(int.Parse(userId), ruoloEnum, checkByFuture);
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
        [Route("eventi/bytime")]
        public async Task<IActionResult> GetEventiByTime([FromQuery] bool checkByFuture)
        {
            var result = await _eventoService.GetEventiAsync(checkByFuture);
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
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> DeleteEvento(DeleteEventoRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            await _eventoService.DeleteEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    $"L'evento con identificativo: {request.IdEvento} è stato eliminato")
            );
        }
    }
}
