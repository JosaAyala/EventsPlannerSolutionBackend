using EventsPlanner.DomainContext.Repository;
using EventsPlanner.WebAPI.AppServices.CategoryEventServices;
using EventsPlanner.WebAPI.AppServices.RoleServices;
using EventsPlanner.WebAPI.AppServices.SubcategoryEventServices;
using EventsPlanner.WebAPI.AppServices.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<RoleAppService>();
            services.AddTransient<UserAppService>();
            services.AddTransient<CategoryEventAppService>();
            services.AddTransient<SubcategoryEventAppService>();

            return services;
        }
    }
}
