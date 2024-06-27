using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels
{
    public class Request_Register
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }

    }
}
