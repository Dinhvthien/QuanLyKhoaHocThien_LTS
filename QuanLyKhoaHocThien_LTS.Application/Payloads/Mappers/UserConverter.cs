using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers
{
    public class UserConverter
    {
        public DataResponseUser EntityToDTO(User user)
        {
            return new DataResponseUser
            {
                
                Username = user.Username,
                Avatar = user.Avatar,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                IsActive = user.IsActive,
                Id = user.Id,
                Fullname = user.FullName
            };
        }
    }
}
