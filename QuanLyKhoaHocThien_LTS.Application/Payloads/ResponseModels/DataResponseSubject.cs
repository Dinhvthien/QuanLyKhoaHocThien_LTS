using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseSubject
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
