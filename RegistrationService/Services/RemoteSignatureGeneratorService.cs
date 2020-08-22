using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationService.Abstractions;

namespace RegistrationService.Services
{
    internal sealed class RemoteSignatureGeneratorService: ISignatureGeneratorService
    {
        private readonly HubConnection _connection;
        
        public RemoteSignatureGeneratorService(IConfiguration config)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(config.GetValue<String>("LicenseGeneratorUrl"))
                .WithAutomaticReconnect()
                .Build();
        }

        private async Task StartConnectionIfNeededAsync()
        {
            while (_connection.State != HubConnectionState.Connected)
            {
                try
                {
                    await _connection.StartAsync();
                    return;
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
        
        
        public async Task<string> SignAsync(String content)
        {
            await StartConnectionIfNeededAsync();
            
            var response = await _connection.InvokeAsync<string>("Perform", content);
            return response;
        }
    }
    
    public static class RemoteSignatureGeneratorExtensions
    {
        public static void AddRemoteSignatureGenerator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISignatureGeneratorService, RemoteSignatureGeneratorService>();
        }
    }

}