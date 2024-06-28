using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProgressController : ControllerBase
    {
        private readonly IStudentProgressService _studentProgressService;
        public StudentProgressController(IStudentProgressService studentProgressService)
        {
            _studentProgressService = studentProgressService;
        }
        [HttpGet]
        [Route("student-progress-in/{courseid}")]
        public async Task<IActionResult> GetCourseProgressAsync(int courseid)
        {
            var progressList = await _studentProgressService.GetCourseProgressAsync(courseid);
            return Ok(progressList);
        }
    }
}
