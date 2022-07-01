using CategoryService.Contract;

namespace CategoryService.AsyncConnection
{
    public interface INotification
    {
        Task CreateNotify(NewsCategoryCreate newsCategoryCreate);
    }
}
