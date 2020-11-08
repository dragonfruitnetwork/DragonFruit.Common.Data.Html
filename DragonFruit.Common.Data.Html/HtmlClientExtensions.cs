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
        public static string GetValueFromXPath(this HtmlNode node, string xpath, string attribute)
        {
            return node.SelectSingleNode(xpath).Attributes.Single(x => x.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase)).Value;
        }

        /// <summary>
        /// Extracts a value (or returns a default value) from the <see cref="HtmlNode"/> based on its XPath and attribute name
        /// </summary>
        public static string GetValueOrDefaultFromXPath(this HtmlNode node, string xpath, string attribute)
        {
            return node.SelectSingleNode(xpath).Attributes.SingleOrDefault(x => x.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        /// <summary>
        /// Extracts the inner text/html from a XPath against the <see cref="HtmlNode"/> provided
        /// </summary>
        public static string GetValueFromXPath(this HtmlNode node, string xpath)
        {
            return node.SelectSingleNode(xpath).InnerText;
        }
    }
}