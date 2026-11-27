<<<<<<< HEAD
using HotelManagement.API.Services.Interfaces;
using HotelManagement.API.Services.Implementations;
=======
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;



>>>>>>> a181239108ee42d7f66fba846690467727e33844
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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
