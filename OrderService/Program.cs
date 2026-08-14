using OrderService.Model;
using OrderService.Service;
using OrderService.Data;
using OrderService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var useAzureDB= builder.Configuration["UseAzureSql"]=="true";

var ConnectionString= builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var ProductServiceUrl=builder.Configuration["ProductServiceUrl"]?? "http://localhost:5xx";

builder.Services.AddHttpClient<IProductServiceClient,ProductServiceClient>(client=>
client.BaseAddress=new Uri(ProductServiceUrl)
);

builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddDbContext<OrderDbContext>(Options =>
{
   if(useAzureDB){
    Options.UseSqlServer(ConnectionString);}
    else{
      Options.UseSqlite(ConnectionString);
    } 
});

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
