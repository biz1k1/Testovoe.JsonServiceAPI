using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Application.Services;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Infrastructure;

namespace WebApplication1.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            #region Default services

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }); ;
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            #endregion

            #region Connection

            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));

            #endregion

            #region DI

            services.AddTransient<DbContext, DataContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IAmountProduct, AmountProduct>();
            services.AddTransient<IStatusOrder, StatusOrder>();
            services.AddTransient<OrderServiceHandler>();

            #endregion

            return services;
        }
    }
}
