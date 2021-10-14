using RestaurantReviews.API.DbContexts;
using RestaurantReviews.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.API.Services
{
    public class RestaurantRepository : IRestaurantRepository, IDisposable
    {
        //context
        private readonly RestaurantRatingContext _context;

        public RestaurantRepository(RestaurantRatingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        //Restaurant CRUD
        public void AddRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            // the repository fills the id (instead of using identity columns)
            restaurant.Id = Guid.NewGuid();

            foreach (var rating in restaurant.Ratings)
            {
                rating.Id = Guid.NewGuid();
            }

            _context.Restaurants.Add(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            _context.Restaurants.Remove(restaurant);
        }

        public Restaurant GetRestaurant(Guid restaurantId)
        {
            if (restaurantId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(restaurantId));
            }

            var restaurant = _context.Restaurants.FirstOrDefault(a => a.Id == restaurantId);
            restaurant.Ratings = _context.Ratings.Where(r => r.RestaurantId == restaurantId).ToList(); //TODO: refactor later as a method, LINQ, or?

            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            var results = _context.Restaurants.ToList<Restaurant>();

            foreach (Restaurant r in results)
            {
                r.Ratings = _context.Ratings.Where(a => a.RestaurantId == r.Id).ToList();
            }

            return results; //_context.Restaurants.ToList<Restaurant>();
        }

        public bool RestaurantExists(Guid restaurantId)
        {
            if (restaurantId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(restaurantId));
            }

            return _context.Restaurants.Any(a => a.Id == restaurantId);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            // no code in this implementation (using repository pattern).
        }


        //Rating CRUD
        public void AddRating(Guid restaurantId, Rating rating)
        {
            if (restaurantId == null)
            {
                throw new ArgumentNullException(nameof(restaurantId));
            }

            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating));
            }

            // the repository fills the id (instead of using identity columns)
            rating.Id = Guid.NewGuid();
            rating.RestaurantId = restaurantId;
            
            _context.Ratings.Add(rating);
        }

        public void DeleteRating(Rating rating)
        {
            _context.Ratings.Remove(rating);
        }

        //Get all ratings for a particular restaurant
        public IEnumerable<Rating> GetRatings(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return _context.Ratings.Where(a => a.RestaurantId == Id).ToList();
        }

        public Rating GetRating(Guid ratingId, Guid restaurantId)
        {
            if (ratingId == Guid.Empty || restaurantId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ratingId), nameof(restaurantId));
            }
            
            return _context.Ratings.Where(a => a.RestaurantId == restaurantId & a.Id == ratingId).FirstOrDefault();
        }

        public void UpdateRating(Rating rating)
        {
            // no code in this implementation (using repository pattern).
        }
       


        //garbage collector items
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

       
    }
}
