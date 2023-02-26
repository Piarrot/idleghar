namespace IdlegharDotnetDomain.Entities.Notifications
{
    public record class SendEmailRequest(string To, string TemplateName, Dictionary<string, string>? Context);
}