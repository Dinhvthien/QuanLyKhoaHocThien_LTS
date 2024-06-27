using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class CertificateTypeService : ICertificateTypeServcie
    {
        private readonly IBaseRespository<CertificateType> _certificateTypeService;
        public CertificateTypeService(IBaseRespository<CertificateType> certificateTypeService)
        {
            _certificateTypeService = certificateTypeService;
        }
        public async Task<bool> CreateCertificatetype(string request)
        {
            try
            {
                if (string.IsNullOrEmpty(request)) { return false; }
                var CreateCertificateType = new CertificateType
                {
                    Name = request
                };
                var result = await _certificateTypeService.CreateAsync(CreateCertificateType);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteCertificatetype(int id)
        {
            try
            {
                var findCertificatetype = await _certificateTypeService.GetByIdAsync(id);
                if (findCertificatetype == null)
                {
                    return false;
                }
                await _certificateTypeService.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<ResponseObject<List<CertificateType>>> GetCertificatetype()
        {
            try
            {
                var getallCertificatetype = await _certificateTypeService.GetAllAsync();
                if (getallCertificatetype == null)
                {
                    return new ResponseObject<List<CertificateType>>(400, "Danh sách trống", null);
                }
                return new ResponseObject<List<CertificateType>>(200, "Danh sách trảng", getallCertificatetype.ToList());
            }
            catch (Exception ex)
            {
                return new ResponseObject<List<CertificateType>>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<CertificateType>> GetCertificatetypeById(int id)
        {
            try
            {
                var findCertificatetype = await _certificateTypeService.GetByIdAsync(id);
                if (findCertificatetype == null)
                {
                    return new ResponseObject<CertificateType>(200, "Tạo Chứng chỉ mới thành công", null);
                }
                return new ResponseObject<CertificateType>(200, "Danh sách trảng", findCertificatetype);
            }
            catch (Exception ex)
            {
                return new ResponseObject<CertificateType>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<CertificateType>> UpdateCertificatetype(int id, string request)
        {
            try
            {
                var findCertificatetype = await _certificateTypeService.GetByIdAsync(id);
                if (findCertificatetype == null)
                {
                    return new ResponseObject<CertificateType>(200, "Khóng tìm thấy chứng chỉ", null);
                }
                findCertificatetype.Name = request;
                await _certificateTypeService.UpdateAsync(findCertificatetype);
                return new ResponseObject<CertificateType>(200, "Update chứng chỉ thành công", findCertificatetype);

            }
            catch (Exception ex)
            {
                return new ResponseObject<CertificateType>(400, ex.Message, null);

            }
        }

        public Task<ResponseObject<CertificateType>> UpdateCertificatetype(int id, Request_Certificate request)
        {
            throw new NotImplementedException();
        }
    }
}
