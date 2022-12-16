namespace IdlegharDotnetBackend;

public class SendEmailRequest
{
    public string To;
    public EmailTemplate Template;
    public Dictionary<string, string>? Context;
}

public interface IEmailsProvider
{
    Task<EmailTemplate> GetTemplate(string templateName);
    Task sendEmail(SendEmailRequest req);
}