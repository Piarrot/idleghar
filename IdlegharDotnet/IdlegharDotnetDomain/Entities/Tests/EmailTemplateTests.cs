using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Tests
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
            Assert.AreEqual("Missing key: someVariable from context", ex!.Message);
        }
    }
}