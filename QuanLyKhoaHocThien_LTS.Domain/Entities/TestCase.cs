using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class TestCase
    {
        public int Id { get; set; }

        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public int ProgramingLanguageId { get; set; }

        public int PracticeId { get; set; }

        public ProgramingLanguage ProgramingLanguage { get; set; } = default!;

        public Practice Practice { get; set; } = default!;

        public ICollection<RunTestCase> RunTestCases { get; set; } = default!;
    }
}
