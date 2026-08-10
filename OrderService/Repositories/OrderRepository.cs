using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Model;

namespace OrderService.Repositories
{
    
public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context=context;
        } 
        public async Task<List<Order>> GetAllAsync()
        {
            return  await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
           
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
        
    }


}