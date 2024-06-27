using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Responses
{
    public static class ResponseMessage
    {
        public static string getEmailSuccessMessage(string email)
        {
            return $"Email đã được gửi đến {email}";
        }
    }
}
