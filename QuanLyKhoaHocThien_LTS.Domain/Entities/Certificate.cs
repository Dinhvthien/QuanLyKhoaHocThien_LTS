using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Certificate
    {
        public int Id { get; set; }

        public int CertificateTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;

        public CertificateType CertificateType { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
    }
}
