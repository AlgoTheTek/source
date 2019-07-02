

namespace AirportInformations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AirportInformations.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AirportInformations.Repository.Models;
    using AirportInformations.Repository.Contracts;
    using AirportInformations.ConcretService.Services;

    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigFile.Instance.Url = configuration.GetSection("Url:value").Value;
            ConfigFile.Instance.localDbPath = configuration.GetSection("localDataBase:value").Value;
            ConfigFile.Instance.localEuCountries = configuration.GetSection("localcountries:value").Value;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<ICountryRepository, Country>();
            services.AddSingleton<IHttpManager, HttpManager>();
            services.AddSingleton<ICacheStorage, LiteLinqDataBase>();


           }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
