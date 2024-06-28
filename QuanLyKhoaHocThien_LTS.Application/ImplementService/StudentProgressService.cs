using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class StudentProgressService : IStudentProgressService
    {
        private readonly IBaseRespository<RegisterStudy> _registerStudyRespository;
        private readonly IBaseRespository<User> _userRespository;
        private readonly IBaseRespository<Course> _courseRespository;



        public StudentProgressService(IBaseRespository<RegisterStudy> registerStudyRespository, IBaseRespository<User> userRespository, IBaseRespository<Course> courseRespository)
        {
            _registerStudyRespository = registerStudyRespository;
            _userRespository = userRespository;
            _courseRespository = courseRespository;
        }
        public async Task<List<DataResponse_StudentProgress>> GetCourseProgressAsync(int courseId)
        {
            var registerStudies = await _registerStudyRespository.GetAllAsync(c => c.CourseId == courseId && c.IsActive);

            var progressList = new List<DataResponse_StudentProgress>();
            foreach (var rs in registerStudies)
            {
                var user = await _userRespository.GetAsync(c => c.Id == rs.UserId);
                var course = await _courseRespository.GetAsync(c => c.Id == rs.CourseId);

                var progress = new DataResponse_StudentProgress
                {
                    UserId = rs.UserId,
                    UserName = user.FullName,
                    CourseId = rs.CourseId,
                    CourseName = course.Name,
                    PercentComplete = rs.PercentComplete,
                    IsFinished = rs.IsFinished,
                    RegisterTime = rs.RegisterTime,
                    DoneTime = rs.DoneTime
                };

                progressList.Add(progress);
            }

            return progressList;
        }


    }
}
