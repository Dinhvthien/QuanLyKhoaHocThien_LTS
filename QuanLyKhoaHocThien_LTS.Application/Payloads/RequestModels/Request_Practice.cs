using QuanLyKhoaHocThien_LTS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_Practice
    {
        public int SubjectDetailId { get; set; }

        public Level Level { get; set; }

        public string PracticeCode { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Topic { get; set; } = default!;

        public string ExpectOutput { get; set; } = default!;

        public int LanguageProgrammingId { get; set; }

        public bool IsRequired { get; set; }

        public bool IsDeleted { get; set; }

        public double MediumScore { get; set; }
    }
}
