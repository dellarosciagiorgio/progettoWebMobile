using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Functions.StaticFunctions;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [Route("feedback/bysagra")]
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> AddBySagra(AddFeedbackBySagraRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _feedbackService.AddFeedbackBySagraAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(FeedbackMapper.ToDto(result))
             );
        }

        [HttpPost]
        [Route("feedback/byevento")]
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> AddByEvento(AddFeedbackByEventoRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _feedbackService.AddFeedbackByEventoAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(FeedbackMapper.ToDto(result))
             );
        }

        [HttpGet]
        [Route("feedbacks")]
        //[Authorize(policy: "IS_ADM")]
        public async Task<IActionResult> Get()
        {
            var result = await _feedbackService.GetFeedbacksAsync();
            return Ok(
                ResponseFactory
                .WithSuccess(
                    result.Select(s =>
                    FeedbackMapper.ToDto(s)
                    ).ToList()
                    )
             );
        }

        [HttpGet]
        [Route("feedbacks/acquirente")]
        [Authorize(policy: "IS_ACQ")]
        public async Task<IActionResult> GetFeedbackByAcquirente(SomethingByUserRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            CheckUser(userId, request);

            var result = await _feedbackService.GetFeedbacksByAcquirenteAsync(request);
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   FeedbackMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpGet]
        [Route("feedbacks/sagra")]
        public async Task<IActionResult> GetFeedbackBySagra(GetFeedbackBySagraRequest request)
        {
            var result = await _feedbackService.GetFeedbacksBySagraAsync(request);
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s =>
                   FeedbackMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpPut]
        [Route("feedback")]
        public async Task<IActionResult> Edit(EditFeedbackRequest request)
        {
            var result = await _feedbackService.EditFeedbackAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(FeedbackMapper.ToDto(result))
             );
        }
        [HttpDelete]
        [Route("feedback")]
        public async Task<IActionResult> DeleteSagra(DeleteFeedbackRequest request)
        {
            await _feedbackService.DeleteFeedback(request);
            return Ok(
                ResponseFactory
                .WithSuccess(
                    $"Il Feedback con identificativo: {request.IdFeedback} è stato eliminato")
            );
        }
    }
}
