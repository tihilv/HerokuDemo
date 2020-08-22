using System;
using Microsoft.Extensions.DependencyInjection;
using RegistrationService.Abstractions;
using RegistrationService.Model;

namespace RegistrationService.Services
{
    internal sealed class FakeLicenseValidatorService: ILicenseValidatorService
    {
        public Boolean IsLicenseInfoValid(RegistrationInfo registrationInfo)
        {
            return registrationInfo.LicenseKey.StartsWith($"{registrationInfo.CompanyName[0]}{registrationInfo.ContactPerson[0]}{registrationInfo.Email[0]}{registrationInfo.Address[0]}") && registrationInfo.LicenseKey.EndsWith("1");
        }
    }

    public static class FakeLicenseValidatorExtensions
    {
        public static void AddLicenseValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILicenseValidatorService, FakeLicenseValidatorService>();
        }
    }
}