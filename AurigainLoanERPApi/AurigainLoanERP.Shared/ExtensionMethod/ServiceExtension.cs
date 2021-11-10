using AurigainLoanERP.Shared.Attribute;
using AurigainLoanERP.Shared.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
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

        public static bool IsBase64(this string base64String)
        {
             
            try
            {
                base64String = Regex.Replace(base64String, @"^\s*$\n", string.Empty, RegexOptions.Multiline).TrimEnd();

              
                if (base64String.Split(';').Length > 0)
                {
                    string[] Fileinfo = base64String.Split(';');
                    base64String = Fileinfo[1].Substring(Fileinfo[1].IndexOf(',') + 1);
                }

                if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                { return false; }


                Convert.FromBase64String(base64String);
                return true;
            }
            catch 
            {
                 return false;
            }
           
        }



    }
}
