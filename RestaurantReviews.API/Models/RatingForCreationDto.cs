using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RestaurantReviews.API.Models
{
    public class RatingForCreationDto : IValidatableObject
    {
        [Required]
        public int Rank { get; set; }

        //validate the data being submitted falls within parameters
        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (Rank == 0 || Rank > 5)
            {
                yield return new ValidationResult(
                    "The provided rating should be a number ranging from 1 to 5.",
                    new[] { "RatingForCreationDto" });
            }
        }
    }
}
