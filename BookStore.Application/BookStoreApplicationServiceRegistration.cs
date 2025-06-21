using BookStore.Application.Features.KullaniciIslemleri;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Configurations.Common.Rules;
 using Shared.Core.Configurations.Common.ExtensionMethods;
using System.Reflection;
using Carter;
using Shared.Core.Configurations.Common.Validation;
using Shared.Core.Configurations.Common.Pipelines.Transaction;
using BookStore.Contracts.TenantIslemleri.Command;
using BookStore.Application.Features.TenantIslemleri;
using BookStore.Contracts.KullaniciIslemleri.Command;
using BookStore.Application.Features.TenantIslemleri.Command;
using BookStore.Application.Features.KullaniciIslemleri.Command;
using BookStore.Contracts.BookStoreIslemleri.Command;
using BookStore.Application.Features.BookStoreIslemleri.Commands;


namespace BookStore.Application;
public static class BookStoreApplicationServiceRegistration
{
    public static IServiceCollection AddBookStoreApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(ICarterModule));
        //services.AddValidatorsFromAssemblyExcludingMarked(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

      
        AddValidatorsService(services, configuration);

        return services;
    }
    private static void AddValidatorsService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IValidator<SaveTenantCommand>>(provider =>
        {
            return new SaveTenantValidator();
        });

        services.AddScoped<IValidator<SaveKullaniciCommand>>(provider =>
        {
            return new SaveKullaniciValidator();
        });

        services.AddScoped<IValidator<LoginCommand>>(provider =>
        {
            return new LoginValidator();
        });

        services.AddScoped<IValidator<SaveBookCommand>>(provider =>
        {
            return new SaveBookValidator();
        });

    }
    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
   
}
