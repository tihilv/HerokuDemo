using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RegistrationService.Abstractions;

namespace RegistrationService.Services
{
    public class RemoteSignatureGeneratorService: ISignatureGeneratorService
    {
        public Task<string> SignAsync(String content)
        {
            throw new NotImplementedException();
        }
    }
    
    public static class RemoteSignatureGeneratorExtensions
    {
        public static void AddRemoteSignatureGenerator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISignatureGeneratorService, RemoteSignatureGeneratorService>();
        }
    }

}