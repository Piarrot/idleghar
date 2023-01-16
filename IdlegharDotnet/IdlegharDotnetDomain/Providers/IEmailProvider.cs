using IdlegharDotnetDomain.Entities.Notifications;

namespace IdlegharDotnetDomain.Providers
{
    public interface IEmailsProvider
    {
        Task<EmailTemplate> GetTemplate(string templateName);
        Task sendEmail(SendEmailRequest req);
    }
}