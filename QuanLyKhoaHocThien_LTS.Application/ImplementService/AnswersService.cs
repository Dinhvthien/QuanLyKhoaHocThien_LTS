using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
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
    public class AnswersService : IAnswersService
    {
        private readonly IBaseRespository<Answers> _baseaAnswersRespository;
        private readonly IBaseRespository<MakeQuestion> _baseMakeQuestionRespository;

        public AnswersService(IBaseRespository<Answers> baseaAnswersRespository, IBaseRespository<MakeQuestion> baseMakeQuestionRespository)
        {
            _baseaAnswersRespository = baseaAnswersRespository;
            _baseMakeQuestionRespository = baseMakeQuestionRespository;
        }
        public async Task<ResponseObject<DataResponseAnswer>> AddAnswers(int userid, int questionId, string answers)
        {
            try
            {
                var findMq = await _baseMakeQuestionRespository.GetByIdAsync(questionId);
                if (findMq == null) { return new ResponseObject<DataResponseAnswer>(400, "Not found", null); }
                var newAnswers = new Answers();
                newAnswers.Answer = answers;
                newAnswers.QuestionId = questionId;
                newAnswers.UserId = userid;
                newAnswers.CreateTime = DateTime.Now;
                await _baseaAnswersRespository.CreateAsync(newAnswers);

                var newResponse = new DataResponseAnswer();
                newResponse.Answer = answers;
                newResponse.QuestionId = questionId;
                newResponse.UserId = userid;
                newResponse.CreateTime = DateTime.Now;
                newResponse.Id = newAnswers.Id;
                newResponse.UpdateTime = newAnswers.UpdateTime;
                return new ResponseObject<DataResponseAnswer>(200, "Add answers thanh cong", newResponse);
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseAnswer>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseAnswer>> DeleteAnswers(int userid, int answerId)
        {
            try
            {
                var findAnswers = await _baseaAnswersRespository.GetByIdAsync(answerId);

                if (findAnswers == null) { return new ResponseObject<DataResponseAnswer>(400, "Not found", null); }
                if (userid != findAnswers.UserId)
                {
                    return new ResponseObject<DataResponseAnswer>(400, "Bạn không có quyền xóa answers", null);
                }
                await _baseaAnswersRespository.DeleteAsync(findAnswers.Id);
                return new ResponseObject<DataResponseAnswer>(200, "Delete answers thanh cong", null);
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseAnswer>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseAnswer>> GetAnswers(int answerId)
        {
            try
            {
                var findAnswers = await _baseaAnswersRespository.GetByIdAsync(answerId);
                if (findAnswers == null) { return new ResponseObject<DataResponseAnswer>(400, "Not found", null); }

                var newResponse = new DataResponseAnswer();
                newResponse.Answer = findAnswers.Answer;
                newResponse.QuestionId = findAnswers.QuestionId;
                newResponse.UserId = findAnswers.UserId;
                newResponse.CreateTime = DateTime.Now;
                newResponse.Id = findAnswers.Id;
                newResponse.UpdateTime = findAnswers.UpdateTime;

                return new ResponseObject<DataResponseAnswer>(200, "Get answers thanh cong", newResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseAnswer>(400, ex.Message, null);

            }
        }

        public async Task<ResponseObject<List<DataResponseAnswer>>> GetAnswersByQuestionId(int questionId)
        {
            try
            {
                var findAnswers = await _baseaAnswersRespository.GetAllAsync(c => c.QuestionId == questionId);
                if (findAnswers == null) { return new ResponseObject<List<DataResponseAnswer>>(400, "Not found", null); }
                var newResponses = new List<DataResponseAnswer>();
                foreach (var item in findAnswers)
                {
                    var newResponse = new DataResponseAnswer();
                    newResponse.Answer = item.Answer;
                    newResponse.QuestionId = item.QuestionId;
                    newResponse.UserId = item.UserId;
                    newResponse.CreateTime = item.CreateTime;
                    newResponse.Id = item.Id;
                    newResponse.UpdateTime = item.UpdateTime;
                    newResponses.Add(newResponse);
                }
                return new ResponseObject<List<DataResponseAnswer>>(200, "Get answers thanh cong", newResponses);
            }
            catch (Exception ex)
            {
                return new ResponseObject<List<DataResponseAnswer>>(200, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseAnswer>> UpdateAnswers(int userid, int answerId, string answers)
        {
            try
            {
                var findAnswers = await _baseaAnswersRespository.GetByIdAsync(answerId);
                if (findAnswers == null) { return new ResponseObject<DataResponseAnswer>(400, "Not found", null); }
                if (userid != findAnswers.UserId)
                {
                    return new ResponseObject<DataResponseAnswer>(400, " không có quyền sửa answers", null);
                }
                findAnswers.Answer = answers;
                findAnswers.UpdateTime = DateTime.Now;
                await _baseaAnswersRespository.UpdateAsync(findAnswers);

                var newResponse = new DataResponseAnswer();
                newResponse.Answer = findAnswers.Answer;
                newResponse.QuestionId = findAnswers.QuestionId;
                newResponse.UserId = findAnswers.UserId;
                newResponse.CreateTime = findAnswers.CreateTime;
                newResponse.Id = findAnswers.Id;
                newResponse.UpdateTime = findAnswers.UpdateTime;
                return new ResponseObject<DataResponseAnswer>(200, "Update answers thanh cong", null);
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseAnswer>(400, ex.Message, null);
            }
        }
    }
}
