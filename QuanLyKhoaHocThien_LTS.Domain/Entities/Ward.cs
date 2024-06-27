using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Ward
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;

        public District District { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
    }
}
