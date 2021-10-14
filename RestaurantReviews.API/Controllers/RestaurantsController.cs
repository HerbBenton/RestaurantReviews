using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Models;
using RestaurantReviews.API.Services;
using System;
using System.Collections.Generic;
using RestaurantReviews.API.Helpers;
using RestaurantReviews.API.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace RestaurantReviews.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantRepository repository,
            IMapper mapper) 
        {
            _restaurantRepository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            var results = _restaurantRepository.GetRestaurants();

            foreach (var restaurant in results)
            {
                restaurant.AverageRating = restaurant.Ratings.GetAverageRating();
            }

            //return new JsonResult(restaurantsFromRepo);
            //return Ok(restaurants);
            return Ok(_mapper.Map<IEnumerable<RestaurantDto>>(results));
        }

        [HttpGet("{restaurantId:guid}", Name = "GetRestaurant")]
        public IActionResult GetRestaurant(Guid restaurantId)
        {
            Restaurant results = _restaurantRepository.GetRestaurant(restaurantId);

            if (results == null)
            {
                return NotFound();
            }

            results.AverageRating = results.Ratings.GetAverageRating();

            return Ok(_mapper.Map<RestaurantDto>(results));
        }

        [HttpPost]
        public ActionResult<RestaurantDto> CreateRestaurant(RestaurantForCreationDto restaurant)
        {
            var restaurantEntity = _mapper.Map<Entities.Restaurant>(restaurant);
            _restaurantRepository.AddRestaurant(restaurantEntity);
            _restaurantRepository.Save();

            var restaurantToReturn = _mapper.Map<RestaurantDto>(restaurantEntity);
            return CreatedAtRoute("GetRestaurant", new { restaurantId = restaurantToReturn.Id }, restaurantToReturn);
        }

        [HttpPatch("{restaurantId}")]
        public ActionResult PartiallyUpdateRestaurant(Guid restaurantId, JsonPatchDocument<RestaurantForUpdateDto> patchDocument)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId)) { return NotFound(); }
            
            var restaurantFromRepo = _restaurantRepository.GetRestaurant(restaurantId);
            if (restaurantFromRepo == null) { return NotFound(); }

            var restaurantToPatch = _mapper.Map<RestaurantForUpdateDto>(restaurantFromRepo);
            patchDocument.ApplyTo(restaurantToPatch);

            if (!TryValidateModel(restaurantToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(restaurantToPatch, restaurantFromRepo);
            _restaurantRepository.UpdateRestaurant(restaurantFromRepo);
            _restaurantRepository.Save();

            return NoContent();
        }

        [HttpDelete("{Id:guid}")]
        public ActionResult DeleteRestaurant(Guid Id)
        {
            var restaurantFromRepo = _restaurantRepository.GetRestaurant(Id);
            if (restaurantFromRepo == null) { return NotFound(); }

            _restaurantRepository.DeleteRestaurant(restaurantFromRepo);
            _restaurantRepository.Save();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetRestaurantOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,DELETE");
            return Ok();
        }
    }
}
