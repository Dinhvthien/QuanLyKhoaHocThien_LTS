using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/practice")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        private readonly IPracticeServcie _practiceServcie;

        public PracticeController(IPracticeServcie practiceServcie)
        {
            _practiceServcie = practiceServcie;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _practiceServcie.GetListPracticeasync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getPractice(int id)
        {
            return Ok(await _practiceServcie.GetPracticeByIdasync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> updatePractice(int idPr, [FromBody] Request_Practice request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _practiceServcie.UpdatePracticeasync(Id,idPr, request));
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> deletePractice(int idPr)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);

            return Ok(await _practiceServcie.DeletePracticeasync(Id, idPr));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> createPractice([FromBody] Request_Practice request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _practiceServcie.CreatePracticeasync(Id, request));
        }
    }
}
