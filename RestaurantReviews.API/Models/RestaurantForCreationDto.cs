using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.Models
{
    public class RestaurantForCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public double AverageRating { get; set; }
    }
}
