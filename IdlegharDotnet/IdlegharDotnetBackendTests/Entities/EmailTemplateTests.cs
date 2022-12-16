using IdlegharDotnetBackend;

namespace IdlegharDotnetBackendTests;

class EmailTemplateTests
{
    [Test]
    public void GivenAContextCanCorrectlyRenderAMessage()
    {
        var template = new EmailTemplate()
        {
            Subject = "A cool message",
            Name = "SOME_ID",
            Message = "This is a {someVariable} message"
        };

        var context = new Dictionary<string, string> { ["someVariable"] = "rendered" };

        Assert.AreEqual("This is a rendered message", template.RenderMessage(context));
    }

    [Test]
    public void GivenNoContextWillRenderMessageAsIs()
    {
        var template = new EmailTemplate()
        {
            Subject = "A cool message",
            Name = "SOME_ID",
            Message = "This is a {someVariable} message"
        };

        Assert.AreEqual("This is a {someVariable} message", template.RenderMessage());
    }

    [Test]
    public void GivenAContextWithEmptyValuesCanCorrectlyRenderAMessage()
    {
        var template = new EmailTemplate()
        {
            Subject = "A cool message",
            Name = "SOME_ID",
            Message = "This is a {someVariable} message"
        };

        var context = new Dictionary<string, string> { ["someVariable"] = null };
        Assert.AreEqual("This is a  message", template.RenderMessage(context));
    }
}