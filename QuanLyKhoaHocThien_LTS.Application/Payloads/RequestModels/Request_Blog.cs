using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_Blog
    {
        public string Content { get; set; } = default!;

        public string Title { get; set; } = default!;
    }
}
