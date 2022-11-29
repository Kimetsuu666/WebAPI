using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa() {
                Id = 1,
                Name = "Royal Villa",
                Details = "Smth",
                ImageUrl = "",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedTime = DateTime.Now
            },
            new Villa() {
                Id = 2,
                Name = "Royal Villa",
                Details = "Smth",
                ImageUrl = "",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedTime = DateTime.Now
            },
            new Villa() {
                Id = 3,
                Name = "Royal Villa",
                Details = "Smth",
                ImageUrl = "",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedTime = DateTime.Now
            }
        );
    }
}