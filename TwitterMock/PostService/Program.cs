using AutoMapper;
using EasyNetQ;
using Messaging;
using Microsoft.EntityFrameworkCore;
using PostService.Core.Entities;
using PostService.Core.Helper;
using PostService.Core.Repositories;
using PostService.Core.Repositories.Interfaces;
using PostService.Core.Services.DTOs;
using PostService.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(config =>
{
    //DTO to entity
    config.CreateMap<AddPostDTO, Post>();
    config.CreateMap<UpdatePostDTO, Post>();
}).CreateMapper();

builder.Services.AddSingleton(mapperConfig);
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("PostDb"));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService.Core.Services.PostService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();