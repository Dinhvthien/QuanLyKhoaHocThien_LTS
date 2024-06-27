using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseBlog
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }

        public string Content { get; set; } = default!;

        public string Title { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
