using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers;
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

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class BlogService : IBlogService
    {
        private readonly IBaseRespository<Blog> _baseBlogRespository;
        private readonly BlogConverter _blogConverter;

        public BlogService(IBaseRespository<Blog> baseBlogRespository, BlogConverter blogConverter)
        {
            _baseBlogRespository = baseBlogRespository;
            _blogConverter = blogConverter;
        }

        public async Task<ResponseObject<DataResponseBlog>> CreateBlogAsync(int idUser, Request_Blog request_Blog)
        {
            try
            {
                var BlogNew = new Blog();
                BlogNew.Content = request_Blog.Content;
                BlogNew.Title = request_Blog.Title;
                BlogNew.CreatorId = idUser;
                BlogNew.NumberOfLikes = 0;
                BlogNew.NumberOfComments = 0;
                BlogNew.CreateTime = DateTime.Now;
                await _baseBlogRespository.CreateAsync(BlogNew);
                return new ResponseObject<DataResponseBlog>(200, "Tạo Blog thành công", _blogConverter.EntityToDTO(BlogNew));

            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseBlog>(400, ex.Message, null);

            }
        }

        public async Task<bool> DeleteBlogAsync(int idUser, int blogid)
        {
            try
            {
                var findBlog = await _baseBlogRespository.GetByIdAsync(blogid);
                if (findBlog == null)
                {
                    return false;
                }

                if (findBlog.CreatorId != idUser)
                {
                    return false;
                }
                await _baseBlogRespository.DeleteAsync(blogid);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ResponseObject<List<DataResponseBlog>>> GetAllBlogAsync(int id)
        {
            try
            {
                var getAllBlogbyCreateid = await _baseBlogRespository.GetAllAsync(c => c.CreatorId == id);
                return new ResponseObject<List<DataResponseBlog>>(200, "Danh sách", _blogConverter.EntityToDTOs(getAllBlogbyCreateid.ToList()));
            }
            catch (Exception ex)
            {
                return new ResponseObject<List<DataResponseBlog>>(200, "Danh sách", null);
            }
        }

        public async Task<ResponseObject<List<DataResponseBlog>>> GetAllBlogAsync()
        {
            try
            {
                var getAllBlogbyCreateid = await _baseBlogRespository.GetAllAsync();
                return new ResponseObject<List<DataResponseBlog>>(200, "Danh sách", _blogConverter.EntityToDTOs(getAllBlogbyCreateid.ToList()));
            }
            catch (Exception ex)
            {
                return new ResponseObject<List<DataResponseBlog>>(200, "Danh sách", null);
            }
        }

        public async Task<ResponseObject<DataResponseBlog>> GetBlogByIdAsync(int id)
        {
            try
            {
                var getAllBlogbyCreateid = await _baseBlogRespository.GetAsync(c => c.Id == id);
                return new ResponseObject<DataResponseBlog>(200, "Danh sách", _blogConverter.EntityToDTO(getAllBlogbyCreateid));
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseBlog>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseBlog>> UpdateBlogAsync(int idUser, int idBlog, Request_Blog request_Blog)
        {
            try
            {

                var BlogNew = await _baseBlogRespository.GetByIdAsync(idBlog);
                if (BlogNew != null)
                {
                    return new ResponseObject<DataResponseBlog>(400, "Not found", null);
                }

                if (BlogNew.CreatorId != idUser)
                {
                    return new ResponseObject<DataResponseBlog>(400, "Bạn không có quyền để Sửa bài viết", null);
                }
                BlogNew.Content = request_Blog.Content;
                BlogNew.Title = request_Blog.Title;
                BlogNew.NumberOfLikes = 0;
                BlogNew.NumberOfComments = 0;
                BlogNew.UpdateTime = DateTime.Now;
                await _baseBlogRespository.CreateAsync(BlogNew);
                return new ResponseObject<DataResponseBlog>(200, "Sửa Blog thành công", _blogConverter.EntityToDTO(BlogNew));

            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseBlog>(400, ex.Message, null);

            }
        }
    }
}
