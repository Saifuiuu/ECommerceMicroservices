using Microsoft.AspNetCore.Http.HttpResults;
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

      public async Task GetByID_ReturnProduct_WhenExist()
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




    

[Fact]
public async Task GetById_ReturnNotFound_WhenProductNotExist()
        {
            var mockRepo=new Mock<IProductRepository>();


            mockRepo.Setup(repo=>repo.GetByIdAsync(99)).ReturnsAsync((Product?)null);


            var controller=new ProductsController(mockRepo.Object);


            var result=await controller.GetById(99);


            Assert.IsType<NotFoundResult>(result.Result);

            
        }


        [Fact]


        public async Task Create_ReturnActionCreated_WhenProductIsValid()
        {
            var mockRepo=new Mock<IProductRepository>();

            var product=new Product{Id = 3, Name = "Keyboard", Price = 80};

            mockRepo.Setup(repo=>repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            mockRepo.Setup(repo=>repo.SaveChangesAsync()).ReturnsAsync(true);

            var controller=new ProductsController(mockRepo.Object);

            var result= await controller.create(product);


    Assert.IsType<CreatedAtActionResult>(result.Result);
    mockRepo.Verify(repo=>repo.AddAsync(It.IsAny<Product>()),Times.Once);
    mockRepo.Verify(repo=>repo.SaveChangesAsync(),Times.Once);


        }


[Fact]


public async Task Update_ReturnNoContent_WhenProductExist()
        {
            var mockRepo=new Mock<IProductRepository>();

var product=new Product{Id = 3, Name = "Keyboard", Price = 80};

            mockRepo.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync(product);
            mockRepo.Setup(repo=>repo.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            mockRepo.Setup(repo=>repo.SaveChangesAsync()).ReturnsAsync(true);


            var controller=new ProductsController(mockRepo.Object);

            var result= await controller.update(1,product);


           Assert.IsType<NoContentResult>(result);

           mockRepo.Verify(repo=>repo.SaveChangesAsync(),Times.Once);


        }


        [Fact]

        public async Task Delete_ReturnNOContent_WhenProductExist()
        {
            var product=new Product{Id = 3, Name = "Keyboard", Price = 80};
            var mockRepo=new Mock<IProductRepository>();

            mockRepo.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync(product);
            mockRepo.Setup(repo=>repo.DeleteAsync(product)).Returns(Task.CompletedTask);
            mockRepo.Setup(repo=>repo.SaveChangesAsync()).ReturnsAsync(true);

            var controller=new ProductsController(mockRepo.Object);

            var result=await controller.delete(1);

            Assert.IsType<NoContentResult>(result);
            mockRepo.Verify(repo=>repo.DeleteAsync(product),Times.Once);

            
        }

    
}
}