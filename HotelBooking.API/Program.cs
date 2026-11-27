<<<<<<< HEAD
using HotelManagement.API.Services.Interfaces;
using HotelManagement.API.Services.Implementations;
=======
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Repositories.Implementations;



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
