using AutoMapper;

namespace RestaurantReviews.API.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Entities.Restaurant, Models.RestaurantDto>()
                .ForMember(
                    dest => dest.Rating,
                     opt => opt.MapFrom(src => src.AverageRating));

            CreateMap<Models.RestaurantForCreationDto, Entities.Restaurant>();
            CreateMap<Models.RestaurantForUpdateDto, Entities.Restaurant>();
            CreateMap<Entities.Restaurant, Models.RestaurantForUpdateDto>();
        }
    }
}
