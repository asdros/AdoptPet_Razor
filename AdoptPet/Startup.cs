using AdoptPet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdoptPet.Extensions;
using Microsoft.AspNetCore.Authorization;
using AdoptPet.Areas.Authorization;
using System.Globalization;

namespace AdoptPet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureSqlContext(Configuration);
            services.ConfigureLoggerService();
            services.ConfigureImageSaveService();
            services.AddAutoMapper(typeof(Startup));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.ConfigureDefaultIdentity();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.ConfigureAuthorization();
            services.ConfigureNotyfications();

            services.AddScoped<IAuthorizationHandler, OwnerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdministratorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ManagerAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var cultureInfo = new CultureInfo("pl-PL");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
