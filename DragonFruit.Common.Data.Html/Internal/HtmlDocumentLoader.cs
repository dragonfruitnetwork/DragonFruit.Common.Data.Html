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

            if (encoding != null)
            {
                document.Load(input, encoding);
            }
            else
            {
                document.Load(input, true);
            }

            return document;
        }
    }
}
