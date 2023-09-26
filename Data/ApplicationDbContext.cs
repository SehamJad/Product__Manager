using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;
namespace ProductManager.Data;

// using DbContext must first install packet
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
class ApplicationDbContext : DbContext
{
   private string connectionString = "Server=.;Database=ProductManager;Trusted_Connection=True;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

     //  a table with name product
    // number of columns = properties in Product
  public DbSet<Product> Product {get;set; }
}