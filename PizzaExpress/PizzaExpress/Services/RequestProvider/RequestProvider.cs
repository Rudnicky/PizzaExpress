using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaExpress.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response != null && response.IsSuccessStatusCode)
            {
                var serialized = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(serialized))
                {
                    var result = JsonConvert.DeserializeObject<TResult>(serialized);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return default;
        }
    }
}
