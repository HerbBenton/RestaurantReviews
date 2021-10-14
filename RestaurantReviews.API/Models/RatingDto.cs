using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.Models
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
