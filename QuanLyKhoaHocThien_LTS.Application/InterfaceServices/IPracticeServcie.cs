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
    public interface IPracticeServcie
    {
        Task<ResponseObject<List<DataResponsePractice>>> GetListPracticeasync();
        Task<ResponseObject<DataResponsePractice>> GetPracticeByIdasync(int id);
        Task<bool> DeletePracticeasync(int userid,int id);
        Task<ResponseObject<DataResponsePractice>> CreatePracticeasync(int Userid, Request_Practice request_Practice);
        Task<ResponseObject<DataResponsePractice>> UpdatePracticeasync(int Userid, int idpractice, Request_Practice request_Practice);

    }
}
