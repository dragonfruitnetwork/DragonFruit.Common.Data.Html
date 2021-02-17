using System;
using System.Linq;
using System.Text;
using DragonFruit.Common.Data.Html.Internal;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html
{
    public static class HtmlClientExtensions
    {
        /// <summary>
        /// Performs a <see cref="ApiRequest"/>, returning the result as a <see cref="HtmlDocument"/>
        /// </summary>
        public static HtmlDocument PerformHtml<TClient>(this TClient client, ApiRequest request, Encoding encoding = null) where TClient : ApiClient
        {
            using var response = client.Perform(request);
            response.EnsureSuccessStatusCode();

            using var stream = response.Content.ReadAsStreamAsync().Result;
            return HtmlDocumentLoader.LoadFromStream(stream, encoding);
        }

        /// <summary>
        /// Extracts a value from the <see cref="HtmlNode"/> based on its XPath and attribute name
        /// </summary>
        public static string GetValue(this HtmlNode node, string xpath = default, string attribute = default, bool throwOnNotFound = false)
        {
            var subNode = string.IsNullOrEmpty(xpath) ? node : node.SelectSingleNode(xpath);
            var useInnerText = string.IsNullOrEmpty(attribute);

            switch (useInnerText)
            {
                case false when throwOnNotFound:
                    return subNode.Attributes.Single(x => x.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase)).Value;

                case false:
                    return subNode.Attributes.SingleOrDefault(x => x.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase))?.Value;

                case true:
                    return subNode.InnerText;
            }
        }
    }
}
