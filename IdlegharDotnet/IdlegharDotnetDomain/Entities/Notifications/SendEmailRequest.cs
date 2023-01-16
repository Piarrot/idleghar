namespace IdlegharDotnetDomain.Entities.Notifications
{
    public record class SendEmailRequest(string To, EmailTemplate Template, Dictionary<string, string>? Context);
}