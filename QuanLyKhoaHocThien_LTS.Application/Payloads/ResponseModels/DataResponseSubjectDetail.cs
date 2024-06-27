using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseSubjectDetail
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }

    }
}
