using Azure.Identity;
using Core.IOC;
using data_processing_fn_app;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace data_processing_fn_app
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();
            builder.ConfigurationBuilder
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false);
            builder.ConfigurationBuilder.AddEnvironmentVariables();
            builder.ConfigurationBuilder.AddAzureKeyVault(new Uri("https://kv-jsp-free-trial.vault.azure.net/"), new DefaultAzureCredential());
            base.ConfigureAppConfiguration(builder);
        }

        public override async void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = "InstrumentationKey=4cb169fe-1800-4a23-846b-428b7c063c0b;IngestionEndpoint=https://centralus-2.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/";
            });
            builder.Services.RegisterCoreDependency();
            builder.Services.RegisterPersistenceDependency(builder.GetContext().Configuration);
        }
    }
}
