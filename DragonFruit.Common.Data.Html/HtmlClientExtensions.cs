using System;
using System.Linq;
using DragonFruit.Common.Data.Html.Internal;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html
{
    public static class HtmlClientExtensions
    {
        /// <summary>
        /// Performs a <see cref="ApiRequest"/>, returning the result as a <see cref="HtmlDocument"/>
        /// </summary>
        public static HtmlDocument PerformHtml<TClient>(this TClient client, ApiRequest request) where TClient : ApiClient
        {
            using var response = client.Perform(request);
            response.EnsureSuccessStatusCode();

            using var stream = response.Content.ReadAsStreamAsync().Result;
            return HtmlDocumentLoader.LoadFromStream(stream);
        }

        /// <summary>
        /// Extracts a value from the <see cref="HtmlNode"/> based on its XPath and attribute name
        /// </summary>
        public static string GetValueFromXPath(this HtmlNode node, string xpath = default, string attribute = default)
        {
            return node.GetValue(xpath, attribute, true);
        }

        /// <summary>
        /// Extracts a value (or returns a default value) from the <see cref="HtmlNode"/> based on its XPath and attribute name
        /// </summary>
        public static string GetValueOrDefaultFromXPath(this HtmlNode node, string xpath = default, string attribute = default)
        {
            return node.GetValue(xpath, attribute, false);
        }

        private static string GetValue(this HtmlNode node, string xpath, string attribute, bool throwOnNotFound)
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
