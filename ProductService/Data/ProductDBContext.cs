using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ProductService.Models;

namespace ProductService.Data{
public class ProductDBContext:DbContext
{
    //program.cs may jo database ki sari detail inject krty hy di container may productdbcontext option woh sab containe krty hy our hmy yaha frmework automatically deta hy
    public ProductDBContext(DbContextOptions<ProductDBContext>options):base(options){}


//is say database may aik Prodcuts name ki table bny gi yaha Product class kay strucutre wali
    public DbSet<Product> Products{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", Price = 1200, StockQuantity = 10, Category = "Electronics", CreatedAt = new DateTime(2025, 1, 1) },
                new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 20, StockQuantity = 50, Category = "Accessories", CreatedAt = new DateTime(2025, 1, 1) }
            );
        }

}
}