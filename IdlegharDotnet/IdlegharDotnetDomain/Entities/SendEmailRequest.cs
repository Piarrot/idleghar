namespace IdlegharDotnetDomain.Entities
{
    public class SendEmailRequest
    {
        public string To;
        public EmailTemplate Template;
        public Dictionary<string, string>? Context;
    }
}