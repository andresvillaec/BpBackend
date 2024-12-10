# Use the .NET 8.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the project files and restore dependencies
COPY Client/src/Client.API/Client.API.csproj ./Client/src/Client.API/
COPY Client/src/Client.Application/Client.Application.csproj ./Client/src/Client.Application/
COPY Client/src/Client.Domain/Client.Domain.csproj ./Client/src/Client.Domain/
COPY Client/src/Client.Infrastructure/Client.Infrastructure.csproj ./Client/src/Client.Infrastructure/

# If SharedKernel is used, copy its project file
COPY SharedKernel/SharedKernel.csproj ./SharedKernel/

# Restore dependencies
RUN dotnet restore ./Client/src/Client.API/Client.API.csproj

# Copy the entire source code
COPY Client/ ./Client/
COPY SharedKernel/ ./SharedKernel/

# Build and publish the application
WORKDIR /source/Client/src/Client.API
RUN dotnet publish -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Client.API.dll"]