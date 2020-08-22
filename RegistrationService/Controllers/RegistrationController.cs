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
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly ILicenseValidatorService _licenseValidatorService;

        public RegistrationController(ILogger<RegistrationController> logger, ILicenseValidatorService licenseValidatorService)
        {
            _logger = logger;
            _licenseValidatorService = licenseValidatorService;
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

            
            string signature = "";
            return Ok(new LicenseSignature() {LicenseKey = registrationInfo.LicenseKey, Signature = signature});
        }
    }
}