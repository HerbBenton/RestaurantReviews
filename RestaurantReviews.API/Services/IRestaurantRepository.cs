using RestaurantReviews.API.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.API.Services
{
    public interface IRestaurantRepository
    {
        //Ratings
        IEnumerable<Rating> GetRatings(Guid ratingId);
        Rating GetRating(Guid ratingId, Guid restaurantId);
        void AddRating(Guid restaurantId, Rating rating);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);

        //Restaurants
        IEnumerable<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(Guid restaurantId);
        void AddRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        bool RestaurantExists(Guid restaurantId);
        bool Save();
    }
}