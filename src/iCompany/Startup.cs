using Gemini.AspNetCore;
using iCompany.Areas.Shared.Models;
using iCompany.Configs;
using iCompany.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace iCompany
{
    public class Startup
    {
        private IConfigurationRoot config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            var configFiles = Directory.GetFiles($"{env.WebRootPath}{Path.DirectorySeparatorChar}configs{Path.DirectorySeparatorChar}default");
            foreach (var file in configFiles)
            {
                builder.AddJsonFile(file, optional: false, reloadOnChange: true);
            }
            builder.AddJsonFile($"{env.WebRootPath}{Path.DirectorySeparatorChar}configs{Path.DirectorySeparatorChar}db.json", optional: false, reloadOnChange: true);
            var defaultValue = builder.Build();

            builder = new ConfigurationBuilder();

            configFiles = Directory.GetFiles($"{env.WebRootPath}{Path.DirectorySeparatorChar}configs");
            foreach (var file in configFiles)
            {
                builder.AddJsonFile(file, optional: false, reloadOnChange: true);
            }

            builder.AddDbConfig(defaultValue);

            config = builder.Build();
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<VisualConfigs>(config.GetSection("VisualConfigs"));
            services.Configure<DbConfigs>(config.GetSection("DbConfigs"));

            services.AddTransient<CompanyDbContext>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddDistributedMemoryCache();
            services.AddSiteMap();
            services.AddMvc();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDirectoryBrowser();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                loggerFactory.AddConsole();
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{area:exists}/{controller}/{action=Index}");
            });
        }
    }
}
