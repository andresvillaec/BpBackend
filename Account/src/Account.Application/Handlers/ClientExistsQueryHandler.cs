using MediatR;
using Microsoft.Extensions.Configuration;

namespace Account.Application.Handlers;

public class ClientExistsQueryHandler : IRequestHandler<ClientExistsQuery, bool>
{
    private readonly string _clientApiBaseUrl;
    private readonly HttpClient _httpClient;

    public ClientExistsQueryHandler(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientApiBaseUrl = configuration["CLIENT_API_BASE_URL"];
    }

    public async Task<bool> Handle(ClientExistsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"CLIENT_API_BASE_URL: {_clientApiBaseUrl}");
            var url = $"{_clientApiBaseUrl}/api/clients/exists/{request.ClientId}";
            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var exists = await response.Content.ReadAsStringAsync();
            return bool.TryParse(exists, out var result) && result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}