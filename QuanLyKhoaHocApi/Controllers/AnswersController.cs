using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answerService;
        public AnswersController(IAnswersService answersService)
        {
            _answerService = answersService;
        }

        [HttpGet]
        [Route("get-answer-by-question-id/{id}")]
        public async Task<IActionResult> GetAnswerByQestionIdAsync(int id)
        {
            return Ok(await _answerService.GetAnswersByQuestionId(id));
        }

        [HttpGet]
        [Route("Get-answer-by-id/{id}")]
        public async Task<IActionResult> GetAnswerByIdAsync(int id)
        {
            return Ok(await _answerService.GetAnswers(id));
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAnswerAsync(int idans)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _answerService.DeleteAnswers(Id, idans));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateAnswerAsync(int QuestionId, string Answer)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _answerService.AddAnswers(Id, QuestionId, Answer));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAnswerAsync(int id, string Answer)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _answerService.UpdateAnswers(Id, id, Answer));
        }
    }
}
