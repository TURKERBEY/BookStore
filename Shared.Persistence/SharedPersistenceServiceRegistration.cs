using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Configurations.Security.Jwt;
using Shared.Core.Repositories;
using Shared.Persistence.Models;
using Shared.Persistence.Repositories;


namespace Shared.Persistence;
public static class SharedPersistenceServiceRegistration
{

    public static async Task<IServiceCollection> AddSharedModule(this IServiceCollection services, IConfiguration configuration)
    {
        var str= configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(str));
  
        services.AddScoped<ITenantRepository, TenantRepository>();

        services.AddScoped<IKullaniciRepository, KullaniciRepository>();
        services.AddScoped<IBookListRepository, BookListRepository>();
        return services;
    }




}
