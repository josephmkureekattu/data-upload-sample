using Core.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.IOC
{
    public static class Dependency
    {
        public static IServiceCollection RegisterCoreDependency(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(assemblies);
            services.AddAutoMapper(typeof(ProfileMapper).GetTypeInfo().Assembly);
            return services;
        }
    }
}
