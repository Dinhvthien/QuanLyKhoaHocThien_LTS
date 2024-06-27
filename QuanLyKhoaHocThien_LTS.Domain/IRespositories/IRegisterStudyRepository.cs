using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.IRespositories
{
    public interface IRegisterStudyRepository
    {
        Task UpdateActiveRgisterStudy(int registerStudy);

    }
}
