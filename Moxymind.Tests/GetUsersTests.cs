using System.Net;
using FluentAssertions;
using Moxymind.Tests.Client;
using Newtonsoft.Json.Linq;

namespace Moxymind.Tests;

public class GetUsersTests
{
    private readonly ReqresApiClient _apiClient;

    public GetUsersTests()
    {
        _apiClient = new ReqresApiClient();
    }

    [Fact]
    public async Task GetUsers_PageTwo_ReturnsExpectedDataAndTypes()
    {
        // Arrange
        int targetPage = 2;

        // Act
        var response = await _apiClient.GetUsersAsync(targetPage);
        var responseData = response.Data;

        // Assert - Basic HTTP verification
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseData.Should().NotBeNull();

        // 1. Assert "total" value
        responseData.Total.Should().Be(12);

        // 2. Assert "last_name" for the first and second user in the "data" array
        responseData.Data[0].LastName.Should().Be("Lawson");
        responseData.Data[1].LastName.Should().Be("Ferguson");

        // 3. Count received users and compare to the "total"
        responseData.Data.Should().HaveCount(responseData.PerPage);

        // --- OPTIONAL BONUS TASK
        
        // Asserting possible data types present in the response
        JObject jsonObject = JObject.Parse(response.Content);
        
        jsonObject["page"].Type.Should().Be(JTokenType.Integer);
        jsonObject["total"].Type.Should().Be(JTokenType.Integer);
        jsonObject["data"].Type.Should().Be(JTokenType.Array);
        
        // Asserting types for properties inside the first user object
        jsonObject["data"][0]["id"].Type.Should().Be(JTokenType.Integer);
        jsonObject["data"][0]["last_name"].Type.Should().Be(JTokenType.String);
        jsonObject["data"][0]["email"].Type.Should().Be(JTokenType.String);
    }
}