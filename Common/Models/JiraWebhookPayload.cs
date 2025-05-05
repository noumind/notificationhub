namespace Common.Models;

public class JiraWebhookPayload
{
    public long Timestamp { get; set; }
    public string WebhookEvent { get; set; }
    public string IssueEventTypeName { get; set; }
    public CommentDetails Comment { get; set; }
    public User User { get; set; }
    public Issue Issue { get; set; }
    public Changelog Changelog { get; set; }
}

public class CommentDetails
{
    public string Self { get; set; }
    public string Id { get; set; }
    public AuthorDetails Author { get; set; }
    public string Body { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public bool JsdPublic { get; set; }
}

public class AuthorDetails
{
    public string Self { get; set; }
    public string AccountId { get; set; }
    public AvatarUrls AvatarUrls { get; set; }
    public string DisplayName { get; set; }
    public bool Active { get; set; }
    public string TimeZone { get; set; }
    public string AccountType { get; set; }
}

public class User
{
    public string Self { get; set; }
    public string AccountId { get; set; }
    public AvatarUrls AvatarUrls { get; set; }
    public string DisplayName { get; set; }
    public bool Active { get; set; }
    public string TimeZone { get; set; }
    public string AccountType { get; set; }
}

public class AvatarUrls
{
    public string _48x48 { get; set; }
    public string _24x24 { get; set; }
    public string _16x16 { get; set; }
    public string _32x32 { get; set; }
}

public class Issue
{
    public string Id { get; set; }
    public string Self { get; set; }
    public string Key { get; set; }
    public IssueFields Fields { get; set; }
}

public class IssueFields
{
    public string StatusCategoryChangeDate { get; set; }
    public IssueType IssueType { get; set; }
    public List<object> Components { get; set; }
    public object TimeSpent { get; set; }
    public object TimeOriginalEstimate { get; set; }
    public string Description { get; set; }
    public Project Project { get; set; }
    public List<object> FixVersions { get; set; }
    public StatusCategory StatusCategory { get; set; }
    public object AggregateTimeSpent { get; set; }
    public object Resolution { get; set; }
    public Timetracking Timetracking { get; set; }
    public object Security { get; set; }
    public List<object> Attachment { get; set; }
    public object AggregateTimeEstimate { get; set; }
    public object ResolutionDate { get; set; }
    public int WorkRatio { get; set; }
    public string Summary { get; set; }
    public IssuerRestriction IssuerRestriction { get; set; }
    public Watches Watches { get; set; }
    public string LastViewed { get; set; }
    public Creator Creator { get; set; }
    public List<object> Subtasks { get; set; }
    public string Created { get; set; }
    public object CustomField10020 { get; set; }
    public Reporter Reporter { get; set; }
    public object CustomField10021 { get; set; }
    public AggregateProgress AggregateProgress { get; set; }
    public Priority Priority { get; set; }
    public object CustomField10001 { get; set; }
    public List<string> Labels { get; set; }
    public object CustomField10016 { get; set; }
    public string Environment { get; set; }
    public string CustomField10019 { get; set; }
    public object TimeEstimate { get; set; }
    public object AggregateTimeOriginalEstimate { get; set; }
    public List<object> Versions { get; set; }
    public object DueDate { get; set; }
    public IssueProgress Progress { get; set; }
    public Votes Votes { get; set; }
    public List<object> IssueLinks { get; set; }
    public object Assignee { get; set; }
    public string Updated { get; set; }
    public Status Status { get; set; }
}

public class IssueType
{
    public string Self { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }
    public string IconUrl { get; set; }
    public string Name { get; set; }
    public bool Subtask { get; set; }
    public int AvatarId { get; set; }
    public string EntityId { get; set; }
    public int HierarchyLevel { get; set; }
}

public class Project
{
    public string Self { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public string ProjectTypeKey { get; set; }
    public bool Simplified { get; set; }
    public AvatarUrls AvatarUrls { get; set; }
}

public class StatusCategory
{
    public string Self { get; set; }
    public object Id { get; set; }
    public string Key { get; set; }
    public string ColorName { get; set; }
    public string Name { get; set; }
}

public class Timetracking
{
    // Add relevant fields if applicable
}

public class IssuerRestriction
{
    public object IssuerRestrictions { get; set; }
    public bool ShouldDisplay { get; set; }
}

public class Watches
{
    public string Self { get; set; }
    public int WatchCount { get; set; }
    public bool IsWatching { get; set; }
}

public class Creator
{
    public string Self { get; set; }
    public string AccountId { get; set; }
    public AvatarUrls AvatarUrls { get; set; }
    public string DisplayName { get; set; }
    public bool Active { get; set; }
    public string TimeZone { get; set; }
    public string AccountType { get; set; }
}

public class Reporter
{
    public string Self { get; set; }
    public string AccountId { get; set; }
    public AvatarUrls AvatarUrls { get; set; }
    public string DisplayName { get; set; }
    public bool Active { get; set; }
    public string TimeZone { get; set; }
    public string AccountType { get; set; }
}

public class AggregateProgress
{
    public int Progress { get; set; }
    public int Total { get; set; }
}

public class Priority
{
    public string Self { get; set; }
    public string IconUrl { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
}

public class IssueProgress
{
    public int Progress { get; set; }
    public int Total { get; set; }
}

public class Votes
{
    public string Self { get; set; }
    public int VotesCount { get; set; }
    public bool HasVoted { get; set; }
}

public class Status
{
    public string Self { get; set; }
    public string Description { get; set; }
    public string IconUrl { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
    public StatusCategory StatusCategory { get; set; }
}

public class Changelog
{
    public string Id { get; set; }
    public List<ChangelogItem> Items { get; set; }
}

public class ChangelogItem
{
    public string Field { get; set; }
    public string FieldType { get; set; }
    public string FieldId { get; set; }
    public string From { get; set; }
    public string FromString { get; set; }
    public string To { get; set; }
    public string ToString { get; set; }
}