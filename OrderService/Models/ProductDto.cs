namespace OrderService.Model
{
   public class ProductDto
    {
        public int Id{get;set;}
        public int ProductId{get;set;}
        public string Name{get;set;}=String.Empty;
        public int StockQuantity{get;set;}
        public decimal Price{get;set;}
    }
}