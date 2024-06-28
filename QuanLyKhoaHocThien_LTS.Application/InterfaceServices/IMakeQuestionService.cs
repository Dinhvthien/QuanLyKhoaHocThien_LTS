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
    public interface IMakeQuestionService
    {
        Task<ResponseObject<DataResponseMakequestion>> CreateMakeQuestion(int userid, Request_MakeQuestion request);
        Task<ResponseObject<DataResponseMakequestion>> UpdateMakeQuestion(int userid, int makeQuestionId, string Question);
        Task<string> DeletrMakeQuestion(int userid, int makeQuestionId);
        Task<ResponseObject<List<DataResponseMakequestion>>> GetMakeQuestionbyUserId(int userid);
    }
}
