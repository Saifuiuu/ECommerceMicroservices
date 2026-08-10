using Microsoft.AspNetCore.Mvc;//by this we can use contrller
using Microsoft.EntityFrameworkCore;
using ProductService.Models;
using ProductService.Repositories;//by this we can use Prodcut model 

namespace ProductService.Controllers//define namespace for controller
{
    [ApiController]//this is attribute which tell framework that this in api controller
    [Route("/api/[controller]")]//this attribte defin route the request /api/Prodct cam to this controller


    public class ProductsController : ControllerBase
    {
        
private readonly IProductRepository _repository;


public ProductsController(IProductRepository repository)
        {
           _repository=repository;
        }
        

        [HttpGet]
public async Task<ActionResult<IEnumerable<Product>>> GetAll()
{
    var products = await _repository.GetAllAsync();

    return Ok(products);
}

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product= await _repository.GetByIdAsync(id);
             

            if (product==null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

    [HttpPost]

    public async Task<ActionResult<Product>> create(Product product)
        {
            product.CreatedAt=DateTime.UtcNow;
            await _repository.AddAsync(product);

            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),new{id=product.Id},product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id,Product updatedProduct)
        {
            var product= await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            
            product.Name=updatedProduct.Name;
            product.Description=updatedProduct.Description;
            product.StockQuantity=updatedProduct.StockQuantity;
            product.Description=updatedProduct.Description;
            product.Price=updatedProduct.Price;

            await _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();
            return NoContent();//return 204 status code with no data to make it simple

        }

    [HttpDelete("{id}")]
    public async Task<IActionResult> delete(int id)
        {
            var product= await _repository.GetByIdAsync(id);

            if (product == null)
              return NotFound();
            

            await _repository.DeleteAsync(product);

            await _repository.SaveChangesAsync();
            
            return NoContent();



        }


    }

}