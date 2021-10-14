using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.API.Entities
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Rank{ get; set; }

        //[ForeignKey("RestaurantId")]
        //public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }
    }
}
