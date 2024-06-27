using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface ICourseService
    {
        Task<ResponseObject<DataResponseCourse>> AddCourseAsync(int userId, Request_AddCourse course);
        Task<ResponseObject<DataResponseSubject>> AddSubjectAsync(int userId, Request_AddSubject course);
        Task<ResponseObject<DataResponseSubject>> AddSubjectDetailAsync(int userId, Request_AddSubject course);


    }
}
