using Newtonsoft.Json;
using Moxymind.Tests.Models;

namespace Moxymind.Tests.Configuration;

public static class ConfigReader
{
    public static AppSettings GetSettings()
    {
        AppSettings settings = new AppSettings();

        // 1. Try to load from the local settings.json file first
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "settings.json");
        
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            settings = JsonConvert.DeserializeObject<AppSettings>(json);
        }

        // 2. Override with Environment Variables if they exist (Priority for CI/CD)
        string envBaseUrl = Environment.GetEnvironmentVariable("BASE_URL");
        if (!string.IsNullOrEmpty(envBaseUrl))
        {
            settings.BaseUrl = envBaseUrl;
        }

        string envApiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (!string.IsNullOrEmpty(envApiKey))
        {
            settings.ApiKey = envApiKey;
        }

        string envMaxTime = Environment.GetEnvironmentVariable("MAX_RESPONSE_TIME_MS");
        if (!string.IsNullOrEmpty(envMaxTime) && int.TryParse(envMaxTime, out int parsedTime))
        {
            settings.MaxResponseTimeMs = parsedTime;
        }

        // 3. Strict Validation - Fail Fast if critical data is missing
        if (string.IsNullOrEmpty(settings.BaseUrl))
        {
            throw new InvalidOperationException("BaseUrl is missing. Please check settings.json or BASE_URL environment variable.");
        }

        // Enforce the API Key requirement
        if (string.IsNullOrEmpty(settings.ApiKey))
        {
            throw new InvalidOperationException("ApiKey is missing! Authentication is required to run these tests");
        }

        if (settings.MaxResponseTimeMs <= 0)
        {
            settings.MaxResponseTimeMs = 1000; 
        }

        return settings;
    }
}