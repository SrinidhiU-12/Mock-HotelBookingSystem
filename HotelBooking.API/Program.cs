using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Repositories.Implementations;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repository registrations
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

builder.Services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
