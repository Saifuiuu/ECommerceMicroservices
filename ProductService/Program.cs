
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;



var builder = WebApplication.CreateBuilder(args);


//builder object which define strucutre and things our app needed it contain services DI container ,configuration logging etc

var useAzureDB= builder.Configuration["UseAzureSql"]=="true";

var ConnectionString= builder.Configuration.GetConnectionString("DefaultConnection");
// Now we are telling the builder to things we need like controller 

builder.Services.AddControllers();
//we need api end point which swagger uses 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDBContext>(Options =>
{
   if(useAzureDB){
    Options.UseSqlServer(ConnectionString);}
    else{
      Options.UseSqlite(ConnectionString);
    } 
}

);



builder.Services.AddScoped<IProductRepository,ProductRepositories>();

//now the actual app is building 
var app = builder.Build();


using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDBContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//this middleware redirect http to https
app.UseHttpsRedirection();
//this middleware is for authorization if endpoint is [protected] then this middleware check authorization 
app.UseAuthorization();
//this helps the request to map the desrie controller 
app.MapControllers();
//start the server 
app.Run();
