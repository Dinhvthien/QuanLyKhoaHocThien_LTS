﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class CertificateType
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<Certificate> Certificates { get; set; } = default!;
    }
}