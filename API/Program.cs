using API.Errors;
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
builder.Services.AddScoped<IProductRepository, ProductRepository>();
    //generic reposiotry service 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    //auto mapper service
builder.Services.AddAutoMapper(typeof(MappingProfiles));


//configure api controller attribute 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
   options.InvalidModelStateResponseFactory = ActionContext =>
   {
       var errors = ActionContext.ModelState
       .Where(e => e.Value.Errors.Count > 0)
       .SelectMany(x => x.Value.Errors)
       .Select(x => x.ErrorMessage).ToArray();

       var errorresponse = new ApiValidationErrorResponse
       {
           Errors = errors
       };

       return new BadRequestObjectResult(errorresponse);
   };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
