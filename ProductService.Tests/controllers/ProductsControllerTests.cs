using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductService.Controllers;
using ProductService.Models;
using ProductService.Repositories;
using Xunit;

namespace ProductService.Tests.Controllers
{
    
public class ProductsControllerTest
    {
        [Fact]

        public async Task GetAll_ReturnsOkResults_WithListOFProducts()
        {
            
            var mockRepo=new Mock<IProductRepository>();

            var fakeProduct=new List<Product>
            {
                 new Product { Id = 1, Name = "Laptop", Price = 1200 },
                new Product { Id = 2, Name = "Mouse", Price = 20 }
            };
            

            mockRepo.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(fakeProduct);

            var controller=new ProductsController(mockRepo.Object);


            var result= await controller.GetAll();

            var okResult=Assert.IsType<OkObjectResult>(result.Result);

            var returnedProducts=Assert.IsType<List<Product>>(okResult.Value);

            Assert.Equal(2,returnedProducts.Count);
        }

      [Fact]

      public async Task GetByID_ReturnProduct()
        {
          var  MockRepo = new Mock<IProductRepository>();


var items=new Product { Id = 1, Name = "Laptop", Price = 1200 };

MockRepo.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync(items);





  var controller= new ProductsController(MockRepo.Object);

   var result= await controller.GetById(1);

   var okResult=Assert.IsType<OkObjectResult>(result.Result);

  var returnProducts=Assert.IsType<Product>(okResult.Value);

  Assert.Equal(1,returnProducts.Id);


        }




    }



    
}