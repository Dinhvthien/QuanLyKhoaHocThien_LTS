using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_RegisterStudy
    {
        public int CourseId { get; set; }

        public int CurrentSubjectId { get; set; }
    }
}
