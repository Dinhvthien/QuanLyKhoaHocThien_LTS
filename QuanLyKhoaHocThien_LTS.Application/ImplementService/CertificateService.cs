using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class CertificateService : ICertificateService
    {
        private readonly IBaseRespository<Certificate> _certificateService;
        private readonly IBaseRespository<CertificateType> _certificateTypeService;

        public CertificateService(IBaseRespository<Certificate> certificateService, IBaseRespository<CertificateType> certificateTypeService)
        {
            _certificateService = certificateService;
            _certificateTypeService = certificateTypeService;
        }

        public async Task<ResponseObject<CertificateResponse>> CreateCertificate(Request_Certificate request)
        {
            try
            {
                var findCertificateType = await _certificateTypeService.GetAsync(x => x.Id == request.CertificateTypeId);
                if (findCertificateType == null)
                {
                    return new ResponseObject<CertificateResponse>(400, "Loại chứng chỉ chưa có mời thêm mới", null);
                }

                var CreateCertificate = new Certificate
                {
                    Name = request.Name,
                    CertificateTypeId = request.CertificateTypeId,
                    Description = request.Description,
                    Image = request.Image
                };
                var result = await _certificateService.CreateAsync(CreateCertificate);
                // response 
                CertificateResponse certificateResponse = new CertificateResponse();
                certificateResponse.Id = result.Id;
                certificateResponse.Name = result.Name;
                certificateResponse.CertificateTypeId = result.CertificateTypeId;
                certificateResponse.Description = result.Description;
                certificateResponse.Image = result.Image;
                return new ResponseObject<CertificateResponse>(200, "Tạo Chứng chỉ mới thành công", certificateResponse);
            }
            catch (Exception ex)
            {

                return new ResponseObject<CertificateResponse>(400, ex.Message, null);
            }
        }

        public async Task<bool> DeleteCertificate(int id)
        {
            try
            {
                var findCertificate = await _certificateService.GetByIdAsync(id);
                if (findCertificate == null)
                {
                    return false;
                }
                await _certificateService.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<ResponseObject<List<CertificateResponse>>> GetCertificate()
        {
            try
            {
                var getallCertificate = await _certificateService.GetAllAsync();
                if (getallCertificate == null)
                {
                    return new ResponseObject<List<CertificateResponse>>(400, "Danh sách trống", null);
                }
                List<CertificateResponse> certificates = new List<CertificateResponse>();
                foreach (var certificate in getallCertificate)
                {
                    CertificateResponse certificateResponse = new CertificateResponse();
                    certificateResponse.Id = certificate.Id;
                    certificateResponse.Name = certificate.Name;
                    certificateResponse.CertificateTypeId = certificate.CertificateTypeId;
                    certificateResponse.Description = certificate.Description;
                    certificateResponse.Image = certificate.Image;
                    certificates.Add(certificateResponse);
                }
                return new ResponseObject<List<CertificateResponse>>(200, "Danh sách trảng", certificates);
            }
            catch (Exception ex)
            {
                return new ResponseObject<List<CertificateResponse>>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<CertificateResponse>> GetCertificateById(int id)
        {
            try
            {
                var findCertificate = await _certificateService.GetByIdAsync(id);
                if (findCertificate == null)
                {
                    return new ResponseObject<CertificateResponse>(200, "Tạo Chứng chỉ mới thành công", null);
                }
                CertificateResponse certificateResponse = new CertificateResponse();
                certificateResponse.Id = findCertificate.Id;
                certificateResponse.Name = findCertificate.Name;
                certificateResponse.CertificateTypeId = findCertificate.CertificateTypeId;
                certificateResponse.Description = findCertificate.Description;
                certificateResponse.Image = findCertificate.Image;
                return new ResponseObject<CertificateResponse>(200, "Danh sách trảng", certificateResponse);
            }
            catch (Exception ex)
            {
                return new ResponseObject<CertificateResponse>(400, ex.Message, null);
            }
        }

        public async Task<ResponseObject<CertificateResponse>> UpdateCertificate(int id, Request_Certificate request)
        {
            try
            {
                var findCertificate = await _certificateService.GetByIdAsync(id);
                if (findCertificate == null)
                {
                    return new ResponseObject<CertificateResponse>(200, "Khóng tìm thấy chứng chỉ", null);
                }
                findCertificate.Name = request.Name;
                findCertificate.CertificateTypeId = request.CertificateTypeId;
                findCertificate.Description = request.Description;
                findCertificate.Image = request.Image;
                await _certificateService.UpdateAsync(findCertificate);

                CertificateResponse certificateResponse = new CertificateResponse();
                certificateResponse.Id = findCertificate.Id;
                certificateResponse.Name = findCertificate.Name;
                certificateResponse.CertificateTypeId = findCertificate.CertificateTypeId;
                certificateResponse.Description = findCertificate.Description;
                certificateResponse.Image = findCertificate.Image;
                return new ResponseObject<CertificateResponse>(200, "Update chứng chỉ thành công", certificateResponse);

            }
            catch (Exception ex)
            {
                return new ResponseObject<CertificateResponse>(400, ex.Message, null);
            }
        }
    }
}
