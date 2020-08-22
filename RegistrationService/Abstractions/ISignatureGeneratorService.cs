using System.Threading.Tasks;

namespace RegistrationService.Abstractions
{
    public interface ISignatureGeneratorService
    {
        public Task<string> SignAsync(string content);
    }
}