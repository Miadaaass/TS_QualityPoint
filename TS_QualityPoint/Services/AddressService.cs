using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TS_QualityPoint.Options;

namespace TS_QualityPoint.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public AddressService(IHttpClientFactory httpClientFactory, DaDataOptions daDataOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiUrl = daDataOptions.ApiUrl;
        }

        public async Task<StandardizedAddressResponse> StandardizeAddressAsync(string address)
        {
            var requestBody = new { query = address };
            var response = await _httpClient.PostAsync(_apiUrl, new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<StandardizedAddressResponse>(jsonResponse);
                return result;
            }

            return null;
        }
    }
}
