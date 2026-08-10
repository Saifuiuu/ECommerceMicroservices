namespace OrderService.Model
{
    public class Order
    {
        public int Id {get;set;}
        public int ProductId {get;set;}
        public string Name{get;set;}=String.Empty;
        public int Quantity{get;set;}
        public decimal TotalPrice{get;set;}
        public string Status{get;set;}="pending";
        public DateTime CreatedAt{get;set;}=DateTime.UtcNow;


    }
}