using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers
{
    public class PracticeConverter
    {
        public DataResponsePractice EntityToDTO(Practice course)
        {
            return new DataResponsePractice
            {

                Id = course.Id,
                SubjectDetailId = course.SubjectDetailId,
                Level = course.Level,
                PracticeCode = course.PracticeCode,
                Title = course.Title,
                ExpectOutput = course.ExpectOutput,
                LanguageProgrammingId = course.LanguageProgrammingId,
                IsRequired = course.IsRequired,
                CreateTime = course.CreateTime,
                IsDeleted = course.IsDeleted,
                MediumScore = course.MediumScore
            };
        }

        public List<DataResponsePractice> EntityToDTOs(List<Practice> course)
        {
            var result = new List<DataResponsePractice>();
            foreach (var item in course) 
            {
                var Practice = EntityToDTO(item);
                result.Add(Practice);
            }
            return result;
        }
    }
}
