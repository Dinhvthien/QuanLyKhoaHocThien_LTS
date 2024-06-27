using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class LikeBlog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public bool Unlike { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User User { get; set; } = default!;

        public Blog Blog { get; set; } = default!;
    }
}
