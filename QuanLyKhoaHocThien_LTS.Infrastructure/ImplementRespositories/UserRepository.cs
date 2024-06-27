using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.ImplementRespositories
{
    public class UserRepository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        #region Xu li chuoi
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }
        private async Task<bool> IsStringInListAsync(string str, List<string> list)
        {
            if (str == null) { throw new ArgumentNullException(nameof(str)); }
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            foreach (var item in list)
            {
                if (await CompareStringAsync(str, item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        public async Task AddRoleToUSerAsync(User user, List<string> roles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            foreach (var role in roles.Distinct())
            {
                var RoleOfUser = await GetRolesOfUserAsync(user);
                if (await IsStringInListAsync(role, RoleOfUser.ToList()))
                {
                    throw new ArgumentException("Người dùng đã có quyền này r");
                }
                else
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    if (roleItem == null)
                    {
                        throw new ArgumentException("Quyền này không tìm thấy");
                    }
                    _context.Permissions.Add(new Permission() { 
                        RoleId = roleItem.Id,
                        UserId = user.Id
                    });
                   await _context.SaveChangesAsync();
                }
            }

        }

        public async Task<IEnumerable<string>> GetRolesOfUserAsync(User user)
        {
           var roles = new List<string>();
            var listRole = _context.Permissions.Where(c=>c.UserId == user.Id).AsQueryable();
            foreach (var item in listRole.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x=>x.Id == item.RoleId);
                roles.Add(role.RoleCode);
            }
            return roles.AsEnumerable();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByName(string name)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower().Equals(name.ToLower()));
            return user;
        }
    }
}
