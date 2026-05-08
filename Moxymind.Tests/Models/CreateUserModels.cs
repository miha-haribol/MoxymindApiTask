using Newtonsoft.Json;

namespace Moxymind.Tests.Models;

// Model for the POST request body
public class CreateUserRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("job")]
    public string Job { get; set; }
}

// Model for the POST response body
public class CreateUserResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("job")]
    public string Job { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
}