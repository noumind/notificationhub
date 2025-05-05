using System.Text;
using System.Text.Json;
using Common.Models;
using Microsoft.Extensions.Configuration;

namespace Common.Slack;

public class SlackNotifier
{
    private readonly HttpClient _httpClient;
    private readonly string _slackWebhookUrl;

    public SlackNotifier(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _slackWebhookUrl = configuration["Slack:WebhookUrl"];
    }

    public async Task SendSlackMessageAsync(JiraWebhookPayload payload)
    {
        Console.WriteLine("Slack webhook: {0}", _slackWebhookUrl);
        var slackMessage = new
        {
            text = BuildMessage(payload)
        };
        
        var jsonPayload = JsonSerializer.Serialize(slackMessage);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_slackWebhookUrl, content);
        response.EnsureSuccessStatusCode(); // Throws if not 2xx
    }
    
    private string BuildMessage(JiraWebhookPayload payload)
    {
        var message = new StringBuilder();

        // Adding a separator for clarity
        message.AppendLine("==============================================");
        message.AppendLine($"🌟 **Webhook Event**: {payload.WebhookEvent ?? "N/A"}");
        message.AppendLine($"⏰ **Timestamp**: {DateTimeOffset.FromUnixTimeMilliseconds(payload.Timestamp):yyyy-MM-dd HH:mm:ss}");
        message.AppendLine("==============================================");

        // Comment Details
        var comment = payload.Comment;
        message.AppendLine(":speech_balloon: **Comment Details**:");
        message.AppendLine($"  - **Comment by**: {comment?.Author?.DisplayName ?? "N/A"}");
        message.AppendLine($"  - **Comment Body**: {comment?.Body ?? "N/A"}");
        message.AppendLine($"  - **Created**: {comment?.Created?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"}");
        message.AppendLine($"  - **Comment URL**: {comment?.Self ?? "N/A"}");
        message.AppendLine("==============================================");
        
        // User Information
        message.AppendLine("👤 **User Information**:");
        message.AppendLine($"  - **Name**: {payload.User?.DisplayName ?? "N/A"} ({payload.User?.AccountId ?? "N/A"})");
        message.AppendLine($"  - **Account Type**: {payload.User?.AccountType ?? "N/A"}");
        message.AppendLine($"  - **Avatar**: {payload.User?.AvatarUrls?._48x48 ?? "N/A"}");
        message.AppendLine($"  - **Time Zone**: {payload.User?.TimeZone ?? "N/A"}");
        message.AppendLine("==============================================");

        // Issue Information
        message.AppendLine("🔧 **Issue Details**:");
        message.AppendLine($"  - **Issue ID**: {payload.Issue?.Id ?? "N/A"} ({payload.Issue?.Key ?? "N/A"})");
        message.AppendLine($"  - **Summary**: {payload.Issue?.Fields?.Summary ?? "N/A"}");
        message.AppendLine($"  - **Description**: {payload.Issue?.Fields?.Description ?? "N/A"}");
        message.AppendLine("==============================================");

        // Status Information
        message.AppendLine("🟢 **Status**:");
        message.AppendLine($"  - **Status**: {payload.Issue?.Fields?.Status?.Name ?? "N/A"} ({payload.Issue?.Fields?.Status?.StatusCategory?.Name ?? "N/A"})");

        // Progress Information
        message.AppendLine("📊 **Progress**:");
        message.AppendLine($"  - **Progress**: {payload.Issue?.Fields?.Progress?.Progress ?? 0} of {payload.Issue?.Fields?.Progress?.Total ?? 0}");

        // Priority Information
        message.AppendLine("⚡ **Priority**:");
        message.AppendLine($"  - **Priority**: {payload.Issue?.Fields?.Priority?.Name ?? "N/A"}");
        message.AppendLine("==============================================");

        // Creator and Reporter Information
        message.AppendLine("📝 **Creator & Reporter**:");
        message.AppendLine($"  - **Creator**: {payload.Issue?.Fields?.Creator?.DisplayName ?? "N/A"}");
        message.AppendLine($"  - **Reporter**: {payload.Issue?.Fields?.Reporter?.DisplayName ?? "N/A"}");
        message.AppendLine("==============================================");

        // Changelog (if exists)
        if (payload.Changelog?.Items?.Any() == true)
        {
            message.AppendLine("🔄 **Changelog**:");
            foreach (var item in payload.Changelog.Items)
            {
                message.AppendLine($"  - **Field**: {item.Field ?? "N/A"} | **From**: {item.FromString ?? "N/A"} | **To**: {item.ToString ?? "N/A"}");
            }
            message.AppendLine("==============================================");
        }

        // Return the beautified message
        return message.ToString();
    }
}