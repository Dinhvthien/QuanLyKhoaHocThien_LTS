using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface ICommentService
    {
        Task<ResponseObject<List<DataResponseCommentBlog>>> GetCommentbyBlogID(int idBlog);
        Task<ResponseObject<DataResponseCommentBlog>> CreateComment(int userId, Request_CommentBlog model);
        Task<ResponseObject<DataResponseCommentBlog>> UpdateComment(int userId,int commentId, Request_CommentBlog model);
        Task<ResponseObject<DataResponseCommentBlog>> DeleteComment(int userId, int id);
    }
}
