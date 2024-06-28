using Microsoft.EntityFrameworkCore;
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
        private readonly IBaseRespository<Bill> _baseBillRespository;
        private readonly IBaseRespository<BillStatus> _baseBillStatusRespository;





        public RegisterStudyService(IBaseRespository<RegisterStudy> baseRegisterStudyRespository,
            IBaseRespository<Course> baseCourseRespository,
            IBaseRespository<Subject> baseSubectRespository,
            IBaseRespository<SubjectDetail> baseSubectDetailRespository,
            IBaseRespository<DoHomework> baseDoHomeworkRespository,
            IBaseRespository<Bill> baseBillRespository,
            IBaseRespository<BillStatus> baseBillStatusRespository
            )
        {
            _baseRegisterStudyRespository = baseRegisterStudyRespository;
            _baseCourseRespository = baseCourseRespository;
            _baseSubectRespository = baseSubectRespository;
            _baseSubectDetailRespository = baseSubectDetailRespository;
            _baseDoHomeworkRespository = baseDoHomeworkRespository;
            _baseBillRespository = baseBillRespository;
            _baseBillStatusRespository = baseBillStatusRespository;
        }
        private string GenerateTradingCode()
        {
            return Guid.NewGuid().ToString();
        }

        private async Task<int> GetBillStatusId(string statusName)
        {
            var billStatus = await _baseBillStatusRespository.GetAsync(bs => bs.Name.Contains(statusName));
            
            return billStatus != null ? billStatus.Id : 0;
        }

        private async Task<int> CreateBillAsync(int userId, int courseId, decimal price, string tradingCode, int billStatusId)
        {
            var bill = new Bill
            {
                UserId = userId,
                CourseId = courseId,
                Price = price,
                TradingCode = tradingCode,
                CreateTime = DateTime.UtcNow,
                BillStatusId = billStatusId
            };

            await _baseBillRespository.CreateAsync(bill);
            return bill.Id; // Trả về Id của hóa đơn vừa được tạo
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
                newRegisterStudy.IsActive = false;
                newRegisterStudy.PercentComplete = 0;
                await _baseRegisterStudyRespository.CreateAsync(newRegisterStudy);
                decimal price = (await _baseCourseRespository.GetAsync(c => c.Id == RgisStudy.CourseId))?.Price ?? 0;
                // Lấy giá khóa học từ cơ sở dữ liệu
                var tradingCode = GenerateTradingCode();
                var billStatusId = await GetBillStatusId("Chưa thanh toán");
                await CreateBillAsync(userId, RgisStudy.CourseId, price, tradingCode, billStatusId);
                return "url";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
