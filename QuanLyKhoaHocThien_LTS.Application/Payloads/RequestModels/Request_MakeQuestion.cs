using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_MakeQuestion
    {
        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;
    }
}
