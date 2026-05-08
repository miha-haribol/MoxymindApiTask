Moxymind API Automation
Automated test suite for the Reqres API built with .NET 10, RestSharp, and xUnit.

🚀 Quick Start
Clone the repository.

Create settings.json in the Moxymind.Tests folder:

JSON
{
  "BaseUrl": "https://reqres.in/",
  "ApiKey": "your_key_here",
  "MaxResponseTimeMs": 1000
}

Run tests:

Bash
dotnet test

🛠 Tech Stack
HTTP Client: RestSharp

Assertions: FluentAssertions

JSON: Newtonsoft.Json

Test Runner: xUnit

📋 Test Scenarios
GET /api/users: Validates pagination, data integrity, and JSON schema (types).

POST /api/users: Data-driven user creation using TestData/users.json.

Performance: Validates that response time is within the threshold set in settings.json.

⚙️ Configuration & CI/CD
All settings can be overridden via Environment Variables for CI/CD integration:

BASE_URL

API_KEY

MAX_RESPONSE_TIME_MS