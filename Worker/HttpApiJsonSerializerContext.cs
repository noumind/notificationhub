using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.SQSEvents;
using Common.Models;

namespace Worker;

/// <summary>
/// Enables performance improvements by using source-generators to precompile the
/// serialization code.
/// </summary>
/// <see href="https://aws.amazon.com/blogs/compute/introducing-the-net-8-runtime-for-aws-lambda/"/>
[JsonSerializable(typeof(SQSEvent))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(Guid?))]
[JsonSerializable(typeof(NotificationModel))]
[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
[JsonSerializable(typeof(JiraWebhookPayload))]
public partial class HttpApiJsonSerializerContext : JsonSerializerContext
{
    
}