using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Ef
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddTodoEntityFramework(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<TodoContext>(opts =>
            //    opts.UseSqlServer(connectionString, b =>
            //        b.MigrationsAssembly(typeof(TodoContext).Assembly.FullName)    
            //    )
            //);

            services.AddDbContextFactory<TodoContext>(opts =>
                opts.UseSqlServer(connectionString, b =>
                    b.MigrationsAssembly(typeof(TodoContext).Assembly.FullName)
                )
            );

            services.AddScoped<TodoContext>(c => c.GetService<IDbContextFactory<TodoContext>>().CreateDbContext());

            return services;
        }
    }
}
