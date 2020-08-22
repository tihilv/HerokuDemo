using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SignatureGenerator.Abstractions;

namespace SignatureGenerator.Services
{
    internal sealed class FakeSignatureService: ISignatureService
    {
        public String Sign(String content)
        {
            return Encrypt(GetHash(content));
        }

        private string Encrypt(Byte[] data)
        {
            // todo: The content of the hash should be encrypted by a private certificate here 

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append((data[i] ^ 117).ToString("x2"));

            return sBuilder.ToString();
        }

        private static Byte[] GetHash(string input)
        {
            var hash = SHA256.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
    
    public static class FakeSignatureServiceExtensions
    {
        public static void AddFakeSignatureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISignatureService, FakeSignatureService>();
        }
    }
}