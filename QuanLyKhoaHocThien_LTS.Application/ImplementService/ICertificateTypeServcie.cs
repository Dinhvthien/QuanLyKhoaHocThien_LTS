using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public interface ICertificateTypeServcie
    {
        Task<bool> CreateCertificatetype(string request);
        Task<ResponseObject<List<CertificateType>>> GetCertificatetype();
        Task<ResponseObject<CertificateType>> GetCertificatetypeById(int id);
        Task<ResponseObject<CertificateType>> UpdateCertificatetype(int id, string request);
        Task<bool> DeleteCertificatetype(int id);
    }
}
