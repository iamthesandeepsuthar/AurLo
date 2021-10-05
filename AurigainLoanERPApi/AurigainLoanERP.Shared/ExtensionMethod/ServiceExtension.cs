using AurigainLoanERP.Shared.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Transactions;

namespace AurigainLoanERP.Shared.ExtensionMethod
{
    public static class ServiceExtension
    {
        private static IHostingEnvironment _hostingEnvironment;

        static ServiceExtension()
        {
            _hostingEnvironment = new HostingEnvironment();
        }
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

        public static string ToAbsoluteUrl(this string path)
        {

            return string.Concat(_hostingEnvironment.WebRootPath, path.Replace("~/", "/"));
             
        }


    }
}
