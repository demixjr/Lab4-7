using System.Linq;
using AutoMapper;
using BLL.dto;
using DAL;

namespace BLL
{
    

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Heading, HeadingDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<Tag,  TagDto>().ReverseMap();
            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
        }
    }

}
