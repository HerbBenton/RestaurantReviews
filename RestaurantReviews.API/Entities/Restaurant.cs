using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.API.Entities
{
    public class Restaurant
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Hours { get; set; }

        [Required]
        [MaxLength(200)]
        public double AverageRating { get; set; }

        public ICollection<Rating> Ratings { get; set; }
            = new List<Rating>();
    }
}
