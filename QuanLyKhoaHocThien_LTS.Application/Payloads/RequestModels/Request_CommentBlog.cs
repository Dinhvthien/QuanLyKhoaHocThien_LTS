using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_CommentBlog
    {
        public int BlogId { get; set; }

        public string Content { get; set; } = default!;
    }
}
