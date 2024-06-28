using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IAnswersService
    {
        Task<ResponseObject<DataResponseAnswer>> AddAnswers(int userid, int questionId, string answers);
        Task<ResponseObject<DataResponseAnswer>> UpdateAnswers(int userid, int answerId, string answers);
        Task<ResponseObject<DataResponseAnswer>> DeleteAnswers(int userid, int answerId);
        Task<ResponseObject<DataResponseAnswer>> GetAnswers(int answerId);
        Task<ResponseObject<List<DataResponseAnswer>>> GetAnswersByQuestionId(int questionId);

    }
}
