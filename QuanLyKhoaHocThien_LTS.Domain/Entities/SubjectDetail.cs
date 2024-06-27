using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class SubjectDetail
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }

        public Subject Subject { get; set; } = default!;

        public ICollection<MakeQuestion> MakeQuestions { get; set; } = default!;

        public ICollection<Practice> Practices { get; set; } = default!;
    }
}
