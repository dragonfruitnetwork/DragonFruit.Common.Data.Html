# HtmlDocument Loader for DragonFruit.Data.Common

![NuGet Publishing](https://github.com/dragonfruitnetwork/DragonFruit.Common.Data.Html/workflows/Publish/badge.svg)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a8d6e2cfb55449099204fb2208e07a26)](https://app.codacy.com/gh/dragonfruitnetwork/common-html-serializer?utm_source=github.com&utm_medium=referral&utm_content=dragonfruitnetwork/DragonFruit.Common.Data.Html&utm_campaign=Badge_Grade)
[![NuGet](https://img.shields.io/nuget/v/DragonFruit.Common.Data.Html)](https://www.nuget.org/packages/DragonFruit.Common.Data.Html/)
[![Nuget](https://img.shields.io/nuget/dt/DragonFruit.Common.Data.Html)](https://www.nuget.org/packages/DragonFruit.Common.Data.Html/)
![GitHub](https://img.shields.io/github/license/dragonfruitnetwork/DragonFruit.Common.Data.Html)
[![DragonFruit Discord](https://img.shields.io/discord/482528405292843018?label=Discord)](https://discord.gg/VA26u5Z)

## Overview

This is an extension of the `DragonFruit.Common.Data` NuGet package that allows developers to perform an `ApiRequest` that can be deserialized into a `HtmlDocument` for data extraction, manipulation, etc. It provides 2 methods for doing so:

### Extension Method

The easiest to use without messing with existing code is the extension method, `PerformHtml(request)`. This will perform the request and return the result as a `HtmlDocument`.

Example:

```cs
using DragonFruit.Common.Data;
using DragonFruit.Common.Data.Html;

private readonly ApiClient _client = new ApiClient();

private void GetHtmlDocument()
{
    var htmlDocument = _client.PerformHtml(new DummyApiRequest());
}
```

### `ApiHtmlDeserializer`

This package exposes a new ISerializer that can "wrap" an existing `ISerializer` and intercept all `HtmlDocument` requests and handle them separately. This is the more advanced method but adds no extra functions to remember.

Example:

```cs
using DragonFruit.Common.Data;
using DragonFruit.Common.Data.Html;

private readonly _serializerClient = new ApiClient
{
    Serializer = new ApiHtmlSerializer() // this will wrap an ApiXmlSerializer
    Serializer = new ApiHtmlSerializer(new ApiJsonSerializer()) // this will wrap an ApiJsonSerializer
};

private void GetHtmlDocument()
{
    // this will cause the wrapper to intercept the request and do its own thing
    var htmlDocument = _serializerClient.Perform<HtmlDocument>(new DummyApiRequest());
    
    // this will be deserialized by whatever is being wrapped
    var baseDeserializedEntry = _serializerClient.Perform<DummyResponse>(new DummyApiRequest());
}
```

## License & Contributing

DragonFruit.Common.Data.Html is licensed under the MIT License. Should you encounter an issue, feel free to open an issue and we'll aim to respond ASAP
