using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.ImplementService;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateTypeController : ControllerBase
    {
        private readonly ICertificateTypeServcie _certificateService;

        public CertificateTypeController(ICertificateTypeServcie certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _certificateService.GetCertificatetype());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get(int id)
        {
            return Ok(await _certificateService.GetCertificatetypeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> createcertificate(string request)
        {
            return Ok(await _certificateService.CreateCertificatetype(request));
        }
        [HttpPut]
        public async Task<IActionResult> updatecertificate(int id, string request)
        {
            return Ok(await _certificateService.UpdateCertificatetype(id, request));
        }
        [HttpDelete]
        public async Task<IActionResult> deletecertificate(int id)
        {
            return Ok(await _certificateService.DeleteCertificatetype(id));
        }
    }
}
