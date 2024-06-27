using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class RegisterStudyService : IRegisterStudyService
    {
        private readonly IBaseRespository<RegisterStudy> _baseRegisterStudyRespository;
        private readonly IBaseRespository<Course> _baseCourseRespository;
        private readonly IBaseRespository<Subject> _baseSubectRespository;
        private readonly IBaseRespository<SubjectDetail> _baseSubectDetailRespository;
        private readonly IBaseRespository<DoHomework> _baseDoHomeworkRespository;



        public RegisterStudyService(IBaseRespository<RegisterStudy> baseRegisterStudyRespository,
            IBaseRespository<Course> baseCourseRespository,
            IBaseRespository<Subject> baseSubectRespository,
            IBaseRespository<SubjectDetail> baseSubectDetailRespository,
            IBaseRespository<DoHomework> baseDoHomeworkRespository
            )
        {
            _baseRegisterStudyRespository = baseRegisterStudyRespository;
            _baseCourseRespository = baseCourseRespository;
            _baseSubectRespository = baseSubectRespository;
            _baseSubectDetailRespository = baseSubectDetailRespository;
            _baseDoHomeworkRespository = baseDoHomeworkRespository;
        }
        public async Task<string> RegisterStudy(int userId, Request_RegisterStudy RgisStudy)
        {
            try
            {
                var findCourse = await _baseCourseRespository.GetByIdAsync(RgisStudy.CourseId);
                if (findCourse == null) { return "Khóa học bạn đăng ký không tồn tại"; }
                var newRegisterStudy = new RegisterStudy();
                newRegisterStudy.UserId = userId;
                newRegisterStudy.CourseId = RgisStudy.CourseId;
                newRegisterStudy.RegisterTime = DateTime.Now;
                newRegisterStudy.CurrentSubjectId = RgisStudy.CurrentSubjectId;
                newRegisterStudy.IsFinished = false;
                newRegisterStudy.IsActive = true;
                newRegisterStudy.PercentComplete = 0;
                await _baseRegisterStudyRespository.CreateAsync(newRegisterStudy);

                //var findAllSubject = await _baseSubectRespository.GetAsync(x => x.Id == RgisStudy.CurrentSubjectId);
                //var findAllSubjectDetail = await _baseSubectDetailRespository.GetAllAsync(c=>c.SubjectId == findAllSubject.Id);
                //foreach (var item in findAllSubjectDetail)
                //{
                //    var DoHomework = new DoHomework
                //    {
                //        UserId = userId,
                //        HomeworkStatus = Domain.HomeworkStatus.notdone,
                //        PracticeId = item.Id,
                //        ActualOutput = "",
                //        IsFinished = false,
                //        RegisterStudyId = newRegisterStudy.Id,
                //    };
                //    await _baseDoHomeworkRespository.CreateAsync(DoHomework);
                //}

                return "url";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
