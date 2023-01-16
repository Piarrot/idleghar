using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Notifications.Tests
{
    class EmailTemplateTests : BaseTests
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

            Assert.That(template.RenderMessage(context), Is.EqualTo("This is a rendered message"));
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

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                template.RenderMessage();
            });
            Assert.That(ex!.Message, Is.EqualTo("Missing context when 'someVariable' is required"));
        }

        [Test]
        public void GivenAMessageWithKeysNotPresentInContextItShouldFail()
        {
            var template = new EmailTemplate()
            {
                Subject = "A cool message",
                Name = "SOME_ID",
                Message = "This is a {someVariable} message"
            };

            var context = new Dictionary<string, string> { };

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                template.RenderMessage(context);
            });
            Assert.That(ex!.Message, Is.EqualTo("Missing required key 'someVariable' from context"));
        }
    }
}