using System.Diagnostics;
using System.Net;
using FluentAssertions;
using Moxymind.Tests.Client;
using Moxymind.Tests.Configuration;
using Moxymind.Tests.Models;
using Newtonsoft.Json;

namespace Moxymind.Tests;

public class CreateUserTests
{
    private readonly ReqresApiClient _apiClient;
    private readonly int _maxResponseTimeMs;

    public CreateUserTests()
    {
        // Initialize the client
        _apiClient = new ReqresApiClient();
        
        // Fetch the performance threshold limit from our configuration module
        _maxResponseTimeMs = ConfigReader.GetSettings().MaxResponseTimeMs;
    }

    /// <summary>
    /// Helper method to read the external JSON file and yield data for xUnit [Theory]
    /// </summary>
    public static IEnumerable<object[]> GetUsersTestData()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "users.json");
        string json = File.ReadAllText(filePath);
        
        var users = JsonConvert.DeserializeObject<List<CreateUserRequest>>(json);
        
        foreach (var user in users)
        {
            yield return new object[] { user };
        }
    }

    [Theory]
    [MemberData(nameof(GetUsersTestData))]
    public async Task CreateUser_WithValidData_ReturnsCreatedAndValidatesTime(CreateUserRequest requestPayload)
    {
        // Arrange
        var stopwatch = new Stopwatch();

        // Act
        stopwatch.Start();
        
        // Create user
        var response = await _apiClient.CreateUserAsync(requestPayload);
        
        stopwatch.Stop();

        // The model is already deserialized inside the client
        var responseData = response.Data;

        // Assert - HTTP Status
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseData.Should().NotBeNull();

        // Assert - ID and timestamp (createdAt)
        responseData.Id.Should().NotBeNullOrEmpty("API should generate a unique ID for the new user");
        
        // Validate that the timestamp is close to the current UTC time
        responseData.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1), 
            "createdAt timestamp should reflect the current time of creation");

        // Assert - Data integrity
        responseData.Name.Should().Be(requestPayload.Name);
        responseData.Job.Should().Be(requestPayload.Job);

        // Assert whether Response time was less than the variable loaded from settings
        long actualResponseTime = stopwatch.ElapsedMilliseconds;
        
        actualResponseTime.Should().BeLessThan(_maxResponseTimeMs, 
            $"API response time ({actualResponseTime}ms) exceeded the configured limit of {_maxResponseTimeMs}ms");
    }
}