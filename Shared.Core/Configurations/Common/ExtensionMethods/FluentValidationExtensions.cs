using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Configurations.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.ExtensionMethods;
public static class FluentValidationExtensions
{
    public static IServiceCollection AddValidatorsFromAssemblyExcludingMarked(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<Type> enumerable = from type in assembly.GetTypes()
                                       where typeof(IValidator).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface
                                       where type.GetCustomAttribute<ExcludeFromRegistrationAttribute>() == null
                                       select type;
        foreach (Type item in enumerable)
        {
            Type serviceType = item.GetInterfaces().First((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
            services.AddScoped(serviceType, item);
        }

        return services;
    }
}