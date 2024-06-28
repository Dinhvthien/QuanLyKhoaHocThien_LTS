using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponse_StudentProgress
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int PercentComplete { get; set; }
        public bool IsFinished { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime? DoneTime { get; set; }
    }
}
