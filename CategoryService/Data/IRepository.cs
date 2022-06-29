using CategoryService.Dtos;
namespace CategoryService.Data
{
    public interface IRepository
    {
        Task<NewsCategoryReadDto> Add(NewsCategoryCreateDto newsCategoryCreate);
        Task<NewsCategoryReadDto> Update(NewsCategoryCreateDto newsCategoryCreate);
        Task<NewsCategoryReadDto> GetById(int id);
        Task<IEnumerable<NewsCategoryReadDto>> Get();
        void Remove(NewsCategoryReadDto newsCategoryRead);
    }
}
