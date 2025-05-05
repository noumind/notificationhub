using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Common.Configurations;

[ExcludeFromCodeCoverage]
public abstract class BaseLambdaConfig : ILambdaConfig
{
    protected IConfiguration Configuration { get; }

    public string Environment => Configuration["ENVIRONMENT"];

    public string HostedZone => Configuration["HOSTED_ZONE"];

    public string Feature => Configuration["FEATURE"];

    public string AwsRegion => Configuration["AWS_REGION"];

    public string FeatureVersion => Configuration["FEATURE_VERSION"];

    public string StatsdHost => Configuration["MONITORING_ADDRESS"];

    public string AwsAccountId => Configuration["AWS_ACCOUNT_ID"];

    protected BaseLambdaConfig(IConfiguration config = null)
    {
        Configuration = config ?? new ConfigurationBuilder().AddEnvironmentVariables().Build();
    }
}