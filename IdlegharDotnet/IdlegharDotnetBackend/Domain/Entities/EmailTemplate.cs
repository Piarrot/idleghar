using System.Text.RegularExpressions;

namespace IdlegharDotnetBackend;
public class EmailTemplate
{
    public string Name { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Message { get; set; } = "";

    public string RenderMessage(Dictionary<string, string>? context = null)
    {
        string result = this.Message;

        if (context == null)
        {
            return result;
        }

        var properties = context.Keys;
        foreach (var propKey in properties)
        {
            string propValue = context[propKey]?.ToString() ?? "";
            result = Regex.Replace(result, $"{{{propKey}}}", propValue);
        }

        return result;
    }
}