using AurigainLoanERP.Shared.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AurigainLoanERP.Shared.ExtensionMethod
{
   public static class ServiceExtension
    {
        public static IServiceCollection AllowCors(this IServiceCollection services, params string[] origins)
        {
            services.AddCors(setupAction => setupAction.AddPolicy(Constants.ALLOW_ALL_ORIGINS,
                options =>
                    options.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            ));
            return services;
        }
    }
}
