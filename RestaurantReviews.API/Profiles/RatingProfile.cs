using AutoMapper;

namespace RestaurantReviews.API.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Entities.Rating, Models.RatingDto>();

            CreateMap<Models.RatingForCreationDto, Entities.Rating>();

            CreateMap<Models.RatingForUpdateDto, Entities.Rating>();
        }
        
    }
}
