using RestaurantReviews.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.Helpers
{
    public static class RestaurantRatingExtensions
    {
        public static double GetAverageRating(this ICollection<Rating> ratings)
        {
            if (ratings.Count > 0)
            {
                var results = new List<int>();

                foreach (Rating r in ratings)
                {
                    results.Add(r.Rank);
                }

                double average = results.Average();
                return average;
            }
            else
            {
                return 0;
            }
        }
    }
}
