using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.ValidateInput
{
    public class ValidateInput
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidVietnamPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy cho số điện thoại Việt Nam
            string pattern = @"^(03|05|07|08|09)\d{8}$";

            // Kiểm tra tính hợp lệ của số điện thoại
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
