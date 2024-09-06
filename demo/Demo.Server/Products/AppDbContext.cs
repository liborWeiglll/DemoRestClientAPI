using Microsoft.EntityFrameworkCore;

namespace Demo.Server.Products;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Apple", Price = 1.99m, Description = "Example description of apple"},
            new Product { Id = 2, Name = "Banana", Price = 2.99m, Description = "Example description of banana"},
            new Product { Id = 3, Name = "Cherry", Price = 3.99m, Description = "Example description of cherry"},
            new Product { Id = 4, Name = "Date", Price = 4.99m, Description = "Example description of date"},
            new Product { Id = 5, Name = "Elderberry", Price = 5.99m, Description = "Example description of elderberry"},
            new Product { Id = 6, Name = "Fig", Price = 6.99m, Description = "Example description of fig"},
            new Product { Id = 7, Name = "Grape", Price = 3.54m, Description = "Example description of grape"},
            new Product { Id = 8, Name = "Honeydew", Price = 2.34m, Description = "Example description of honeydew"},
            new Product { Id = 9, Name = "Jackfruit", Price = 1.23m, Description = "Example description of jackfruit"},
            new Product { Id = 10, Name = "Kiwi", Price = 4.56m, Description = "Example description of kiwi"},
            new Product { Id = 11, Name = "Lemon", Price = 7.89m, Description = "Example description of lemon"},
            new Product { Id = 12, Name = "Mango", Price = 9.87m, Description = "Example description of mango"},
            new Product { Id = 13, Name = "Nectarine", Price = 5.67m, Description = "Example description of nectarine"},
            new Product { Id = 14, Name = "Orange", Price = 3.45m, Description = "Example description of orange"},
            new Product { Id = 15, Name = "Peach", Price = 2.34m, Description = "Example description of peach"},
            new Product { Id = 16, Name = "Quince", Price = 1.23m, Description = "Example description of quince"},
            new Product { Id = 17, Name = "Raspberry", Price = 4.56m, Description = "Example description of raspberry"},
            new Product { Id = 18, Name = "Strawberry", Price = 7.89m, Description = "Example description of strawberry"},
            new Product { Id = 19, Name = "Tangerine", Price = 9.87m, Description = "Example description of tangerine"},
            new Product { Id = 20, Name = "Ugli", Price = 5.67m, Description = "Example description of ugli"}
        );
    }
}