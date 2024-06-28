using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseAnswer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Answer { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
