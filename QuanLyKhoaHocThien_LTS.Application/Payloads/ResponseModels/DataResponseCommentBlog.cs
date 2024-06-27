using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseCommentBlog
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = default!;

        public bool Edited { get; set; }

    }
}
