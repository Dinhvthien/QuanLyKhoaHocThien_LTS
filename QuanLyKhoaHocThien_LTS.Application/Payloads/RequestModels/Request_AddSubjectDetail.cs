﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_AddSubjectDetail
    {
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = default!;
        public string LinkVideo { get; set; } = default!;
    }
}
