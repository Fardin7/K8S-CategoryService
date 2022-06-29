using CategoryService.Dtos;

namespace CategoryService.NewsClient
{
    public interface IClientUpdate
    {
        Task<HttpResponseMessage> Notify(NewsCategoryCreateDto newsCategoryRead);
    }
}
