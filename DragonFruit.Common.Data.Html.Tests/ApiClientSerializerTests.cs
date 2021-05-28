using DragonFruit.Common.Data.Html.Tests.Common;
using HtmlAgilityPack;
using NUnit.Framework;

namespace DragonFruit.Common.Data.Html.Tests
{
    [TestFixture]
    public class ApiClientSerializerTests
    {
        private readonly ApiClient _serializerClient = new ApiClient
        {
            Serializer = new ApiHtmlSerializer()
        };

        [Test]
        public void PerformApiDeserializationTest()
        {
            var htmlDocument = _serializerClient.Perform<HtmlDocument>(new HtmlPageRequest());
            var title = htmlDocument.DocumentNode.GetValue("/html/body/div/h1[1]/a");

            Assert.AreEqual(title, "certification");
        }
    }
}
