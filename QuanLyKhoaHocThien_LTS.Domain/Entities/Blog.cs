using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }

        public string Content { get; set; } = default!;

        public string Title { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User Creator { get; set; } = default!;

        public ICollection<CommentBlog> CommentBlogs { get; set; } = default!;

        public ICollection<LikeBlog> LikeBlogs { get; set; } = default!;
    }
}
