# Use .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore project files
COPY ["Account/src/Account.API/Account.API.csproj", "Account.API/"]
COPY ["Account/src/Account.Application/Account.Application.csproj", "Account.Application/"]
COPY ["Account/src/Account.Domain/Account.Domain.csproj", "Account.Domain/"]
COPY ["Account/src/Account.Infrastructure/Account.Infrastructure.csproj", "Account.Infrastructure/"]

RUN dotnet restore "Account.API.csproj"

# Copy the remaining files and build
COPY . .
RUN dotnet publish "Account.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Account.API.dll"]