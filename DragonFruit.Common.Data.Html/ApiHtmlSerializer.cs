using System.IO;
using System.Net.Http;
using System.Text;
using DragonFruit.Common.Data.Html.Internal;
using DragonFruit.Common.Data.Serializers;
using HtmlAgilityPack;

namespace DragonFruit.Common.Data.Html
{
    /// <summary>
    /// An <see cref="ISerializer"/> wrapper that intercepts <see cref="HtmlDocument"/> requests and handles them separately,
    /// leaving all other requests to be passed through to the original <see cref="ISerializer"/>
    /// </summary>
    public class ApiHtmlSerializer : ISerializer
    {
        private readonly ISerializer _baseSerializer;
        private readonly Encoding _encoding;

        public ApiHtmlSerializer(Encoding encoding = null)
            : this(new ApiXmlSerializer(), encoding)
        {
        }

        public ApiHtmlSerializer(ISerializer baseSerializer, Encoding encoding = null)
        {
            _baseSerializer = baseSerializer;
            _encoding = encoding;
        }

        public string ContentType => _baseSerializer.ContentType;

        public StringContent Serialize<T>(T input) where T : class => _baseSerializer.Serialize(input);

        public T Deserialize<T>(Stream input) where T : class
        {
            if (typeof(T) != typeof(HtmlDocument))
            {
                return _baseSerializer.Deserialize<T>(input);
            }

            return HtmlDocumentLoader.LoadFromStream(input, _encoding) as T;
        }
    }
}
