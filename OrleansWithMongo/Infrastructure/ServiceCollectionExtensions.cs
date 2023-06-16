using OrleansWithMongo.Grains.Abstactions;
using OrleansWithMongo.Grains.Implementations;
using System.Reflection;

namespace OrleansWithMongo.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {                 
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                })
                .AddRouting(options => options.LowercaseUrls = true)
                .AddTransient<IHelloWorldLocalGrain, HelloWorldLocalGrain>();

            return services;
        }
    }
}
