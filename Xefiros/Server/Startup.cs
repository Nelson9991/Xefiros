using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Xefiros.DataAccess;
using Xefiros.DataAccess.Data.Repository;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.DataAccess.MappingConf;
using Xefiros.DataAccess.Services;
using Xefiros.DataAccess.Services.IServices;
using Xefiros.Server.Helpers;
using Xefiros.Server.Services;
using Xefiros.Shared.Models;
using System.Security.Claims;
using Xefiros.Server.Services.IServices;

namespace Xefiros.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MapperProfile()); });
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddProfileService<IdentityProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.IgnoreReadOnlyFields = true;
                    opts.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                });

            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureModelBindingLocalization>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileUpload, AzureFileUpload>();
            services.AddScoped<IDbInitializer, DbInitializer>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            var supportedCultures = new[] {"es-EC", "en-US", "es", "en"};
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[2])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            dbInitializer.Initialize();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}