using Moq;
using OrderService.Service;
using OrderService.Model;
using OrderService.Repositories;
using OrderService.Controller;
using Microsoft.AspNetCore.Mvc;



namespace OrderService.Test.controller
{
    public class OrdersControllerTest
    {
        [Fact]
        public async Task Create_ReturnCreatedOrder_WhenProductExistAndStockAvailable()
        {
            
            var client = new Mock<IProductServiceClient>();
            var OrderRepo = new Mock<IOrderRepository>();

            var product = new ProductDto
            {
                Id = 1,
                ProductId = 1,
                Name = "gaming Chair",
                StockQuantity = 5,
                Price = 350
            };


            client.Setup(client => client.GetProductByIdAsync(1)).ReturnsAsync(product);
            OrderRepo.Setup(repo => repo.Create(It.IsAny<Order>())).Returns(Task.CompletedTask);
            OrderRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var controller = new OrdersController(OrderRepo.Object, client.Object);
            var orderDto = new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 2
            };

            var result = await controller.Create(orderDto);

            var okresult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var order = Assert.IsType<Order>(okresult.Value);

            Assert.Equal(700, order.TotalPrice);
            Assert.Equal("Confirmed", order.Status);

        }



        [Fact]

        public async Task create_ReturnBadRequest_WhenProductDoesNotExist()
        {
            var client = new Mock<IProductServiceClient>();
            var OrderRepo = new Mock<IOrderRepository>();

            client.Setup(client => client.GetProductByIdAsync(13)).ReturnsAsync((ProductDto?)null);
            
            var controller = new OrdersController(OrderRepo.Object, client.Object);
            var orderDto = new CreateOrderDto
            {
                ProductId = 13,
                Quantity = 2
            };

            var result = await controller.Create(orderDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);

            OrderRepo.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Never);

        }
        [Fact]
        public async Task Create_ReturBadRequest_WhenStockQuantityIsInsufficent()
        {

            var client = new Mock<IProductServiceClient>();
            var OrderRepo = new Mock<IOrderRepository>();

            var fakeProduct = new ProductDto
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200,
                StockQuantity = 5
            };
            client.Setup(client => client.GetProductByIdAsync(1)).ReturnsAsync(fakeProduct);

            var controller = new OrdersController(OrderRepo.Object, client.Object);
            var orderDto = new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 50
            };

            var result = await controller.Create(orderDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            OrderRepo.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Never);

        }





    }
}