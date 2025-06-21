using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.ExtensionMethods
{
    public static class CarterRegistrationExtensions
    {
        public static IServiceCollection AddCustomCarterModules(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var moduleTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

            foreach (var type in moduleTypes)
            {
                services.AddScoped(typeof(ICarterModule), type);
            }

            return services;
        }

        public static void MapCustomCarterModules(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var modules = scope.ServiceProvider.GetServices<ICarterModule>();

            foreach (var module in modules)
            {
                module.AddRoutes(app);
            }
        }
    }
}
