using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<District> Districts { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
    }
}
