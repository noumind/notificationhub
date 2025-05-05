using System.Diagnostics.CodeAnalysis;
using Common.Configurations;
using Microsoft.Extensions.Configuration;

namespace Common.Helpers;

public static class Helpers
{
    [ExcludeFromCodeCoverage]
    public static IConfigurationRoot CreateConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        var configuration = configurationBuilder.Build();
        var config = new Config(configuration);

        return configuration;
    }
}