using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IConfirmFinshSubjectDetail
    {
        Task<string> MarkPracticeAsCompleted(int userId, int practiceId);
        Task UpdatePercentComplete(int userId, int registerStudyId);
    }
}
