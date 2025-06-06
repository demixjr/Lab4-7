
using AutoMapper;
using BLL.dto;
using PL.request_models;
using PL.response_models;

namespace PL
{
    public class MappingProfilePL:Profile
    {
        public MappingProfilePL()
        {

            CreateMap<UserRequestModel, UserDto>();
            CreateMap<UserDto, UserResponseModel>();

            CreateMap<HeadingRequestModel, HeadingDto>();
            CreateMap<HeadingDto, HeadingResponseModel>();

            CreateMap<CategoryRequestModel, CategoryDto>();
            CreateMap<CategoryRequestModelWithoutHeading,  CategoryDto>();
            CreateMap<CategoryDto, CategoryResponseModel>();
            CreateMap<CategoryDto, CategoryResponseModelWithoutSubcategories>();


            CreateMap<SubcategoryRequestModel, SubcategoryDto>();
            CreateMap<SubcategoryRequestModelWithoutCategory, SubcategoryDto>();
            CreateMap<SubcategoryDto, SubcategoryResponseModel>();

            CreateMap<TagRequestModel, TagDto>();
            CreateMap<TagDto, TagResponseModel>();

            CreateMap<AnnouncementRequestModel,  AnnouncementDto>();
            CreateMap<AnnouncementDto, AnnouncementResponseModel>();
            
        }
    }
}
