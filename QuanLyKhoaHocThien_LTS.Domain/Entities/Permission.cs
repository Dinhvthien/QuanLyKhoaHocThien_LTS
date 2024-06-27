using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; } = default!;

        public Role Role { get; set; } = default!;
    }
}
