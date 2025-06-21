using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Application;
namespace BookStore.Persistence;
public static class BookStorePersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceBookStoreApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddBookStoreApplicationServices(configuration);
        return services;
    }
}
