using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers
{
    public class CourseConverter
    {
        public DataResponseCourse EntityToDTO(Course course)
        {
            return new DataResponseCourse
            {
                Id = course.Id,
                Name = course.Name,
                Introduce = course.Introduce,
                ImageCourse = course.ImageCourse,
                CreatorId = course.CreatorId,
                Code = course.Code,
                Price = course.Price,
                TotalCourseDuration = course.TotalCourseDuration,
                NumberOfPurchases = course.NumberOfPurchases,
                NumberOfStudent = course.NumberOfStudent,

            };
        }
    }
}
