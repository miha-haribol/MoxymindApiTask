using Newtonsoft.Json;

namespace Moxymind.Tests.Models;

// Represents an individual user inside the "data" array
public class UserData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("avatar")]
    public string Avatar { get; set; }
}

// Represents the root JSON object of the response
public class GetUsersResponse
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("data")]
    public List<UserData> Data { get; set; }
}