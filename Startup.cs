using Chatik.DataModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Chatik.Hubs;

namespace Chatik
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ChatikDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddIdentity<User, IdentityRole>(option =>
            {
                PasswordOptions passwordOption = option.Password;
                passwordOption.RequireDigit = false;
                passwordOption.RequiredLength = 5;
                passwordOption.RequiredUniqueChars = 0;
                passwordOption.RequireLowercase = false;
                passwordOption.RequireNonAlphanumeric = false;
                passwordOption.RequireUppercase = false;

            }).AddEntityFrameworkStores<ChatikDbContext>();
            services.AddSignalR(option =>
            {
                option.EnableDetailedErrors = true;
            });
            services.AddCors(option =>
            {
                option.AddPolicy("MyAllowSpecificOrigins", policy =>
                {
                    policy.AllowAnyOrigin();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatik", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatik v1"));
            }
            app.UseRouting();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Messager>("/messager");
            });
        }
    }
}
