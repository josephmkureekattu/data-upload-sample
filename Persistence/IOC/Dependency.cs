using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IOC
{
    public static class Dependency
    {
        public static void RegisterPersistenceDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(configuration["DBConnectionString"],
                  b =>
                  {
                      b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                      b.CommandTimeout(1800);

                  })); // will be created in web project root

            using (var scope = serviceCollection.BuildServiceProvider().CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dataContext.Database.Migrate();
            }
            var assembly = Assembly.GetExecutingAssembly();
            serviceCollection
              .AddMediatR(assembly);
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
    }
}
