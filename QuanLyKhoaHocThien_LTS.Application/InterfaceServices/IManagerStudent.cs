using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface IManagerStudent
    {
        Task<List<User>> GetAllStudentsAsync();
        Task<List<Bill>> GetStudentBillsAsync(int studentId);
        /*ask<List<LearningProgress>> GetStudentLearningProgressAsync(int studentId)*/
    }
}
