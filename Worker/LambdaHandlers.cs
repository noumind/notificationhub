using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Common.Converters;
using Common.Helpers;
using Common.Models;
using Common.Slack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Worker;
using Worker.IoC;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<HttpApiJsonSerializerContext>))]

namespace Worker;

[ExcludeFromCodeCoverage]
public class LambdaHandlers
{
    private readonly Lazy<Container> _container;
    private readonly Lazy<ILogger> _logger;
    private readonly SlackNotifier _slackNotifier;
    
    public LambdaHandlers() : this(null) { }

    public LambdaHandlers(IConfigurationRoot configurationRoot)
    {
        _container = new Lazy<Container>(() => new Container(Helpers.CreateConfiguration()));
        _logger = new Lazy<ILogger>(GetService<ILogger<LambdaHandlers>>);
        _slackNotifier = GetService<SlackNotifier>();
    }

    public async Task<APIGatewayProxyResponse> Notify(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
    {
        _logger.Value.LogInformation("{FunctionName} ran with id: {Id}", lambdaContext.FunctionName, Guid.NewGuid());
        _logger.Value.LogInformation("Received webhook: {Body}", request.Body);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new CustomDateTimeConverter() }
        };
        
        var webhookEvent = JsonSerializer.Deserialize<JiraWebhookPayload>(request.Body, options) ?? new JiraWebhookPayload();
        
        await _slackNotifier.SendSlackMessageAsync(webhookEvent);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = "Slack notified."
        };
    }
    
    private T GetService<T>() where T : notnull => _container.Value.GetRequiredService<T>();
}