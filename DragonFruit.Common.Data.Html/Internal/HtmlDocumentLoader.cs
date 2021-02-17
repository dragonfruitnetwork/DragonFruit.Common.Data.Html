using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html.Internal
{
    internal static class HtmlDocumentLoader
    {
        /// <summary>
        /// Creates and populates a <see cref="HtmlDocument"/> from a <see cref="Stream"/> of data
        /// </summary>
        public static HtmlDocument LoadFromStream(Stream input, Encoding encoding)
        {
            var document = new HtmlDocument();

            document.Load(input, encoding ?? Encoding.Default);
            return document;
        }
    }
}