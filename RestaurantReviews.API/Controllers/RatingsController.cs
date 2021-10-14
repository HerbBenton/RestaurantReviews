using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Entities;
using RestaurantReviews.API.Models;
using RestaurantReviews.API.Services;
using System;

namespace RestaurantReviews.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{Id}/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RatingsController(IRestaurantRepository repository,
            IMapper mapper)
        {
            _restaurantRepository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetRatings(Guid Id)
        {
            var results = _restaurantRepository.GetRatings(Id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("{ratingId:guid}", Name = "GetRatingForRestaurant")]
        public ActionResult<RatingDto> GetRatingForRestaurant(Guid Id, Guid ratingId)
        {
            var results = _restaurantRepository.GetRating(ratingId, Id);
            
            return Ok(_mapper.Map<RatingDto>(results));
        }

        [HttpPost]
        public ActionResult<RatingDto> CreateRatingForRestaurant(Guid Id, RatingForCreationDto rating)
        {
            //check if restaurant exists first
            if (!_restaurantRepository.RestaurantExists(Id))
            {
                return NotFound();
            }
            
            var ratingEntity = _mapper.Map<Entities.Rating>(rating);
            _restaurantRepository.AddRating(Id, ratingEntity);
            _restaurantRepository.Save();

            var ratingToReturn = _mapper.Map<RatingDto>(ratingEntity);
            return CreatedAtRoute("GetRatingForRestaurant", new { Id = Id, ratingId = ratingToReturn.Id }, ratingToReturn);
        }

        [HttpPut("{ratingId}")]
        public ActionResult UpdateRatingForRestaurant(Guid Id, Guid ratingId, RatingForUpdateDto rating)
        {
            if (!_restaurantRepository.RestaurantExists(Id)) { return NotFound(); }

            var ratingFromRepo = _restaurantRepository.GetRating(ratingId, Id);
            if (ratingFromRepo == null) { return NotFound(); }

            //map incoming DTO to rating from the persistence object for projection
            _mapper.Map(rating, ratingFromRepo);
            _restaurantRepository.UpdateRating(ratingFromRepo);
            _restaurantRepository.Save();

            return NoContent();
        }

        [HttpDelete("{ratingId:guid}")]
        public ActionResult DeleteRatingForRestaurant(Guid Id, Guid ratingId)
        {
            if (!_restaurantRepository.RestaurantExists(Id)) { return NotFound(); }
            var ratingFromRepo = _restaurantRepository.GetRating(ratingId, Id);
            if (ratingFromRepo == null) { return NotFound(); }

            _restaurantRepository.DeleteRating(ratingFromRepo);
            _restaurantRepository.Save();

            return NoContent();
        }
    }
}
