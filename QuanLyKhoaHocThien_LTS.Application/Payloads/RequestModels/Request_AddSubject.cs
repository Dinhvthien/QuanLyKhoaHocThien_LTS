using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_AddSubject
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
