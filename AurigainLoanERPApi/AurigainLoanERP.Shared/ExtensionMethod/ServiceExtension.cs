using AurigainLoanERP.Shared.Attribute;
using AurigainLoanERP.Shared.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Transactions;

namespace AurigainLoanERP.Shared.ExtensionMethod
{
    public static class ServiceExtension
    {
        private static IHttpContextAccessor _httpContext;
        static ServiceExtension()
        {
            _httpContext = new HttpContextAccessor();
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

        public static string ToAbsolutePath(this string filePath)

        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    HttpRequest request = _httpContext.HttpContext.Request;

                    return string.Concat(request.IsHttps ? "https://" : "http://" , request.HttpContext.Request.Host.Value, filePath.Replace("~", "").Replace(@"\", @"/").Replace(@"//", @"/"));

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }


        }




    }
}
