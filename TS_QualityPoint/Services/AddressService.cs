using TS_QualityPoint.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TS_QualityPoint.Options;

namespace TS_QualityPoint.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly DaDataOptions _options;

        public AddressService(IHttpClientFactory httpClientFactory, IOptions<DaDataOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _options = options.Value;
        }

        public async Task<AddressResponse> StandardizeAddressAsync(string address)
        {
            var request = new { query = address };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_options.ApiKey}");

            var response = await _httpClient.PostAsync(_options.BaseUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return null; // Возврат null в случае ошибки
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<AddressResponse>>(jsonResponse);
            return result?.Count > 0 ? result[0] : null; // Возвращаем первый элемент результата
        }
    }
}
