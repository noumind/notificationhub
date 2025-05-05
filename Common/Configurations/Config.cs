using Microsoft.Extensions.Configuration;

namespace Common.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class Config : BaseLambdaConfig, IConfig
{
    public string ShortEnvironment => Configuration["ShortEnvironment"];

    public string ConnectionString => Configuration["ConnectionString"];

    public IConfiguration ConfigurationSettings => Configuration;

    public Config(IConfiguration config = null) : base(config) { }
}