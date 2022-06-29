using CategoryService.Dtos;
using System.Text;
using System.Text.Json;

namespace CategoryService.NewsClient
{
    public class NewsService : IClientUpdate
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> Notify(NewsCategoryCreateDto newsCategoryRead)
        {
            HttpClient _httpClient = _httpClientFactory.CreateClient("NewsClientPlicy");

            HttpContent content = new StringContent(JsonSerializer.Serialize(newsCategoryRead), Encoding.UTF8
                , "application/json");

           return  await _httpClient.PostAsync("http://localhost:5056/api/news", content);
        }
    }
}
