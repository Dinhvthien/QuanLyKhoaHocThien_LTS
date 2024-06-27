using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.IRespositories
{
    public interface IUserRespository
    {
        Task<User> GetUserByName(string name);

        Task<User> GetUserByEmail(string email);

        Task AddRoleToUSerAsync(User user, List<string> roles);
        Task<IEnumerable<string>> GetRolesOfUserAsync(User user);

    }
}
