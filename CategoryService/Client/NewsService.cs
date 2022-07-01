using CategoryService.Contract;
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
        public async Task<HttpResponseMessage> Notify(NewsCategoryCreate newsCategoryCreate)
        {
            HttpClient _httpClient = _httpClientFactory.CreateClient("NewsClientPlicy");

            HttpContent content = new StringContent(JsonSerializer.Serialize(newsCategoryCreate), Encoding.UTF8
                , "application/json");
            Console.WriteLine("sending HttpClient request to news service.......!");
            try
            {
               
              
                HttpResponseMessage httpResponseMessage= await _httpClient.PostAsync("http://news-clusterip-srv:80/api/news/", content);
                //var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();


                //    var re= await System.Text.Json.JsonSerializer.DeserializeAsync<newsCategoryCreate>(contentStream);

                //Console.WriteLine("request has been sent...."+ re.Description);

                return httpResponseMessage;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error in sending request...."+ex.Message);
            }
            return null;
        }
    }
}
