using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Domain;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class ConfirmFinshSubjectDetail : IConfirmFinshSubjectDetail
    {
        private readonly IBaseRespository<DoHomework> _baseRespository;
        private readonly IBaseRespository<RegisterStudy> _baseRegisterStudyRespository;
        private readonly IBaseRespository<CourseSubject> _baseCourseRespository;
        private readonly IBaseRespository<Practice> _basePracticeRespository;


        public ConfirmFinshSubjectDetail(IBaseRespository<DoHomework> baseRespository,
            IBaseRespository<Practice> basePracticeRespository,
            IBaseRespository<RegisterStudy> baseRegisterStudyRespository,
            IBaseRespository<CourseSubject> baseCourseRespository)
        {
            _baseRespository = baseRespository;
            _basePracticeRespository = basePracticeRespository;
            _baseRegisterStudyRespository = baseRegisterStudyRespository;
            _baseCourseRespository = baseCourseRespository;
        }
        public async Task<string> MarkPracticeAsCompleted(int userId, int practiceId)
        {
            try
            {
                var doHomework = await _baseRespository.GetAsync(dh => dh.UserId == userId && dh.PracticeId == practiceId);

                if (doHomework == null)
                {
                    return "Bài tập không tồn tại hoặc học viên chưa làm bài tập này.";
                }

                doHomework.IsFinished = true;
                doHomework.HomeworkStatus = HomeworkStatus.done;

                await _baseRespository.UpdateAsync(doHomework);
                await UpdatePercentComplete(userId, doHomework.RegisterStudyId);
                return "Đánh giả bài tập làm xong.";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePercentComplete(int userId, int registerStudyId)
        {
            try
            {
                var registerStudy = await _baseRegisterStudyRespository.GetAsync(rs => rs.Id == registerStudyId && rs.UserId == userId);

                if (registerStudy == null)
                {
                    throw new Exception("Đăng ký học không tồn tại.");
                }

                //var courseSubjects = await _context.CourseSubjects
                //    .Where(cs => cs.CourseId == registerStudy.CourseId)
                //    .ToListAsync();
                var courseSubjects = await _baseCourseRespository.GetAllAsync(c => c.CourseId == registerStudy.CourseId);

                int totalPractices = 0;
                int completedPractices = 0;

                foreach (var courseSubject in courseSubjects)
                {
                    var subjectPractices = await _basePracticeRespository.GetAllAsync(p => p.SubjectDetailId == courseSubject.SubjectId);

                    totalPractices += subjectPractices.Count();

                    foreach (var practice in subjectPractices)
                    {
                        var isCompleted = await _baseRespository.GetAllAsync
                            (dh => dh.PracticeId == practice.Id && dh.UserId == userId && dh.IsFinished);

                        if (isCompleted != null && isCompleted.Count() > 0)
                        {
                            completedPractices++;
                        }
                    }
                }

                if (totalPractices > 0)
                {
                    registerStudy.PercentComplete = (completedPractices * 100) / totalPractices;
                }
                else
                {
                    registerStudy.PercentComplete = 0;
                }
                await _baseRegisterStudyRespository.UpdateAsync(registerStudy);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
