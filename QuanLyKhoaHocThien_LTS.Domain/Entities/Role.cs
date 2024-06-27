using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleCode { get; set; } = default!;

        public string RoleName { get; set; } = default!;

        public ICollection<Permission> Permissions { get; set; } = default!;
    }
}
