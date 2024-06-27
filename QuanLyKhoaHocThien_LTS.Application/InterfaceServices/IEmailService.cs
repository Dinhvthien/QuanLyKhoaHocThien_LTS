using QuanLyKhoaHocThien_LTS.Application.HandleEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IEmailService
    {
        string SendEmail(EmailMessage emailMs);
    }
}
