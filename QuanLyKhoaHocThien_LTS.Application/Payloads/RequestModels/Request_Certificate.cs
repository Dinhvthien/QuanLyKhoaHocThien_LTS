using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_Certificate
    {
        public int CertificateTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }
}
