using System.IO;
using DragonFruit.Common.Data.Html.Internal;
using DragonFruit.Common.Data.Serializers;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html
{
    public class ApiHtmlSerializer : IntermediateSerializer
    {
        public ApiHtmlSerializer()
            : base(new ApiXmlSerializer())
        {
        }

        public override T Deserialize<T>(Stream input) where T : class
        {
            if (typeof(T) != typeof(HtmlDocument))
            {
                return base.Deserialize<T>(input);
            }

            return HtmlDocumentLoader.LoadFromStream(input, Encoding) as T;
        }
    }
}
