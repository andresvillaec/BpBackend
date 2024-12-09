# Use .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore project files
COPY ["Client.API.csproj", "./"]

COPY ["Account/src/Client.API/Client.API.csproj", "Client.API/"]
COPY ["Account/src/Client.Application/Client.Application.csproj", "Client.Application/"]
COPY ["Account/src/Client.Domain/Client.Domain.csproj", "Client.Domain/"]
COPY ["Account/src/Client.Infrastructure/Client.Infrastructure.csproj", "Client.Infrastructure/"]

RUN dotnet restore "Client.API.csproj"

# Copy the remaining files and build
COPY . .
RUN dotnet publish "Client.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Client.API.dll"]