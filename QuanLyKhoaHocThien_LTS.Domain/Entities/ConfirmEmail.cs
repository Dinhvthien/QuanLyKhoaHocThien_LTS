﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class ConfirmEmail
    {
        public int Id { get; set; }

        public string ConfirmCode { get; set; } = default!;

        public DateTime ExpiryTime { get; set; }

        public int UserId { get; set; }

        public bool IsConfirm { get; set; }

        public User User { get; set; } = default!;
    }
}
