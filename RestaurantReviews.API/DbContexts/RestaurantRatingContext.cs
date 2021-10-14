using RestaurantReviews.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace RestaurantReviews.API.DbContexts
{
    public class RestaurantRatingContext : DbContext
    {
        public RestaurantRatingContext(DbContextOptions<RestaurantRatingContext> options)
            : base(options)
        { 
            //stubbed
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed the db with some data ...
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Name = "Old Templeton",
                    Address = "123 Main St. Anytown NY 61732",
                    Description = "Americana",
                    Hours = "M-F 8AM - 9PM",
                    AverageRating = 0.0
                },
                new Restaurant()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Name = "Taco Crunchers",
                    Address = "400 E. Salvadora Dr. Anywhere NM 52431",
                    Description = "Mexicana",
                    Hours = "Saturday & Sunday 11AM - 7PM",
                    AverageRating = 0.0
                }
            );

            modelBuilder.Entity<Rating>().HasData(
                new Rating()
                {
                    Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    RestaurantId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Rank = 1 
                },
                new Rating()
                {
                    Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    RestaurantId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Rank = 5
                },
                new Rating()
                {
                    Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    RestaurantId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Rank = 2
                },
                new Rating()
                {
                    Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                    RestaurantId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Rank = 3
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
