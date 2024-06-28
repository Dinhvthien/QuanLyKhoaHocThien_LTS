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
    public interface ILikeBlogService
    {
        Task<ResponseObject<List<DataResponseLikeBlog>>> GetLikeBlogbyBlogID(int idBlog);
        Task<ResponseObject<DataResponseLikeBlog>> CreateLikeblog(int userId, Request_LikeBlog model);
        Task<ResponseObject<DataResponseLikeBlog>> UpdateLikeblog(int userId, int likeblogid, Request_LikeBlog model);
        Task<ResponseObject<DataResponseLikeBlog>> DeleteLikeblog(int userId, int id);
    }
}
