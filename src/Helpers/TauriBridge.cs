using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace src.Helpers;

// Represents the request structure the .NET backend plugin expects
public class PluginRequest
{
    [JsonPropertyName("controller")]
    public required string Controller { get; set; } // Add 'required'

    [JsonPropertyName("action")]
    public required string Action { get; set; } // Add 'required'

    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

// Represents the response structure from the .NET backend plugin
public class PluginResponse<T>
{
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }
}

public class TauriBridge
{
    private readonly IJSRuntime _js;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public TauriBridge(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<T?> InvokeAsync<T>(string controller, string action, object? data = null)
    {
        var pluginRequest = new PluginRequest
        {
            Controller = controller,
            Action = action,
            Data = data
        };

        // This calls the 'dotnet_request' command defined in src-tauri/src/lib.rs
        var responseJson = await _js.InvokeAsync<string>("window.__TAURI_INTERNALS__.invoke", "dotnet_request", new
        {
            request = JsonSerializer.Serialize(pluginRequest, _jsonOptions)
        });

        var response = JsonSerializer.Deserialize<PluginResponse<T>>(responseJson, _jsonOptions);

        if (response is null)
        {
            throw new Exception("Failed to deserialize the response from the backend.");
        }

        if (!string.IsNullOrEmpty(response.ErrorMessage))
        {
            throw new Exception($"Backend error: {response.ErrorMessage}");
        }

        return response.Data;
    }
}
