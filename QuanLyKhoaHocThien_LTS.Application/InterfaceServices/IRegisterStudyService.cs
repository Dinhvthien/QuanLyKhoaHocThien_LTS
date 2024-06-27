using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IRegisterStudyService
    {
        Task<string> RegisterStudy(int userId, Request_RegisterStudy RgisStudy);
    }
}
