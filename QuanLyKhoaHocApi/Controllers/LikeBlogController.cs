using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeBlogController : ControllerBase
    {
        private readonly ILikeBlogService _likeBlogService;
        public LikeBlogController(ILikeBlogService likeBlogService)
        {
            _likeBlogService = likeBlogService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getComment(int idBlog)
        {
            return Ok(await _likeBlogService.GetLikeBlogbyBlogID(idBlog));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> createComment([FromBody] Request_LikeBlog request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _likeBlogService.CreateLikeblog(Id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> deleteComment(int id)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _likeBlogService.DeleteLikeblog(Id, id));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> updateComment(int id, [FromBody] Request_LikeBlog request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _likeBlogService.UpdateLikeblog(Id, id, request));
        }
    }
}
