# Moxymind API Automation Framework

A professional API testing suite for [ReqRes](https://reqres.in/) built with **.NET 10**, **RestSharp**, and **xUnit**. The project is designed for reliable API validation, clean test structure, and flexible execution in local and CI environments. ReqRes requires an `x-api-key` header for every request, so the framework is configured around secure API-key-based execution.[web:3]

## Overview

This repository contains an automated API test suite for validating ReqRes endpoints with modern .NET tooling. It supports both Docker-based execution and local runs, making it easy to use in development, CI pipelines, and code review workflows.

## Tech Stack

| Component | Technology |
|---|---|
| Framework | xUnit |
| HTTP Client | RestSharp |
| JSON Serialization | Newtonsoft.Json |
| Assertions | FluentAssertions |
| Infrastructure | Docker, Docker Compose |
| CI/CD | GitHub Actions |

## Project Goals

- Provide a maintainable API automation framework for ReqRes.
- Keep execution simple for both local developers and CI systems.
- Support environment-based configuration without code changes.
- Enforce response validation and performance expectations.
- Offer a clean starting point for extending API coverage.

## Quick Start

### Docker Execution

Docker Compose is the recommended way to run the test suite because it gives a consistent environment across different machines.

Set the API key first:

```powershell
# PowerShell
$env:API_KEY="your_key_here"
```

```bash
# Linux/macOS/Bash
export API_KEY="your_key_here"
```

Run the tests:

```bash
docker-compose up --build --exit-code-from api-tests
```

### Local Execution

For local execution, install the .NET 10 SDK and create a `settings.json` file inside the `Moxymind.Tests` folder.

```json
{
  "BaseUrl": "https://reqres.in/",
  "ApiKey": "your_key_here",
  "MaxResponseTimeMs": 1000
}
```

Then run:

```bash
dotnet test
```

## Configuration

The framework supports overriding configuration values through environment variables, which is especially useful in CI/CD pipelines.

| Variable | Purpose |
|---|---|
| `BASE_URL` | Overrides the API base URL |
| `API_KEY` | Supplies the ReqRes API key |
| `MAX_RESPONSE_TIME_MS` | Sets the allowed response time threshold |

ReqRes documents that every request must include an `x-api-key` header.[web:3]

## CI/CD

The project is intended to work well with GitHub Actions and other CI systems. ReqRes highlights that its API can be used directly from CI environments without maintaining separate local servers, which fits this framework's container-friendly design.[web:9]

## Notes

- Store secrets such as `API_KEY` in environment variables or CI secrets.
- Avoid committing real credentials into `settings.json`.
- Keep test data and assertions deterministic when expanding the suite.
