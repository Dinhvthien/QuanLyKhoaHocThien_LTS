using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseMakequestion
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;

        public int NumberOfAnswers { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
