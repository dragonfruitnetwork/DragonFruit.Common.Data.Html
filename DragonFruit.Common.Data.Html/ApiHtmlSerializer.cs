using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

        public ApiHtmlSerializer()
            : this(new ApiXmlSerializer())
        {
        }

        public ApiHtmlSerializer(ISerializer baseSerializer)
        {
            _baseSerializer = baseSerializer;
        }

        public string ContentType => _baseSerializer.ContentType;

        public StringContent Serialize<T>(T input) where T : class => _baseSerializer.Serialize(input);

        public T Deserialize<T>(Task<Stream> input) where T : class
        {
            if (typeof(T) != typeof(HtmlDocument))
            {
                return _baseSerializer.Deserialize<T>(input);
            }

            using var stream = input.Result;
            return HtmlDocumentLoader.LoadFromStream(stream) as T;
        }
    }
}