using RestSharp;
using Moxymind.Tests.Configuration;
using Moxymind.Tests.Models;
using RestSharp.Serializers.NewtonsoftJson;

namespace Moxymind.Tests.Client;

public class ReqresApiClient
{
    private readonly RestClient _client;

    public ReqresApiClient()
    {
        // Load configuration using the dedicated ConfigReader
        var settings = ConfigReader.GetSettings();

        var options = new RestClientOptions(settings.BaseUrl)
        {
        };
        
        _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());

        // Add the mandatory API key header for all requests
        _client.AddDefaultHeader("x-api-key", settings.ApiKey);
        
    }

    /// <summary>
    /// Specific method to create a user. Automatically deserializes the response.
    /// </summary>
    public async Task<RestResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest payload)
    {
        return await ExecutePostAsync<CreateUserResponse>("api/users", payload);
    }

    /// <summary>
    /// Specific method to get a list of users. Automatically deserializes the response.
    /// </summary>
    public async Task<RestResponse<GetUsersResponse>> GetUsersAsync(int page)
    {
        return await ExecuteGetAsync<GetUsersResponse>($"api/users?page={page}");
    }

    private async Task<RestResponse<T>> ExecuteGetAsync<T>(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Get);
        return await _client.ExecuteAsync<T>(request);
    }

    private async Task<RestResponse<T>> ExecutePostAsync<T>(string endpoint, object payload)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(payload); 
        return await _client.ExecuteAsync<T>(request);
    }
}