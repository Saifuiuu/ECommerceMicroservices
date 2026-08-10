using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderService.Model;
using OrderService.Repositories;
using OrderService.Service;

namespace OrderService.Controller{

[ApiController]
[Route("api/[controller]")]
 public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repo;
    private readonly IProductServiceClient _client;
    public OrdersController(IOrderRepository repo,IProductServiceClient client)
    {
        _repo=repo;
        _client=client;
    }
    [HttpGet]
    public async  Task<ActionResult<IEnumerable<Order>>> GetAll()
    {
        var Orders= await _repo.GetAllAsync();

        return Ok(Orders);
    }
[HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(int id)
    {
        var order=await _repo.GetByIdAsync(id);
       
        if (order == null)
        return NotFound();

        return Ok(order);
    }

[HttpPost]
    public async Task<ActionResult<Order>> Create(CreateOrderDto request)
    {
        var product= await _client.GetProductByIdAsync(request.ProductId);
        Console.WriteLine(request.ProductId);

        if (product == null)
        {
            return BadRequest($"Product with Id :${request.ProductId} does not exist");
        }

        if (product.StockQuantity < request.Quantity)
        {
            return BadRequest($"Insufficient stock!");
        }

    var order = new Order
    {
        ProductId=request.ProductId,
        Name=product.Name,
        TotalPrice=product.Price*request.Quantity,
        Status="Confirmed",
        CreatedAt=DateTime.UtcNow,
        Quantity=request.Quantity
    };

    await _repo.Create(order);
    await _repo.SaveChangesAsync();

    return CreatedAtAction(nameof (GetById),new {id=order.Id},order);

    }



}}