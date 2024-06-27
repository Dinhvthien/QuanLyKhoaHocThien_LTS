using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.ImplementRespositories
{
    public class RegisterStudyRepository : IRegisterStudyRepository
    {
        private readonly ApplicationDbContext _context;
        public RegisterStudyRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task UpdateActiveRgisterStudy(int registerStudy)
        {
			try
			{
				var findRegisterStudy =await _context.RegisterStudys.FindAsync(registerStudy);
                if (findRegisterStudy != null)
                {
                    findRegisterStudy.IsActive = true; 
                    await _context.SaveChangesAsync();
                }
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
