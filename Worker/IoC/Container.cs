using Common;
using Common.Configurations;
using Common.Helpers;
using Common.Slack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Templates;

namespace Worker.IoC;

public class Container : BaseDependencyContainer<Config>
{
    private readonly IConfiguration _config;

    public Container(IConfiguration config) : base(new Config(config))
    {
        _config = config;
    }

    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        // Config
        serviceCollection
            .AddSingleton(_config)
            .AddSingleton<IConfig>(Config)
            .AddSingleton(Config);

        // General infrastructure
        serviceCollection.AddHttpClient();
        serviceCollection
            .AddSingleton<ILambdaConfig>(Config);
        serviceCollection.AddLogging(builder =>
        {
            builder.ClearProviders();
            var template =
                "{ { Timestamp: @t, Message: @m, Level: @l, Exception: @x, ..@p, MessageTemplate: @mt} }\n";
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("FeatureVersion v1", "<not_set>")
                .WriteTo.Console(new ExpressionTemplate(template))
                .CreateLogger();
            _ = builder.AddSerilog(logger, dispose: true);
        });
        
        // Core services
        serviceCollection.AddSingleton<SlackNotifier>();
    }

    protected override IConfigurationRoot CreateConfiguration() => Helpers.CreateConfiguration();
}