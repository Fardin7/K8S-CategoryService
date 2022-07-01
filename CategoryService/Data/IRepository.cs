using CategoryService.Contract;

namespace CategoryService.Data
{
    public interface IRepository
    {
        Task<NewsCategoryRead> Add(NewsCategoryCreate newsCategoryCreate);
        Task<NewsCategoryRead> Update(NewsCategoryCreate newsCategoryCreate);
        Task<NewsCategoryRead> GetById(int id);
        Task<IEnumerable<NewsCategoryRead>> Get();
        void Remove(NewsCategoryRead newsCategoryRead);
    }
}
