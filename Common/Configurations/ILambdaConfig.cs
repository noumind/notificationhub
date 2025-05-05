namespace Common.Configurations;

public interface ILambdaConfig
{
    string Environment { get; }

    string HostedZone { get; }

    string Feature { get; }

    string AwsRegion { get; }

    string FeatureVersion { get; }

    string StatsdHost { get; }

    string AwsAccountId { get; }
}