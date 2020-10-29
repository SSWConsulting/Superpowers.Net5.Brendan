using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add the following
            var currentAssembly = typeof(Program).Assembly;
            builder.Services.AddFluxor(options => {
                options.ScanAssemblies(currentAssembly);
                options.UseReduxDevTools();
            });

            builder.Services.AddSingleton<HubConnection>(c => new HubConnectionBuilder()
                .WithUrl($"{builder.HostEnvironment.BaseAddress}/todohub")
                .Build()
            );

            await builder.Build().RunAsync();
        }
    }
}
