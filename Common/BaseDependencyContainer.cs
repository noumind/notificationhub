using Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public abstract class BaseDependencyContainer<T> : IServiceProvider, IDisposable
    where T : ILambdaConfig
{
    protected readonly T Config;
    private IServiceProvider _serviceProvider;

    protected BaseDependencyContainer(T config) => Config = config;

    public object GetService(Type serviceType)
    {
        if (_serviceProvider == null)
            BuildServiceProvider();

        return _serviceProvider.GetService(serviceType);
    }

    protected abstract void ConfigureServices(IServiceCollection serviceCollection);

    protected virtual IServiceProvider BuildServiceProvider(IServiceCollection serviceCollection)
        => serviceCollection.BuildServiceProvider();

    protected abstract IConfigurationRoot CreateConfiguration();

    private void BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(s => CreateConfiguration());

        ConfigureServices(serviceCollection);

        _serviceProvider = BuildServiceProvider(serviceCollection);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            (_serviceProvider as IDisposable)?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}