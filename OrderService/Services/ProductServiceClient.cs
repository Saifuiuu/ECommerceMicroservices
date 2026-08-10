 using OrderService.Model;
 namespace  OrderService.Service{
    public class ProductServiceClient : IProductServiceClient
{
   private readonly HttpClient _httpClient;

   public ProductServiceClient(HttpClient httpClient)
    {
        _httpClient=httpClient;
    }


     public async Task<ProductDto?> GetProductByIdAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");

    if(!response.IsSuccessStatusCode)
    return null;


    var product=await  response.Content.ReadFromJsonAsync<ProductDto>();
    return product;

    }
}
    
 }

