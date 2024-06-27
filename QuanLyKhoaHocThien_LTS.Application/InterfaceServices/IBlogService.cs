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
    public interface IBlogService
    {
        Task<ResponseObject<DataResponseBlog>> GetBlogByIdAsync(int id);
        Task<ResponseObject<List<DataResponseBlog>>> GetAllBlogAsync(int idUser);
        Task<ResponseObject<List<DataResponseBlog>>> GetAllBlogAsync();

        Task<ResponseObject<DataResponseBlog>> CreateBlogAsync(int idUser, Request_Blog request_Blog);
        Task<ResponseObject<DataResponseBlog>> UpdateBlogAsync(int idUser, int idBlog, Request_Blog request_Blog);
        Task<bool> DeleteBlogAsync(int idUser, int blogid);


    }
}
