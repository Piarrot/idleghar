using System.Text.RegularExpressions;

namespace IdlegharDotnetDomain.Entities
{
    public class EmailTemplate : Entity
    {
        public string Name { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";

        public string RenderMessage(Dictionary<string, string>? context = null)
        {
            string result = this.Message;

            string pattern = @"{(.*?)}";

            var matches = Regex.Matches(this.Message, pattern);

            foreach (Match match in matches)
            {
                if (match.Success && match.Groups.Count > 0)
                {
                    string key = match.Groups[1].Value;
                    string? propValue;
                    if (context == null) throw new ArgumentException($"Missing context when '{key}' is required");
                    var isPresent = context.TryGetValue(key, out propValue);
                    if (!isPresent || propValue == null) throw new ArgumentException($"Missing required key '{key}' from context");
                    result = Regex.Replace(result, match.Value, propValue);
                }
            }

            return result;
        }
    }
}