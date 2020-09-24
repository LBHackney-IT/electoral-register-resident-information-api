FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ElectoralRegisterResidentInformationApi.sln ./
COPY ./ElectoralRegisterResidentInformationApi/ElectoralRegisterResidentInformationApi.csproj ./ElectoralRegisterResidentInformationApi/
COPY ./ElectoralRegisterResidentInformationApi.Tests/ElectoralRegisterResidentInformationApi.Tests.csproj ./ElectoralRegisterResidentInformationApi.Tests/

RUN dotnet restore ./ElectoralRegisterResidentInformationApi/ElectoralRegisterResidentInformationApi.csproj
RUN dotnet restore ./ElectoralRegisterResidentInformationApi.Tests/ElectoralRegisterResidentInformationApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ElectoralRegisterResidentInformationApi.Tests/ElectoralRegisterResidentInformationApi.Tests.csproj

CMD dotnet test
