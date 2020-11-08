using System.IO;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html.Internal
{
    internal static class HtmlDocumentLoader
    {
        /// <summary>
        /// Creates and populates a <see cref="HtmlDocument"/> from a <see cref="Stream"/> of data
        /// </summary>
        public static HtmlDocument LoadFromStream(Stream input)
        {
            var document = new HtmlDocument();

            document.Load(input);
            return document;
        }
    }
}