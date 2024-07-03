using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApplication1.Application.Services.AmountProduct;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Application.Services.StatusOrder;
using WebApplication1.Infrastructure;
using WebApplication1.Infrastructure.Pipeline;

namespace WebApplication1.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }); ;
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();




            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));


            services.AddTransient<DbContext, DataContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IAmountProducts, AmountProducts>();
            services.AddTransient<IStatusOrder, StatusOrder>();
            services.AddTransient<OrderServiceHandler>();


            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
