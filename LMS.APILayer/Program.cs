using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace LMS.APILayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                var configuration = configurationBuilder.Build();
                string clientId = configuration["AzureKeyVault:ClientId"];
                string clientSecret = configuration["AzureKeyVault:ClientSecret"];
                string vaultName = configuration["AzureKeyVault:VaultName"];
                configurationBuilder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net/", clientId, clientSecret);
            })
            .UseApplicationInsights()
                .UseStartup<Startup>();

    }
}
