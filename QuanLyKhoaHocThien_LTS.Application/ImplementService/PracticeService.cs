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
    public class PracticeService : IPracticeServcie
    {
        private readonly IBaseRespository<Practice> _basePracticeRespository;
        private readonly IBaseRespository<Course> _baseCourseRespository;

        private readonly IBaseRespository<Subject> _baseSubjectRespository;
        private readonly IBaseRespository<SubjectDetail> _baseSubjectDetailRespository;
        private readonly IBaseRespository<CourseSubject> _baseCourseSujectRespository;

        private readonly PracticeConverter _courseConverter;


        public PracticeService(IBaseRespository<Practice> basePracticeRespository,
            IBaseRespository<Subject> baseSubjectRespository,
            IBaseRespository<SubjectDetail> baseSubjectDetailRespository,
            IBaseRespository<CourseSubject> baseCourseSujectRespository,
            IBaseRespository<Course> baseCourseRespository,
            PracticeConverter courseConverter
            )
        {
            _basePracticeRespository = basePracticeRespository;
            _baseSubjectRespository = baseSubjectRespository;
            _baseSubjectDetailRespository = baseSubjectDetailRespository;
            _baseCourseSujectRespository = baseCourseSujectRespository;
            _baseCourseRespository = baseCourseRespository;
            _courseConverter = courseConverter;
        }
        public async Task<ResponseObject<DataResponsePractice>> CreatePracticeasync(int Userid, Request_Practice request_Practice)
        {
            try
            {
                var findSubjectdetail = await _baseSubjectDetailRespository.GetByIdAsync(request_Practice.SubjectDetailId);
                if (findSubjectdetail == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }
                var findCourseSubject = await _baseCourseSujectRespository.GetAsync(c => c.SubjectId == findSubjectdetail.SubjectId);
                if (findCourseSubject == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }

                var findCourse = await _baseCourseRespository.GetByIdAsync(findCourseSubject.CourseId);
                if (findCourse == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }
                if (findCourse.CreatorId != Userid)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Bạn không có quyền thêm bài tập cho bài học", null);
                }
                var practice = new Practice();
                practice.SubjectDetailId = request_Practice.SubjectDetailId;
                practice.Level = request_Practice.Level;
                practice.PracticeCode = request_Practice.PracticeCode;
                practice.Title = request_Practice.Title;
                practice.Topic = request_Practice.Topic;
                practice.ExpectOutput = request_Practice.ExpectOutput;
                practice.LanguageProgrammingId = request_Practice.LanguageProgrammingId;
                practice.IsRequired = request_Practice.IsRequired;
                practice.CreateTime = DateTime.Now;
                practice.MediumScore = request_Practice.MediumScore;
                practice.IsDeleted = request_Practice.IsDeleted;
                await _basePracticeRespository.CreateAsync(practice);
                return new ResponseObject<DataResponsePractice>(200, "Bài tập được thêm với chương học", _courseConverter.EntityToDTO(practice));

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePractice>(400, ex.Message, null);

            }
        }

        public async Task<bool> DeletePracticeasync(int userid, int id)
        {
            try
            {
                var findPractive = await _basePracticeRespository.GetByIdAsync(id);
                if (findPractive == null)
                {
                    return false;
                }

                var findSubjectdetail = await _baseSubjectDetailRespository.GetByIdAsync(findPractive.SubjectDetailId);
                if (findSubjectdetail == null)
                {
                    return false;
                }
                var findCourseSubject = await _baseCourseSujectRespository.GetAsync(c => c.SubjectId == findSubjectdetail.SubjectId);
                if (findCourseSubject == null)
                {
                    return false;
                }

                var findCourse = await _baseCourseRespository.GetByIdAsync(findCourseSubject.CourseId);
                if (findCourse == null)
                {
                    return false;
                }
                if (findCourse.CreatorId != userid)
                {
                    return false;
                }

                await _basePracticeRespository.DeleteAsync(findPractive.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseObject<List<DataResponsePractice>>> GetListPracticeasync()
        {
            try
            {
                var Practices = await _basePracticeRespository.GetAllAsync();
                return new ResponseObject<List<DataResponsePractice>>(200, "Danh sách", _courseConverter.EntityToDTOs(Practices.ToList()));
            }
            catch (Exception)
            {
                return new ResponseObject<List<DataResponsePractice>>(200, "Danh sách", null);
            }
        }

        public async Task<ResponseObject<DataResponsePractice>> GetPracticeByIdasync(int id)
        {
            try
            {
                var Practices = await _basePracticeRespository.GetByIdAsync(id);
                return new ResponseObject<DataResponsePractice>(200, "Danh sách", _courseConverter.EntityToDTO(Practices));
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePractice>(200, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponsePractice>> UpdatePracticeasync(int Userid, int idpractice,Request_Practice request_Practice)
        {
            try
            {
                var findSubjectdetail = await _baseSubjectDetailRespository.GetByIdAsync(request_Practice.SubjectDetailId);
                if (findSubjectdetail == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }
                var findCourseSubject = await _baseCourseSujectRespository.GetAsync(c => c.SubjectId == findSubjectdetail.SubjectId);
                if (findCourseSubject == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }

                var findCourse = await _baseCourseRespository.GetByIdAsync(findCourseSubject.CourseId);
                if (findCourse == null)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Không tìm thấy bài học", null);
                }
                if (findCourse.CreatorId != Userid)
                {
                    return new ResponseObject<DataResponsePractice>(400, "Bạn không có quyền thêm bài tập cho bài học", null);
                }
                var practice = await _basePracticeRespository.GetByIdAsync(idpractice);

                practice.SubjectDetailId = request_Practice.SubjectDetailId;
                practice.Level = request_Practice.Level;
                practice.PracticeCode = request_Practice.PracticeCode;
                practice.Title = request_Practice.Title;
                practice.Topic = request_Practice.Topic;
                practice.ExpectOutput = request_Practice.ExpectOutput;
                practice.LanguageProgrammingId = request_Practice.LanguageProgrammingId;
                practice.IsRequired = request_Practice.IsRequired;
                practice.CreateTime = DateTime.Now;
                practice.MediumScore = request_Practice.MediumScore;
                practice.IsDeleted = request_Practice.IsDeleted;
                await _basePracticeRespository.UpdateAsync(practice);
                return new ResponseObject<DataResponsePractice>(200, "Bài tập đã được sửa thêm với chương học", _courseConverter.EntityToDTO(practice));
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePractice>(200, ex.Message, null);
            }
        }
    }
}
