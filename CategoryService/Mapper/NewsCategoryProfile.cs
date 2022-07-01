using AutoMapper;
using CategoryService.Model;
using CategoryService.Contract;

namespace CategoryService.Mapper
{
    public class NewsCategoryProfile:Profile
    {
        public NewsCategoryProfile()
        {
            CreateMap<NewsCategoryCreate, NewsCategory>();
            CreateMap<NewsCategory, NewsCategoryRead>();
            CreateMap<NewsCategoryRead, NewsCategory>();
            CreateMap<NewsCategoryRead, NewsCategoryCreate>();
        }
    }
}
