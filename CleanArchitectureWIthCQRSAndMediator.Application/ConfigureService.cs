using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitectureWIthCQRSAndMediator.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(mr =>
            {
                mr.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                //Validation
                mr.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
