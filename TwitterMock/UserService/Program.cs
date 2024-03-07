using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Configs;
using UserService.Core.Entities;
using UserService.Core.Helper;
using UserService.Core.Repositories;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services.Dtos;
using UserService.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(config =>
{
    //DTO to entity
    config.CreateMap<UpdateUserDTO, User>();
    config.CreateMap<CreateUserDTO, User>();
    config.CreateMap<User, GetUserDTO>();
}).CreateMapper();

builder.Services.AddSingleton(mapperConfig);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("UserDB"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService.Core.Services.UserService>();
builder.Services.ConfigureDi();

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