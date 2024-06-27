using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseUser>> RegisterUser(Request_Register request);
        Task<string> ConfirmRegisterUser(string confirmCode);
        Task<ResponseObject<DataReponseLogin>> GetJWTTokenAsync(User user);
        Task<ResponseObject<DataReponseLogin>> Login(Request_Login request);
        Task<ResponseObject<DataResponseUser>> ChangePassword(int Id, Request_ChangePassword request);
        Task<string> ForgotPassword(string email);
        Task<string> ConfirmForgotPassword(Request_newPassword request);
        Task<ResponseObject<DataResponseUser>> UpdateInfoUser(int userId, Request_UpdateUser request);
        Task<ResponseObject<DataResponseUser>> GetUserbyId(int userId);
        Task<string> LockUser(int userId);
        Task<string> UnlockUser(int userId);


    }
}
