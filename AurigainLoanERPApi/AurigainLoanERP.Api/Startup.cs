using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Services;
using AurigainLoanERP.Services.Account;
using AurigainLoanERP.Services.Common;
using AurigainLoanERP.Services.KycDocumentType;
using AurigainLoanERP.Services.PaymentMode;
using AurigainLoanERP.Services.Qualification;
using AurigainLoanERP.Services.StateAndDistrict;
using AurigainLoanERP.Services.User;
using AurigainLoanERP.Services.UserRoles;
using AurigainLoanERP.Shared.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AurigainLoanERP.Api
{
    public class Startup
    {




        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        const string JWT_Key = "Jwt:Key";
        const string JWT_ISSUER = "Jwt:Issuer";
        const string CONNECTION_STRING = Constants.CONNECTION_STRING;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aurigain", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header, },
            new List<string>()
          }
        });

            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration[JWT_ISSUER],
                    ValidAudience = Configuration[JWT_ISSUER],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[JWT_Key]))
                };
            });
            services.AddDbContext<AurigainContext>(options => options.UseSqlServer(Configuration[CONNECTION_STRING]));
            services.AddCors(setupAction => setupAction.AddPolicy(Constants.ALLOW_ALL_ORIGINS, options =>
                options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())); // allow CORS
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            RegisterServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aurigain Loan ERP (v1)"));

            }
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUserRoleService, UserRoleService>().Reverse();
            services.AddTransient<ICommonService, CommonService>().Reverse();
            services.AddTransient<IStateAndDistrictService, StateAndDistrictSrivce>().Reverse();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>().Reverse();
            services.AddTransient<IQualificationService, QualificationService>().Reverse();
            services.AddTransient<IPaymentModeService, PaymentModeService>().Reverse();
            services.AddTransient<IAccountService, AccountService>().Reverse();
            services.AddTransient<IUserService, UserService>().Reverse();


        }
    }
}
