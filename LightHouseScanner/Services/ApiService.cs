using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LightHouseScanner.Services
{
    public class ApiService
    {
        private readonly HttpClient _apiHttpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _apiHttpClient = httpClientFactory.CreateClient("ApiHttpClient");
        }

        private async Task<HttpResponseMessage> PostAsync(object payload)
        {
            // Serialize the payload to JSON
            var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            // Post the payload to the API
            return await _apiHttpClient.PostAsync("", content);
        }

        public async Task<HttpResponseMessage> FetchAndSaveLighthouseReportAsync(string url)
        {
            var payload = new { url, CallType = "GetOrScanWebsite" };
            return await PostAsync(payload);
        }

        public async Task<HttpResponseMessage> FetchTop10ReScannedWebsitesAsync()
        {
            var payload = new { CallType = "GetLast10ReScannedWebsites" };
            return await PostAsync(payload);
        }

        public async Task<HttpResponseMessage> FetchLast10ScannedWebsitesAsync()
        {
            var payload = new { CallType = "GetLast10ScannedWebsites" };
            return await PostAsync(payload);
        }

        public async Task<HttpResponseMessage> FetchTop10BestWebsitesAsync()
        {
            var payload = new { CallType = "GetTop10BestWebsites" };
            return await PostAsync(payload);
        }

        private async Task<HttpResponseMessage> PostAsync(object payload, string? apiUrl = null)
        {
            var url = apiUrl;
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            try
            {
                var response = await _apiHttpClient.PostAsync(url, content);

                var responseData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success:");
                    Console.WriteLine(responseData);
                }
                else
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(responseData);
                }

                return response;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Request exception:");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}