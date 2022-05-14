using API.Errors;
using API.Extensions;
using API.Helper;
using API.Middleware;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add DbContext 
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDBConnection")));

//Add Custome Services
//product repo service
//generic reposiotry service 
//auto mapper service
//configure api controller attribute 

//get the commentary services from the extension 
builder.Services.AddApplicationServices();

// configuring Swagger



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

// when no endpoint in the app matching the request, this middleware will redirect it to errorcontroller with 0 code as placeholder 
//which will generate ApiResponse object with 404 status code
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

StoreContextSeed.SeedAsync(app).Wait();
app.Run();
