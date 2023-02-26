using IdlegharDotnetDomain.Entities.Notifications;
using IdlegharDotnetDomain.Providers;
using Newtonsoft.Json;

namespace IdlegharDotnetBackend.Providers
{
    public class EmailsProvider : IEmailsProvider
    {
        private Dictionary<string, EmailTemplate> EmailTemplates = new Dictionary<string, EmailTemplate>();

        public EmailsProvider(ILogger<EmailsProvider> logger)
        {
            Logger = logger;

            this.LoadTemplates();
        }

        private void LoadTemplates()
        {
            using (StreamReader file = File.OpenText(@"assets/email-templates.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var templates = (List<EmailTemplate>)serializer.Deserialize(file, typeof(List<EmailTemplate>))!;
                foreach (var template in templates)
                {
                    this.EmailTemplates.Add(template.Name, template);
                }
            }
        }

        public ILogger<EmailsProvider> Logger { get; }

        public async Task<EmailTemplate> GetTemplate(string templateName)
        {
            EmailTemplate? template = null;
            EmailTemplates.TryGetValue(templateName, out template);
            await Task.Yield();
            if (template != null)
            {
                return template;
            }

            throw new ArgumentException($"Template '{templateName}' not found");
        }

        public async Task SendEmail(SendEmailRequest req)
        {
            await Task.Yield();
            var template = await this.GetTemplate(req.TemplateName);
            var renderedMessage = template.RenderMessage(req.Context);
            this.Logger.LogInformation($"[[EMAIL SENT]]]: <{req.To}> ({template.Subject}) {renderedMessage}");
        }
    }
}