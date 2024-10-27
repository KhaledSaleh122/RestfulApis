using Microsoft.Extensions.Configuration;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;
using System.Net.Http;
using System.Text.Json;

namespace RestfulApis_Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Currency?> GetLatestCurrencyExchange()
        {
            var url = _configuration["CurrencyApi:Url"];
            var key = _configuration["CurrencyApi:Key"];
            var endpoint = $"latest?access_key={key}";
            var response = await _httpClient.GetStringAsync(url+endpoint);
            var currency = JsonSerializer.Deserialize<Currency>(response);
            return currency;
        }
    }
}
