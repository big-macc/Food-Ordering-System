using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Food_Ordering_System.Models;

public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options) { }

    public DbSet<CompanyName> CompanyNames { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }

    //TODO
    /*User
      Cart
      Payment
      .....*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyName>()
            .HasMany<FoodItem>()
            .WithOne(f => f.Company)
            .HasForeignKey(f => f.ComId)
            //.OnDelete(DeleteBehavior.Cascade);
            .OnDelete(DeleteBehavior.SetNull);  // Testing
            
        // Testing make company null
        modelBuilder.Entity<FoodItem>()
            .Property(f => f.ComId)
            .IsRequired(false);
    }

}

public class CompanyName
{
    [Key]
    public string ComId { get; set; }

    public string ComName { get; set; } = "";
}

public class FoodItem
{
    [Key]
    public string FoodId { get; set; }

    public string FoodName { get; set; } = "";

    public int FoodQty { get; set; }

    public decimal Price { get; set; }

    public string FoodPic { get; set; } = "";
    
    public string? ComId { get; set; }
    
    public CompanyName? Company { get; set; }
}
