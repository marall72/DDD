using Customers.Application.CreateCustomer;
using Customers.Application.DeleteCustomer;
using Customers.Application.GetAllCustomers;
using Customers.Application.GetCustomerById;
using Customers.Data;
using Customers.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<CreateCustomerHandler>();
builder.Services.AddScoped<DeleteCustomerHandler>();
builder.Services.AddScoped<GetCustomerByIdHandler>();
builder.Services.AddScoped<GetAllCustomersHandler>();

builder.Services.AddControllers();

builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD API", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
