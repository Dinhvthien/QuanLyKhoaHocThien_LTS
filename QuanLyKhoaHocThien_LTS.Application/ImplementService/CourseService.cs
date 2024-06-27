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
    public class CourseService : ICourseService
    {
        private readonly IBaseRespository<User> _baseRespository;
        private readonly IBaseRespository<Certificate> _baseCertificateRespository;
        private readonly IBaseRespository<Course> _baseCourseRespository;
        private readonly IBaseRespository<CertificateType> _baseCertificateTypeRespository;
        private readonly IBaseRespository<RegisterStudy> _baseRegisterStudyTypeRespository;
        private readonly IBaseRespository<Notification> _baseNotificationRespository;
        private readonly IBaseRespository<Subject> _baseSubjectRespository;
        private readonly IBaseRespository<CourseSubject> _baseCourseSubjectRespository;
        private readonly CourseConverter _courseConverter;


        public CourseService(IBaseRespository<User> baseRespository,
            IBaseRespository<Certificate> baseCertificateRespository,
            IBaseRespository<Course> baseCourseRespository,
            CourseConverter courseConverter,
            IBaseRespository<CertificateType> baseCertificateTypeRespository,
            IBaseRespository<RegisterStudy> baseRegisterStudyTypeRespository,
            IBaseRespository<Notification> baseNotificationRespository,
            IBaseRespository<Subject> baseSubjectRespository,
            IBaseRespository<CourseSubject> baseCourseSubjectRespository
            )
        {
            _baseRespository = baseRespository;
            _baseCertificateRespository = baseCertificateRespository;
            _baseCourseRespository = baseCourseRespository;
            _courseConverter = courseConverter;
            _baseCertificateTypeRespository = baseCertificateTypeRespository;
            _baseRegisterStudyTypeRespository = baseRegisterStudyTypeRespository;
            _baseNotificationRespository = baseNotificationRespository;
            _baseSubjectRespository = baseSubjectRespository;
            _baseCourseSubjectRespository = baseCourseSubjectRespository;
        }
        public async Task<ResponseObject<DataResponseCourse>> AddCourseAsync(int userId, Request_AddCourse course)
        {
            try
            {
                // timf user
                var findUser = await _baseRespository.GetByIdAsync(userId);

                // cac chung chi cua user dang login
                var findAllCertificateOfUser = await _baseCertificateRespository.GetAllAsync(c => c.Id == findUser.CertificateId);
                if (findAllCertificateOfUser == null || findAllCertificateOfUser.Count() == 0)
                {
                    return new ResponseObject<DataResponseCourse>(400, "Bạn cần có chứng chỉ loại giáo viên để thêm khóa học 1 ", null);
                }

                // tim cac chung chi co name trong do co giang vien
                var findAllCertificateType = await _baseCertificateTypeRespository.GetAllAsync(c => c.Name.ToLower().Contains("giáo viên"));
                if (findAllCertificateType == null || findAllCertificateType.Count() == 0)
                {
                    return new ResponseObject<DataResponseCourse>(400, "Bạn cần có chứng chỉ loại giáo viên để thêm khóa học 2", null);
                }
                // tao list cac chung chi id
                var requiredCertificateTypeIds = findAllCertificateType.Select(c => c.Id).ToList();
                if (findAllCertificateOfUser.Any(c => requiredCertificateTypeIds.Contains(c.CertificateTypeId)) == null)
                {
                    return new ResponseObject<DataResponseCourse>(400, "Bạn cần có chứng chỉ loại giáo viên để thêm khóa học 3", null);
                }
                var CreateCourse = new Course();
                CreateCourse.Name = course.Name;
                CreateCourse.Introduce = course.Introduce;
                CreateCourse.ImageCourse = course.ImageCourse;
                CreateCourse.CreatorId = userId;
                CreateCourse.Code = course.Code;
                CreateCourse.Price = course.Price;
                CreateCourse.TotalCourseDuration = course.TotalCourseDuration;
                CreateCourse.NumberOfStudent = course.NumberOfStudent;
                CreateCourse.NumberOfPurchases = course.NumberOfPurchases;
                await _baseCourseRespository.CreateAsync(CreateCourse);

                var allCoursebyCreator = await _baseCourseRespository.GetAllAsync(c => c.CreatorId == userId);
                if (allCoursebyCreator != null)
                {
                    var requiredCourseId = allCoursebyCreator.Select(c => c.Id).ToList();
                    var GetAllRegisterStudy = await _baseRegisterStudyTypeRespository.GetAllAsync(c => requiredCourseId.Contains(c.CourseId));
                    if (GetAllRegisterStudy != null)
                    {
                        foreach (var item in GetAllRegisterStudy)
                        {
                            var CreateNotification = new Notification();
                            CreateNotification.UserId = item.UserId;
                            CreateNotification.Image = course.ImageCourse;
                            CreateNotification.Link = $"/course/{item.CourseId}";
                            CreateNotification.IsSeen = false;
                            CreateNotification.CreateTime = DateTime.Now;
                            CreateNotification.Content = $"Giảng viên {findUser.FullName} đã tạo ra một khoá học {course.Name} trên hệ thống";
                            await _baseNotificationRespository.CreateAsync(CreateNotification);
                        }
                    }
                }
                return new ResponseObject<DataResponseCourse>(200, "Thêm khóa học thành công", _courseConverter.EntityToDTO(CreateCourse));
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseCourse>(200, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseSubject>> AddSubjectAsync(int userId, Request_AddSubject course)
        {
            try
            {
                var findCourse = await _baseCourseRespository.GetByIdAsync(course.CourseId);
                if (findCourse == null)
                {
                    return new ResponseObject<DataResponseSubject>(400, "Khóa học không tồn tại", null);
                }
                if (findCourse.CreatorId != userId)
                {
                    return new ResponseObject<DataResponseSubject>(400, "Bạn không có quyền thêm chương học này", null);
                }
                var CreateSubject = new Subject();
                CreateSubject.Name = course.Name;
                CreateSubject.Symbol = course.Symbol;
                CreateSubject.IsActive = course.IsActive;
                await _baseSubjectRespository.CreateAsync(CreateSubject);
                var CreateNewCourseSubject = new CourseSubject();
                CreateNewCourseSubject.SubjectId = CreateSubject.Id;
                CreateNewCourseSubject.CourseId = course.CourseId;
                await _baseCourseSubjectRespository.CreateAsync(CreateNewCourseSubject);
                DataResponseSubject dataResponseSubject = new DataResponseSubject();
                dataResponseSubject.Id = CreateSubject.Id;
                dataResponseSubject.Name = CreateSubject.Name;
                dataResponseSubject.Symbol = CreateSubject.Symbol;
                dataResponseSubject.IsActive = CreateSubject.IsActive;
                return new ResponseObject<DataResponseSubject>(200, "Thêm chương học này thành công", dataResponseSubject);

            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseSubject>(400, ex.Message, null);

            }
        }
    }
}