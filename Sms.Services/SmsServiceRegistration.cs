using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sms.Services.SmsHelper;


namespace Sms.Services;
public static class SmsServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
     
        services.AddScoped<ISmsSender, SmsSender>();
        return services;
    }

}
