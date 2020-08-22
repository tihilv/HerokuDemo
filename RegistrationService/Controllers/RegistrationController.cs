using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistrationService.Abstractions;
using RegistrationService.Model;

namespace RegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly ILicenseValidatorService _licenseValidatorService;
        private readonly ISignatureGeneratorService _signatureGeneratorService;

        public RegistrationController(ILogger<RegistrationController> logger, ILicenseValidatorService licenseValidatorService, ISignatureGeneratorService signatureGeneratorService)
        {
            _logger = logger;
            _licenseValidatorService = licenseValidatorService;
            _signatureGeneratorService = signatureGeneratorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationInfo registrationInfo)
        {
            _logger.LogTrace($"license request received: {registrationInfo.CompanyName}, {registrationInfo.ContactPerson}, {registrationInfo.Email}, {registrationInfo.Address}, {registrationInfo.LicenseKey}");

            if (!_licenseValidatorService.IsLicenseInfoValid(registrationInfo))
            {
                _logger.LogTrace($"License key is invalid");
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            try
            {
                string signature = await _signatureGeneratorService.SignAsync(registrationInfo.ToString());
                return Ok(new LicenseSignature() {LicenseKey = registrationInfo.LicenseKey, Signature = signature});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to sign the license");
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}