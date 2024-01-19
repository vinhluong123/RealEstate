using Microsoft.EntityFrameworkCore;
using RealEstateServices.DBContexts;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Add services config for Dbcontext
/// </summary>
builder.Services.AddDbContext<RealEstateContext>(options =>
        options.UseSqlServer("Server=VINHLUONG-LAPTO\\VINHLUONG_SERVER;Database=RealEstateDb;User Id=sa;Password=12345678x@X"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


