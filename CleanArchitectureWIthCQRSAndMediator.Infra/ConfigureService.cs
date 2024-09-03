using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using CleanArchitectureWIthCQRSAndMediator.Infrastructure.Data;
using CleanArchitectureWIthCQRSAndMediator.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWIthCQRSAndMediator.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BlogDBContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("praticeDBConnectionString"));
            });

            service.AddTransient<IBlogRepository, BlogRepository>();
            return service;
        }
    }
}
