﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Image { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Link { get; set; } = default!;

        public bool IsSeen { get; set; }

        public DateTime CreateTime { get; set; }

        public User User { get; set; } = default!;

    }
}
