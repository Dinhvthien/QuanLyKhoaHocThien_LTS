using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class ProgramingLanguage
    {
        public int Id { get; set; }

        public string LanguageName { get; set; } = default!;

        public ICollection<Practice> Practices { get; set; } = default!;

        public ICollection<TestCase> TestCases { get; set; } = default!;
    }
}
