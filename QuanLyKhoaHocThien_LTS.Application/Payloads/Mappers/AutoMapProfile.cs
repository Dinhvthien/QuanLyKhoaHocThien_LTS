using AutoMapper;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers
{
    public class AutoMapProfile:Profile
    {

        public AutoMapProfile() 
        {
            CreateMap<Request_Register, User>();
            CreateMap<User, DataResponseUser>();
        }

    }
}
