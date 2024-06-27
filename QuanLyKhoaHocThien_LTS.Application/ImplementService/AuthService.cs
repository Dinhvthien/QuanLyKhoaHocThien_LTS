using AutoMapper;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using QuanLyKhoaHocThien_LTS.Application.HandleEmail;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels.ReponseUsers;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using QuanLyKhoaHocThien_LTS.Domain.ValidateInput;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRespository<User> _baseRespository;
        private readonly UserConverter _userConverter;
        private readonly IConfiguration _configuration;
        private readonly IUserRespository _userRespository;
        private readonly IEmailService _emailService;
        private readonly IBaseRespository<ConfirmEmail> _baseRespositoryConfirmEmail;
        private readonly IBaseRespository<Permission> _baseRespositoryPermission;
        private readonly IBaseRespository<Role> _baseRespositoryRole;
        private readonly IBaseRespository<RefreshToken> _baseRespositoryRefreshToken;


        public AuthService(IBaseRespository<User> baseRespository,
            UserConverter userConverter,
            IConfiguration configuration,
            IUserRespository userRespository,
            IEmailService emailService,
            IBaseRespository<ConfirmEmail> baseRespositoryConfirmEmail,
            IBaseRespository<Permission> baseRespositoryPermission,
            IBaseRespository<Role> baseRespositoryRole,
            IBaseRespository<RefreshToken> baseRespositoryRefreshToken
            )
        {
            _baseRespository = baseRespository;
            _userConverter = userConverter;
            _configuration = configuration;
            _userRespository = userRespository;
            _emailService = emailService;
            _baseRespositoryConfirmEmail = baseRespositoryConfirmEmail;
            _baseRespositoryPermission = baseRespositoryPermission;
            _baseRespositoryRole = baseRespositoryRole;
            _baseRespositoryRefreshToken = baseRespositoryRefreshToken;
        }
        public AuthService()
        {

        }
        public async Task<ResponseObject<DataResponseUser>> RegisterUser(Request_Register request)
        {
            try
            {
                if (!ValidateInput.IsValidEmail(request.Email))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = 400,
                        Message = "Email is not valid",
                        Data = null
                    };
                }
                if (await _userRespository.GetUserByEmail(request.Email) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = 400,
                        Message = "Email is already exists",
                        Data = null
                    };
                }
                var user = new User();
                user.Avatar = "https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg";
                user.IsActive = true;
                user.CreateTime = DateTime.Now;
                user.FullName = request.FullName;
                user.Username = request.Username;
                user.Email = request.Email;
                user.Password = request.Password;
                user.DateOfBirth = request.DateOfBirth;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.UserStatus = Domain.UserStatus.UnActive;
                await _baseRespository.CreateAsync(user);
                await _userRespository.AddRoleToUSerAsync(user, new List<string> { "User" });
                ConfirmEmail confirmEmail = new ConfirmEmail();
                confirmEmail.UserId = user.Id;
                confirmEmail.ConfirmCode = GenerateCode();
                confirmEmail.ExpiryTime = DateTime.Now.AddMinutes(1);
                confirmEmail.IsConfirm = false;
                await _baseRespositoryConfirmEmail.CreateAsync(confirmEmail);
                var message = new EmailMessage(new string[] { request.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận: {confirmEmail.ConfirmCode}");
                var responseMessage = _emailService.SendEmail(message);
                return new ResponseObject<DataResponseUser>
                {
                    Status = 200,
                    Message = "Register success! Vui lòng nhập mã xác nhận để kích hoạt tài khoản",
                    Data = _userConverter.EntityToDTO(user)
                };
            }
            catch (Exception ex)
            {

                return new ResponseObject<DataResponseUser>
                {
                    Status = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        private static string GenerateCode()
        {
            // Tạo một đối tượng Random
            Random random = new Random();

            // Tạo ra một số ngẫu nhiên gồm 6 chữ số
            int randomNumber = random.Next(100000, 1000000);

            return randomNumber.ToString();
        }

        public async Task<string> ConfirmRegisterUser(string confirmCode)
        {
            try
            {
                var code = await _baseRespositoryConfirmEmail.GetAsync(x => x.ConfirmCode.Equals(confirmCode));
                if (code == null)
                {
                    return "Mã xác nhận không hợp lệ";
                }
                if (code.ExpiryTime < DateTime.Now)
                {
                    await _baseRespositoryConfirmEmail.DeleteAsync(code.Id);

                    ConfirmEmail confirmEmail = new ConfirmEmail();
                    confirmEmail.UserId = code.UserId;
                    confirmEmail.ConfirmCode = GenerateCode();
                    confirmEmail.ExpiryTime = DateTime.Now.AddMinutes(2);
                    confirmEmail.IsConfirm = false;
                    await _baseRespositoryConfirmEmail.CreateAsync(confirmEmail);
                    var findUserEmail = await _baseRespository.GetByIdAsync(code.UserId);
                    var message = new EmailMessage(new string[] { findUserEmail.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận: {confirmEmail.ConfirmCode}");
                    var responseMessage = _emailService.SendEmail(message);
                    return "Thời gian xác nhận đã hết hạn! Mã xác nhận đã được gửi lại @@";
                }
                var User = await _baseRespository.GetAsync(x => x.Id == code.UserId);
                User.UserStatus = Domain.UserStatus.Active;
                code.IsConfirm = true;
                await _baseRespository.UpdateAsync(User);
                return "Kiểm tra xác nhận thanh cong";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        #region Private Methods
        private JwtSecurityToken GetJwtToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            _ = int.TryParse(_configuration["JWT:TokenValidInHours"], out int TokenValidInHours);
            var expiresUTC = DateTime.Now.AddHours(TokenValidInHours);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expiresUTC,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private string GenerateJwtToken()
        {
            var RandomNuber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(RandomNuber);
            return Convert.ToBase64String(RandomNuber);

        }
        #endregion
        public async Task<ResponseObject<DataReponseLogin>> GetJWTTokenAsync(User user)
        {
            var permission = await _baseRespositoryPermission.GetAllAsync(c => c.UserId == user.Id);

            var roles = await _baseRespositoryRole.GetAllAsync();
            var authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("UserName", user.FullName.ToString()),
                    new Claim("Email", user.Email.ToString()),
                };

            foreach (var item in permission)
            {
                foreach (var role in roles)
                {
                    if (role.Id == item.RoleId)
                    {
                        authClaims.Add(new Claim("Permission", role.RoleName.ToString()));
                    }
                }
            }

            var UserRole = await _userRespository.GetRolesOfUserAsync(user);
            foreach (var item in UserRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var jwtToken = GetJwtToken(authClaims);
            var refreshToken = GenerateJwtToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidInHours"], out int RefreshTokenValidInHours);
            RefreshToken CreeatrefreshToken = new RefreshToken
            {
                ExpiryTime = DateTime.Now.AddHours(RefreshTokenValidInHours),
                UserId = user.Id,
                Token = refreshToken
            };
            await _baseRespositoryRefreshToken.CreateAsync(CreeatrefreshToken);
            return new ResponseObject<DataReponseLogin> { Status = 200, Message = "Login success", Data = new DataReponseLogin { accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken), refreshToken = refreshToken } };
        }

        public async Task<ResponseObject<DataReponseLogin>> Login(Request_Login request)
        {
            var User = await _baseRespository.GetAsync(c => c.Username.Equals(request.UserName.Trim()));
            if (User == null)
            {
                return new ResponseObject<DataReponseLogin>(
                    400,
                    "Ten dang nhap khong ton tai", null
                    );
            }
            if (User.UserStatus.ToString().Equals(Domain.UserStatus.UnActive.ToString()))
            {
                return new ResponseObject<DataReponseLogin>(
                  400,
                  "Tài khoản của bạn chưa kích hoạt", null
                  );
            }
            if (User.UserStatus.ToString().Equals(Domain.UserStatus.Block.ToString()))
            {
                return new ResponseObject<DataReponseLogin>(
                  400,
                  "Tài khoản của bạn đã bị khóa", null
                  );
            }
            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, User.Password);
            if (!checkPassword)
            {
                return new ResponseObject<DataReponseLogin>(
                 400,
                 "Mật khẩu không chính xác", null
                 );
            }

            return new ResponseObject<DataReponseLogin>(
             200,
             "Thành công",
            new DataReponseLogin { accessToken = GetJWTTokenAsync(User).Result.Data.accessToken, refreshToken = GetJWTTokenAsync(User).Result.Data.refreshToken });
        }

        public async Task<ResponseObject<DataResponseUser>> ChangePassword(int id, Request_ChangePassword request)
        {
            try
            {
                var User = await _baseRespository.GetByIdAsync(id);
                bool checkPassword = BCrypt.Net.BCrypt.Verify(request.OldPassword, User.Password);
                if (!checkPassword)
                {
                    return new ResponseObject<DataResponseUser>(
                    400,
                    "Mật khẩu này không chính xác", null
                    );
                }

                if (request.NewPassword != request.ConfirmPassword)
                {
                    return new ResponseObject<DataResponseUser>(
                    400,
                    "Xác nhận mật không chính xác", null
                    );
                }
                User.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                User.UpdateTime = DateTime.Now;
                await _baseRespository.UpdateAsync(User);
                return new ResponseObject<DataResponseUser>(
                200,
                "Thay đổi mật khẩu thanh cách",
                _userConverter.EntityToDTO(User)
                );
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>(400, ex.Message, null);
            }
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRespository.GetUserByEmail(email);
                if (user == null)
                {
                    return "Email không tồn tại trong hệ thống";
                }
                //var listConfirmCodeOld = await _baseRespositoryConfirmEmail.GetAllAsync(x => x.UserId == user.Id);
                //if (listConfirmCodeOld.ToList().Count > 0)
                //{
                //    foreach (var item in listConfirmCodeOld)
                //    {
                //        await _baseRespositoryConfirmEmail.DeleteAsync(item.Id);
                //    }
                //}

                var conFirmEmail = new ConfirmEmail();
                conFirmEmail.IsConfirm = false;
                conFirmEmail.ConfirmCode = GenerateCode();
                conFirmEmail.ExpiryTime = DateTime.Now.AddMinutes(1);
                conFirmEmail.UserId = user.Id;

                await _baseRespositoryConfirmEmail.CreateAsync(conFirmEmail);
                var message = new EmailMessage(new string[] { user.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận: {conFirmEmail.ConfirmCode}");
                var responseMessage = _emailService.SendEmail(message);
                return "Gửi mã xác nhận thành công vui lòng kiểm tra email";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> ConfirmForgotPassword(Request_newPassword request)
        {
            try
            {
                var confirmEmail = await _baseRespositoryConfirmEmail.GetAsync(x => x.ConfirmCode.Equals(request.confirmCode));
                if (confirmEmail == null)
                {
                    return "Mã xác nhận không hợp lệ";
                }
                if (confirmEmail.ExpiryTime == DateTime.Now)
                {
                    return "Mã xác nhận hết hiệu lực";
                }
                if (request.newPassword != request.confirmPassword)
                {
                    return "Password confirm sai rồi";
                }
                var user = await _baseRespository.GetAsync(x => x.Id == confirmEmail.UserId);
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
                await _baseRespository.UpdateAsync(user);
                return "Tạo mật khẩu mới thành công";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<ResponseObject<DataResponseUser>> UpdateInfoUser(int userId, Request_UpdateUser request)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ResponseObject<DataResponseUser>(
                    400,
                    "Bạn chưa đăng nhập", null
                    );
                }
                var user = await _baseRespository.GetAsync(x => x.Id == userId);
                if (user == null)
                {
                    return new ResponseObject<DataResponseUser>(
                    400,
                    "User không tồn tại", null
                    );
                }
                user.FullName = request.Fullname;
                user.Username = request.Username;
                user.UpdateTime = DateTime.Now;
                user.Email = request.Email;
                user.IsActive = request.IsActive;
                user.DateOfBirth = request.DateOfBirth;
                user.Avatar = request.Avatar;
                user.Address = request.Address;
                await _baseRespository.UpdateAsync(user);
                return new ResponseObject<DataResponseUser>(
                200,
                "Cập nhập thành công",
                _userConverter.EntityToDTO(user)
                );
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<DataResponseUser>> GetUserbyId(int userId)
        {
            try
            {
                var getUserbyid = await _baseRespository.GetByIdAsync(userId);
                return new ResponseObject<DataResponseUser>(
                    200,
                    "Thông tin user",
                    _userConverter.EntityToDTO(getUserbyid)
                    );
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>(400,ex.Message, null);
            }
        }

        public async Task<string> LockUser(int userId)
        {
            try
            {
                var getUserbyid = await _baseRespository.GetByIdAsync(userId);
                if (getUserbyid == null) { return "User không tồn tại"; }
                getUserbyid.UserStatus = Domain.UserStatus.Block;
                await _baseRespository.UpdateAsync(getUserbyid);
                return "Khóa user thàng công";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> UnlockUser(int userId)
        {
            try
            {
                var getUserbyid = await _baseRespository.GetByIdAsync(userId);
                if (getUserbyid == null) { return "User không tồn tại"; }
                getUserbyid.UserStatus = Domain.UserStatus.Active;
                await _baseRespository.UpdateAsync(getUserbyid);
                return "Mở khóa user thàng công";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
