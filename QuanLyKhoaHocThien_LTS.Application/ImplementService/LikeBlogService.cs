using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class LikeBlogService : ILikeBlogService
    {
        private readonly IBaseRespository<LikeBlog> _baseRespository;
        private readonly IBaseRespository<Blog> _blogRespository;

        public LikeBlogService(IBaseRespository<LikeBlog> baseRespository, IBaseRespository<Blog> blogRespository)
        {
            _baseRespository = baseRespository;
            _blogRespository = blogRespository;
        }
        public async Task<ResponseObject<DataResponseLikeBlog>> CreateLikeblog(int userId, Request_LikeBlog model)
        {
            try
            {
                var newcm = new LikeBlog();
                newcm.UserId = userId;
                newcm.BlogId = model.BlogId;
                newcm.Unlike = false;
                newcm.CreateTime = DateTime.Now;
                await _baseRespository.CreateAsync(newcm);

                var newResponse = new DataResponseLikeBlog();
                newResponse.UserId = userId;
                newResponse.Unlike = newcm.Unlike;
                newResponse.BlogId = model.BlogId;
                newResponse.CreateTime = newcm.CreateTime;
                newResponse.Id = newcm.Id;


                var findBlog = await _blogRespository.GetByIdAsync(newcm.BlogId);
                findBlog.NumberOfComments += 1;
                await _blogRespository.UpdateAsync(findBlog);
                return new ResponseObject<DataResponseLikeBlog>(200, "Like Blog success", newResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseLikeBlog>(200, ex.Message, null);

            }
        }

        public async Task<ResponseObject<DataResponseLikeBlog>> DeleteLikeblog(int userId, int id)
        {
            try
            {
                var find = await _baseRespository.GetByIdAsync(id);
                if (find == null)
                {
                    return new ResponseObject<DataResponseLikeBlog>(400, "Không tìm thấy comment", null);
                }
                if (find.UserId != userId)
                {
                    return new ResponseObject<DataResponseLikeBlog>(400, "Bạn không có quyền xóa comment", null);
                }
                await _baseRespository.DeleteAsync(find.Id);
                var findBlog = await _blogRespository.GetByIdAsync(find.BlogId);
                findBlog.NumberOfComments -= 1;
                await _blogRespository.UpdateAsync(findBlog);
                return new ResponseObject<DataResponseLikeBlog>(200, "Xóa comment thành công", null);

            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseLikeBlog>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<List<DataResponseLikeBlog>>> GetLikeBlogbyBlogID(int idBlog)
        {
            try
            {
                var find = await _baseRespository.GetAllAsync(c => c.BlogId == idBlog);
                var newResponses = new List<DataResponseLikeBlog>();
                foreach (var item in find)
                {
                    var newResponse = new DataResponseLikeBlog();
                    newResponse.UserId = item.UserId;
                    newResponse.Unlike = item.Unlike;
                    newResponse.BlogId = item.BlogId;
                    newResponse.CreateTime = item.CreateTime;
                    newResponse.Id = item.Id;
                    newResponses.Add(newResponse);
                }
                return new ResponseObject<List<DataResponseLikeBlog>>(200, "Danh sách comment", newResponses);
            }
            catch (Exception ex)
            {

                return new ResponseObject<List<DataResponseLikeBlog>>(200, ex.Message, null);

            }
        }

        public async Task<ResponseObject<DataResponseLikeBlog>> UpdateLikeblog(int userId, int likeblogid, Request_LikeBlog model)
        {
            try
            {
                var newcm = await _baseRespository.GetByIdAsync(likeblogid);
                if (userId != newcm.UserId)
                {
                    return new ResponseObject<DataResponseLikeBlog>(400, "Bạn không có quyền sửa", null);

                }
                newcm.Unlike = model.Unlike;
                newcm.BlogId = model.BlogId;
                newcm.UpdateTime = DateTime.Now;
                await _baseRespository.CreateAsync(newcm);

                var newResponse = new DataResponseLikeBlog();
                newResponse.UserId = newcm.UserId;
                newResponse.Unlike = newcm.Unlike;
                newResponse.BlogId = newcm.BlogId;
                newResponse.CreateTime = newcm.CreateTime;
                newResponse.Id = newcm.Id;
                return new ResponseObject<DataResponseLikeBlog>(200, "Update like blog success", newResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseLikeBlog>(500, ex.Message, null);

            }
        }
    }
}
