using API.Extensions;
using API.Middleware;
using Core.Identity;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add Store DbContext 
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDBConnection")));

//add Identity DbContext
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDBConnection")));

// add in memory Redis 
builder.Services.AddSingleton<IConnectionMultiplexer>(c => {
    var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(config);
});

//Add Custome Services
//product repo service
//generic reposiotry service 
//auto mapper service
//configure api controller attribute 

//get the commentary services from the extension 
builder.Services.AddApplicationServices();

// add IdentityDB Service 
builder.Services.AddIdentityServices(builder.Configuration);

// configuring Swagger
builder.Services.AddSwaggerDocumentation();



// add cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

//Use Swagger From Extension 
app.UseSwaggerDocumentation();

// when no endpoint in the app matching the request, this middleware will redirect it to errorcontroller with 0 code as placeholder 
//which will generate ApiResponse object with 404 status code
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

StoreContextSeed.SeedAsync(app).Wait();
AppIdentityDbContextSeed.SeedUsersAsync(app).Wait();
app.Run();
