using OrderService.Model;

namespace OrderService.Service{
public interface IProductServiceClient
{
    Task<ProductDto?> GetProductByIdAsync(int productId);
    
}
}