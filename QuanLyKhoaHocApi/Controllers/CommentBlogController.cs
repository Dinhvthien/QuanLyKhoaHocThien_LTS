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
    public class CommentBlogController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentBlogController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getComment(int idBlog)
        {
            return Ok(await _commentService.GetCommentbyBlogID(idBlog));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> createComment([FromBody] Request_CommentBlog request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _commentService.CreateComment(Id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> deleteComment(int id)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _commentService.DeleteComment(Id, id));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> updateComment(int id, [FromBody] Request_CommentBlog request)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _commentService.UpdateComment(Id, id, request));
        }
    }
}
