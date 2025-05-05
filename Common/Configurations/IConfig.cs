namespace Common.Configurations;

public interface IConfig : ILambdaConfig
{
    /// <summary>
    /// Gets the "short" environment name (aka <c>env_sid</c>).
    /// </summary>
    string ShortEnvironment { get; }
    string ConnectionString { get; }
}