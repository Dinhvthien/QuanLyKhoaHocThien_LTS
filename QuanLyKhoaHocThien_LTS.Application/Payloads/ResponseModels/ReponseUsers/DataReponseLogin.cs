using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers
{
    public class DataReponseLogin
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
