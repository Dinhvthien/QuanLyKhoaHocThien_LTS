using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.ImplementService;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeQuestionController : ControllerBase
    {
        private readonly IMakeQuestionService _makeQuestionService;
        public MakeQuestionController(IMakeQuestionService makeQuestionService)
        {
                _makeQuestionService = makeQuestionService;
        }

        [HttpPost]   
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateMakeQuestion([FromBody] Request_MakeQuestion request_changePassword)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _makeQuestionService.CreateMakeQuestion(Id, request_changePassword));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateMakeQuestion(int MakeQuestionid, string Question)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _makeQuestionService.UpdateMakeQuestion(Id, MakeQuestionid, Question));
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletrMakeQuestion(int MakeQuestionid)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _makeQuestionService.DeletrMakeQuestion(Id, MakeQuestionid));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMakeQuestionbyUserId()
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _makeQuestionService.GetMakeQuestionbyUserId(Id));
        }
    }
}
