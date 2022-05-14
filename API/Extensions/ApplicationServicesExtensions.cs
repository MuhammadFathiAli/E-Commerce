using API.Errors;
using API.Helper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            //Add Custome Services
            //product repo service
            Services.AddScoped<IProductRepository, ProductRepository>();
            //generic reposiotry service 
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //auto mapper service
            Services.AddAutoMapper(typeof(MappingProfiles));
            //configure api controller attribute 
            Services.Configure<ApiBehaviorOptions>(options =>
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

            return Services;
        }
    }
}
