using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/certificates")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _certificateService.GetCertificate());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get(int id)
        {
            return Ok(await _certificateService.GetCertificateById(id));
        }

        [HttpPost]
        public async Task<IActionResult> createcertificate([FromBody] Request_Certificate request)
        {
            return Ok(await _certificateService.CreateCertificate(request));
        }
        [HttpPut]
        public async Task<IActionResult> updatecertificate(int id, [FromBody] Request_Certificate request)
        {
            return Ok(await _certificateService.UpdateCertificate(id,request));
        }
        [HttpDelete]
        public async Task<IActionResult> deletecertificate(int id)
        {
            return Ok(await _certificateService.DeleteCertificate(id));
        }
    }
}
