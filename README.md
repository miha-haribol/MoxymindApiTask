Moxymind API Automation Framework 
A professional API testing suite for Reqres.in built with .NET 10, RestSharp, and xUnit.

🐳 Quick Start (Docker - Recommended)
The easiest way to run the tests is using Docker Compose. This ensures a consistent environment regardless of your local setup.

Set your API Key:

# PowerShell
$env:API_KEY="your_key_here"

# Linux/macOS/Bash
export API_KEY="your_key_here"

Run tests:

Bash
docker-compose up --build --exit-code-from api-tests

💻 Local Execution
If you prefer running it without Docker, ensure you have the .NET 10 SDK installed.

Create a settings.json in the Moxymind.Tests folder:

JSON
{
  "BaseUrl": "https://reqres.in/",
  "ApiKey": "your_key_here",
  "MaxResponseTimeMs": 1000
}

Run command:

Bash
dotnet test

🛠 Tech Stack

Framework: xUnit

HTTP Client: RestSharp (with Newtonsoft.Json)

Assertions: FluentAssertions

Infrastructure: Docker & GitHub Actions

⚙️ CI/CD & Configuration
The project is integrated with GitHub Actions. All configuration values can be overridden via Environment Variables:

BASE_URL

API_KEY

MAX_RESPONSE_TIME_MS