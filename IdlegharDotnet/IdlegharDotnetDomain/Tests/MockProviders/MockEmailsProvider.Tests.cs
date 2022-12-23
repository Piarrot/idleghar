using IdlegharDotnetDomain;
using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockEmailsProvider : IEmailsProvider
    {
        private Dictionary<string, EmailTemplate> EmailTemplates = new Dictionary<string, EmailTemplate>();
        private List<SendEmailRequest> EmailsSent = new List<SendEmailRequest>();

        public MockEmailsProvider()
        {
            EmailTemplates.Add(EmailTemplateNames.VALIDATION_CODE, new EmailTemplate
            {
                Message = "Code: {code}",
                Name = EmailTemplateNames.VALIDATION_CODE,
                Subject = "You got registered"
            });
        }

        public async Task sendEmail(SendEmailRequest req)
        {
            EmailsSent.Add(req);
            await Task.Yield();
        }

        public async Task<EmailTemplate> GetTemplate(string templateName)
        {
            EmailTemplate? template = null;
            EmailTemplates.TryGetValue(EmailTemplateNames.VALIDATION_CODE, out template);
            await Task.Yield();
            if (template != null)
            {
                return template;
            }

            throw new ArgumentException($"Template '{templateName}' not found");
        }

        public bool HasSentEmailsTo(string to)
        {
            return GetEmailsSentTo(to).Count() > 0;
        }

        public int CountEmailsSentTo(string to)
        {
            return GetEmailsSentTo(to).Count();
        }

        public List<SendEmailRequest> GetEmailsSentTo(string to)
        {
            return EmailsSent.FindAll(req => req.To == to);
        }

    }
}