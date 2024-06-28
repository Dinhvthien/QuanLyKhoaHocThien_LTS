using Org.BouncyCastle.Asn1.Ocsp;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class MakeQuestionService : IMakeQuestionService
    {
        private readonly IBaseRespository<MakeQuestion> _baseRespository;
        public MakeQuestionService(IBaseRespository<MakeQuestion> baseRespository)
        {
            _baseRespository = baseRespository;
        }
        public async Task<ResponseObject<DataResponseMakequestion>> CreateMakeQuestion(int userid, Request_MakeQuestion request)
        {
            try
            {
                var MQ = new MakeQuestion();
                MQ.UserId = userid;
                MQ.SubjectDetailId = request.SubjectDetailId;
                MQ.Question = request.Question;
                MQ.NumberOfAnswers = 0;
                MQ.CreateTime = DateTime.Now;

                var response = new DataResponseMakequestion();
                response.Id = MQ.Id;
                response.Question = request.Question;
                response.UserId = userid;
                response.NumberOfAnswers = 0;
                response.CreateTime = DateTime.Now;
                response.SubjectDetailId = MQ.SubjectDetailId;



                await _baseRespository.CreateAsync(MQ);
                return new ResponseObject<DataResponseMakequestion>(200, "Create make question success", response);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseMakequestion>(400, ex.Message, null);

            }
        }

        public async Task<string> DeletrMakeQuestion(int userid, int makeQuestionId)
        {
            try
            {
                var findMakeQ = await _baseRespository.GetByIdAsync(makeQuestionId);
                if (findMakeQ == null)
                {
                    return "Not Found";
                }

                if (findMakeQ.UserId != userid)
                {
                    return "Bạn không có quyền xóa câu hỏi này";
                }
                await _baseRespository.DeleteAsync(makeQuestionId);
                return "Xóa câu hỏi thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ResponseObject<List<DataResponseMakequestion>>> GetMakeQuestionbyUserId(int userid)
        {
            try
            {
                var findAllMakeQByUserID = await _baseRespository.GetAllAsync(c=>c.UserId == userid);
                if (findAllMakeQByUserID == null || findAllMakeQByUserID.Count() == 0)
                {
                    return new ResponseObject<List<DataResponseMakequestion>>(400, "Danh sách câu hỏi của bạn", null);
                }
                var responses = new List<DataResponseMakequestion>();
                foreach (var MQ in findAllMakeQByUserID)
                {
                    var response = new DataResponseMakequestion();
                    response.Id = MQ.Id;
                    response.Question = MQ.Question;
                    response.UserId = userid;
                    response.NumberOfAnswers = MQ.NumberOfAnswers;
                    response.CreateTime = MQ.CreateTime;
                    response.UpdateTime = MQ.UpdateTime;
                    response.SubjectDetailId = MQ.SubjectDetailId;
                    responses.Add(response);
                }
                return new ResponseObject<List<DataResponseMakequestion>>(200, "Danh sách câu hỏi của bạn", responses);

            }
            catch (Exception ex)
            {

                return new ResponseObject<List<DataResponseMakequestion>>(400, ex.Message, null);

            }
        }

        public async Task<ResponseObject<DataResponseMakequestion>> UpdateMakeQuestion(int userid, int makeQuestionId, string Question)
        {
            try
            {
                var MQ = await _baseRespository.GetByIdAsync(makeQuestionId);
                if (userid != MQ.UserId)
                {
                return new ResponseObject<DataResponseMakequestion>(400, "bạn không có quyền để sửa câu hỏi này", null);

                }
                MQ.Question = Question;
                MQ.UpdateTime = DateTime.Now;
                var response = new DataResponseMakequestion();
                response.Id = MQ.Id;
                response.Question = Question;
                response.UserId = MQ.UserId;
                response.NumberOfAnswers = MQ.UserId;
                response.CreateTime = MQ.CreateTime;
                response.UpdateTime = MQ.UpdateTime;
                response.SubjectDetailId = MQ.SubjectDetailId;
                await _baseRespository.UpdateAsync(MQ);
                return new ResponseObject<DataResponseMakequestion>(200, "update make question success", response);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseMakequestion>(400, ex.Message, null);

            }
        }
    }
}
