using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignatureGenerator.Abstractions;

namespace SignatureGenerator.Hubs
{
    internal sealed class SignatureHub : Hub
    {
        private readonly ISignatureService _signatureService;

        public SignatureHub(ISignatureService signatureService)
        {
            _signatureService = signatureService;
        }

        public async Task<string> Perform(string message)
        {
            return _signatureService.Sign(message);
        }
    }
}