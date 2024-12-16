# Use the .NET 8.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the project files and restore dependencies
COPY Account/src/Account.API/Account.API.csproj ./Account/src/Account.API/
COPY Account/src/Account.Application/Account.Application.csproj ./Account/src/Account.Application/
COPY Account/src/Account.Domain/Account.Domain.csproj ./Account/src/Account.Domain/
COPY Account/src/Account.Infrastructure/Account.Infrastructure.csproj ./Account/src/Account.Infrastructure/


# Restore dependencies
RUN dotnet restore ./Account/src/Account.API/Account.API.csproj

# Copy the entire source code
COPY Account/ ./Account/

# Build and publish the application
WORKDIR /source/Account/src/Account.API
RUN dotnet publish -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]