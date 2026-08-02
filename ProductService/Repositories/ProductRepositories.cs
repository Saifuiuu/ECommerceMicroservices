
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;
namespace ProductService.Repositories{


  public  class ProductRepositories:IProductRepository
{
    
    private readonly ProductDBContext _context;

    public ProductRepositories(ProductDBContext context)
    {
        _context=context;
    }

public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products= await _context.Products.ToListAsync();

        return products;
    }


     public async Task<Product?> GetByIdAsync(int id)
    {
       return await _context.Products.FindAsync(id);

        
}

 public    async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        
    }
  public  Task UpdateAsync(Product product)
    {
         _context.Products.Update(product);
         return Task.CompletedTask;
    }
   public  Task DeleteAsync(Product product)
    {
     _context.Products.Remove(product);
      return   Task.CompletedTask;
        
    }

    public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }



}}