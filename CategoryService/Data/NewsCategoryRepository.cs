using AutoMapper;
using CategoryService.Dtos;
using CategoryService.Model;
using Microsoft.EntityFrameworkCore;
namespace CategoryService.Data
{
    public class NewsCategoryRepository : IRepository
    {
        private readonly AppDBContext _appDbContext;
        private readonly IMapper _mapper;

        public NewsCategoryRepository(AppDBContext appDBContext, IMapper mapper)
        {
            _appDbContext = appDBContext;
            _mapper = mapper;
        }
        public async Task<NewsCategoryReadDto> Add(NewsCategoryCreateDto newsCategoryCreate)
        {
            var newsCategory = _mapper.Map<NewsCategory>(newsCategoryCreate);

            await _appDbContext.NewsCategory.AddAsync(newsCategory);

            _appDbContext.SaveChanges();

            return _mapper.Map<NewsCategoryReadDto>(newsCategory);
        }
        public async Task<IEnumerable<NewsCategoryReadDto>> Get()
        {
            var newsCategories = await _appDbContext.NewsCategory.ToListAsync();

            var result = new List<NewsCategoryReadDto>();
            newsCategories.ForEach(q =>
            {
                result.Add(_mapper.Map<NewsCategoryReadDto>(q));
            });

            return result;
        }
        public async Task<NewsCategoryReadDto> GetById(int id)
        {
            var newsCategory = await _appDbContext.NewsCategory.FindAsync(id);

            return _mapper.Map<NewsCategoryReadDto>(newsCategory);
        }
        public void Remove(NewsCategoryReadDto newsCategoryRead)
        {
            _appDbContext.NewsCategory.Remove(_mapper.Map<NewsCategory>(newsCategoryRead));

            _appDbContext.SaveChanges();
        }
        public async Task<NewsCategoryReadDto> Update(NewsCategoryCreateDto newsCategoryCreate)
        {
            var category = await _appDbContext.NewsCategory.FindAsync(newsCategoryCreate.Id);

            category.Description = newsCategoryCreate.Description;
            category.Name = newsCategoryCreate.Name;

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<NewsCategoryReadDto>(category);
        }
    }
}
