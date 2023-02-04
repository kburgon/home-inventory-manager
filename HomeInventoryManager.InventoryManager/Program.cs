using HomeInventoryManager.InventoryManager.Data;
using HomeInventoryManager.InventoryManager.Data.Repositories;
using HomeInventoryManager.InventoryManager.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddControllers();
services.AddDbContextPool<InventoryDbContext>(
    options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
);

services.AddInMemorySubscriptions();

services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddMutationType<Mutation>()
        .AddSubscriptionType<Subscription>();

// services.AddCors(option => 
// {
//     option.AddPolicy("allowedOrigin",
//         builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.UseCors("allowedOrigin");
app.UseWebSockets();

app.MapControllers();

app.UseRouting()
    .UseEndpoints(endpoints => 
    {
        endpoints.MapGraphQL();
    });

app.Run();
