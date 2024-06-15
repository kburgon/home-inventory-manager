using HomeInventoryManager.Commands;
using HomeInventoryManager.Data.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetProductsQueryHandler>());
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/products/all", async (IMediator mediator) =>
{
	var query = new GetProductsQuery();
	var result = await mediator.Send(query);
	return result;
})
.WithName("GetAllProducts")
.WithOpenApi();

app.Run();
