using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.InterfaceServices
{
    public interface ICertificateService
    {
        Task<ResponseObject<CertificateResponse>> CreateCertificate(Request_Certificate request);
        Task<ResponseObject<List<CertificateResponse>>> GetCertificate();
        Task<ResponseObject<CertificateResponse>> GetCertificateById(int id);
        Task<ResponseObject<CertificateResponse>> UpdateCertificate(int id, Request_Certificate request);
        Task<bool> DeleteCertificate(int id);
    }
}
