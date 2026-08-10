using OrderService.Model;
namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        public Task<List<Order?>> GetAllAsync();
        public Task<Order?> GetByIdAsync(int orderId);

        public Task Create(Order order);

        public Task<bool> SaveChangesAsync();
    }
}