using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_UpdateUser
    {
        public string Username { get; set; } = default!;
        public string Fullname { get; set; } = default!;

        public string? Avatar { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;


        public DateTime? DateOfBirth { get; set; }

        public bool IsActive { get; set; }
    }
}
