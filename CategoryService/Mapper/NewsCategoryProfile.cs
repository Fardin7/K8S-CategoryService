using AutoMapper;
using CategoryService.Dtos;
using CategoryService.Model;
using CategoryService.Contract;

namespace CategoryService.Mapper
{
    public class NewsCategoryProfile:Profile
    {
        public NewsCategoryProfile()
        {
            CreateMap<NewsCategoryCreateDto, NewsCategory>();
            CreateMap<NewsCategory, NewsCategoryReadDto>();
            CreateMap<NewsCategoryReadDto, NewsCategoryCreateDto>();

            CreateMap<NewsCategoryCreateDto, NewsCategoryCreate>();
            CreateMap<NewsCategoryCreateDto, NewsCategoryUpdate>();
            CreateMap<NewsCategoryReadDto, NewsCategoryDelete>();
            CreateMap<NewsCategoryReadDto, NewsCategoryCreate>();
        }
    }
}
