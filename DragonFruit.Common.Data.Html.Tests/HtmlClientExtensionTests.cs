using DragonFruit.Common.Data.Html.Tests.Common;
using NUnit.Framework;

namespace DragonFruit.Common.Data.Html.Tests
{
    [TestFixture]
    public class HtmlClientExtensionTests
    {
        private readonly ApiClient _basicClient = new ApiClient();

        [Test]
        public void PerformHtmlExtensionTest()
        {
            var htmlDocument = _basicClient.PerformHtml(new HtmlPageRequest());
            var title = htmlDocument.DocumentNode.GetValue("/html/body/div/h1[1]/a");

            Assert.AreEqual(title, "certification");
        }
    }
}
