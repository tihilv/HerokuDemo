using RegistrationService.Model;

namespace RegistrationService.Abstractions
{
    public interface ILicenseValidatorService
    {
        bool IsLicenseInfoValid(RegistrationInfo registrationInfo);
    }
}