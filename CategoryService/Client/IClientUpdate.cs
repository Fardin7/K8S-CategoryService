using CategoryService.Contract;

namespace CategoryService.NewsClient
{
    public interface IClientUpdate
    {
        Task<HttpResponseMessage> Notify(NewsCategoryCreate newsCategoryRead);
    }
}
