using System.Net.Http.Json;
using System.Text.Json;

namespace tripscribe.Api.Integration.Test.TestUtilities;

public static class HttpClientJsonExtensionsPatch
{
    public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient @this, string? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        if (@this == null) throw new ArgumentNullException(nameof(@this));
        
        var content = JsonContent.Create(value, mediaType: null, options);
        return @this.PatchAsync(requestUri, content, cancellationToken);
    }
}