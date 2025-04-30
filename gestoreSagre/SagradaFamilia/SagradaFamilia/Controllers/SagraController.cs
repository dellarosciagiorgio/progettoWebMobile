using Application.Abstraction.Services;
using Application.Factories;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.Mapper;
using Microsoft.AspNetCore.Authorization;
using static Application.Functions.StaticFunctions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SagraController : Controller
    {
        private readonly ISagraService _sagraService;
        private readonly IUserService _userService;
        public SagraController(ISagraService sagraService, IUserService userService)
        {
            _sagraService = sagraService;
            _userService = userService;
        }

        [HttpPost]
        [Route("sagra")]
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> Add(AddSagraRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _sagraService.AddSagraAsync(request);

            return Ok(
                ResponseFactory
                .WithSuccess(SagraMapper.ToDto(result))
            );
        }

        [HttpPut]
        [Route("sagra")]
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> Edit(EditSagraRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            var idorg = await _userService.GetOrgIdByUserIdAsync(int.Parse(userId));
            CheckUser(userId, request);
            //request.IdUser = idorg;
            var result = await _sagraService.EditSagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(SagraMapper.ToDto(result))
            );
        }

        [HttpGet]
        [Route("sagre/mysagre")]
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> GetSagreByOrg()
        {
            var userId = User.FindFirst("sub")?.Value;
            var result = await _sagraService.GetSagreByOrgAsync(int.Parse(userId));
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   SagraMapper.ToDto(s)
                   ).ToList()
                   )
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
        [HttpGet]
        [Route("sagre/{id:int}")]
        public async Task<IActionResult> GetSagre(int id)
        {
            var result = await _sagraService.GetSagreAsync(id);
            return Ok(
               ResponseFactory
               .WithSuccess(
                   SagraMapper.ToDto(result)
                   )
            );
        }

        [HttpDelete]
        [Route("sagra")]
        [Authorize(policy: "IS_ORG")]
        public async Task<IActionResult> DeleteSagra(DeleteSagraRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            await _sagraService.DeleteSagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    $"La sagra con identificativo: {request.IdSagra} è stata eliminata")
            );
        }
        [HttpGet]
        [Route("sagra/ratingbyid/{id:int}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var result = await _sagraService.GetRatingSagraAsync(id);

            return Ok(
                ResponseFactory
                .WithSuccess(result)
            );
        }
    }
}
