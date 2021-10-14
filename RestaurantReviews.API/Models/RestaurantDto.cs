using RestaurantReviews.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.Models
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public double Rating { get; set; }
        //public ICollection<Rating> Rating { get; set; }
    }

}

