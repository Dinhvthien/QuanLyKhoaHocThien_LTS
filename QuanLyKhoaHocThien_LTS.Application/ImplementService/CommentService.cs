using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class CommentService : ICommentService
    {
        private readonly IBaseRespository<CommentBlog> _commentRespository;
        private readonly IBaseRespository<Blog> _blogRespository;

        public CommentService(IBaseRespository<CommentBlog> commentRespository, IBaseRespository<Blog> blogRespository)
        {
                _commentRespository = commentRespository;
                _blogRespository = blogRespository;
        }
        public async Task<ResponseObject<DataResponseCommentBlog>> CreateComment(int userId, Request_CommentBlog model)
        {
            try
            {
                var newcm = new CommentBlog();
                newcm.UserId = userId;
                newcm.Content = model.Content;
                newcm.BlogId = model.BlogId;
                newcm.Edited = false;
                await _commentRespository.CreateAsync(newcm);

                var newResponse = new DataResponseCommentBlog();
                newResponse.UserId = userId;
                newResponse.Content = model.Content;
                newResponse.BlogId = model.BlogId;
                newResponse.Edited = false;
                newResponse.Id = newcm.Id;

                var findBlog = await _blogRespository.GetByIdAsync(newcm.BlogId);
                findBlog.NumberOfComments += 1;
                await _blogRespository.UpdateAsync(findBlog);
                return new ResponseObject<DataResponseCommentBlog>(200, "Comment success", newResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseCommentBlog>(200, ex.Message, null);

            }
        }

        public async Task<ResponseObject<DataResponseCommentBlog>> DeleteComment(int userId, int id)
        {
            try
            {
                var find = await _commentRespository.GetByIdAsync(id);
                if (find == null) 
                {
                    return new ResponseObject<DataResponseCommentBlog>(400,"Không tìm thấy comment", null);
                }
                if (find.UserId != userId)
                {
                    return new ResponseObject<DataResponseCommentBlog>(400, "Bạn không có quyền xóa comment", null);
                }
                await _commentRespository.DeleteAsync(find.Id);
                var findBlog = await _blogRespository.GetByIdAsync(find.BlogId);
                findBlog.NumberOfComments -= 1;
                await _blogRespository.UpdateAsync(findBlog);
                return new ResponseObject<DataResponseCommentBlog>(200, "Xóa comment thành công", null);

            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseCommentBlog>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<List<DataResponseCommentBlog>>> GetCommentbyBlogID(int idBlog)
        {
            try
            {
                var find = await _commentRespository.GetAllAsync(c => c.BlogId == idBlog);
                var newResponses = new List<DataResponseCommentBlog>();
                foreach (var item in find)
                {
                    var newResponse = new DataResponseCommentBlog();
                    newResponse.UserId = item.UserId;
                    newResponse.Content = item.Content;
                    newResponse.BlogId = item.BlogId;
                    newResponse.Edited = item.Edited;
                    newResponse.Id = item.Id;
                }
                return new ResponseObject<List<DataResponseCommentBlog>>(200, "Danh sách comment", newResponses);
            }
            catch (Exception ex)
            {

                return new ResponseObject<List<DataResponseCommentBlog>>(200, ex.Message, null);

            }
        }

        public async Task<ResponseObject<DataResponseCommentBlog>> UpdateComment(int userId,int idComment, Request_CommentBlog model)
        {
            try
            {
                var newcm = await _commentRespository.GetByIdAsync(idComment);
                if (userId != newcm.UserId)
                {
                    return new ResponseObject<DataResponseCommentBlog>(400, "Bạn không có quyền sửa comment", null);

                }
                newcm.Content = model.Content;
                newcm.BlogId = model.BlogId;
                newcm.Edited = true;
                await _commentRespository.CreateAsync(newcm);

                var newResponse = new DataResponseCommentBlog();
                newResponse.UserId = userId;
                newResponse.Content = newcm.Content;
                newResponse.BlogId = newcm.BlogId;
                newResponse.Edited = newcm.Edited;
                newResponse.Id = newcm.Id;
                return new ResponseObject<DataResponseCommentBlog>(200, "Update Comment success", newResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseCommentBlog>(500, ex.Message, null);

            }
        }
    }
}
