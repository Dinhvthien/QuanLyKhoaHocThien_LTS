using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_newPassword
    {
        public string confirmCode { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }

    }
}
