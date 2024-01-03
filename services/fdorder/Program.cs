using fdorder;
using fdorder.DataAccess;
using fdorder.Domain.Ports;
using fdorder.Domain.Service;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderDomainService, OrderDomainService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<OrderContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();
