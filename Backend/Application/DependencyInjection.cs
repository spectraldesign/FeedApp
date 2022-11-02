using Application.Extentions;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IIotDeviceRepository, IoTDeviceRepository>();
            services.AddTransient<UserManager<User>>();
            services.AddTransient<IGenericExtension, GenericExtensions>();
            return services;
        }
    }
}
