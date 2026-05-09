FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore "Moxymind.Tests/Moxymind.Tests.csproj"
RUN dotnet build "Moxymind.Tests/Moxymind.Tests.csproj" -c Release --no-restore

ENTRYPOINT ["dotnet", "test", "Moxymind.Tests/Moxymind.Tests.csproj", "-c", "Release", "--no-build", "--verbosity", "detailed", "--logger", "trx;LogFileName=Moxymind_Report.trx", "--results-directory", "/app/TestResults"]