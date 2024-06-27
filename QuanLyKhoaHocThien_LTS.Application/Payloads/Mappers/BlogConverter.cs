using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers
{
    public class BlogConverter
    {
        public DataResponseBlog EntityToDTO(Blog course)
        {
            return new DataResponseBlog
            {
                Id = course.Id,
                Title = course.Title,
                Content = course.Content,
                CreatorId = course.CreatorId,
                CreateTime = course.CreateTime,
                NumberOfComments = course.NumberOfComments,
                NumberOfLikes = course.NumberOfLikes
            };
        }

        public List<DataResponseBlog> EntityToDTOs(List<Blog> course)
        {
            var result = new List<DataResponseBlog>();
            foreach (var item in course)
            {
                var Practice = EntityToDTO(item);
                result.Add(Practice);
            }
            return result;
        }
    }
}
