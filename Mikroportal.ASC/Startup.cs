using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mikroportal.ACS.Services;
using Mikroportal.ACS.Services.Admin;
using Mikroportal.MODEL.ServiceContracts;
using Mikroportal.MODEL.ServiceContracts.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.ACS
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddScoped<ITechnicalDocumentService, TechnicalDocumentService>();
            services.AddScoped<IInstallationService, InstallationService>();
            services.AddScoped<IReplacementService, ReplacementService>();
            services.AddScoped<IRepairAndMaintenanceService, RepairAndMaintenanceService>();
            services.AddScoped<ICallCenterService, CallCenterService>();
            services.AddScoped<IReplacementPartService, ReplacementPartService>();
            services.AddScoped<IHomeService, HomeService>();

            services.AddScoped<IAdminHomeService, AdminHomeService>();
            services.AddScoped<IAdminReportService, AdminReportService>();
            services.AddScoped<IAdminCallCenterService, AdminCallCenterService>();
            services.AddScoped<IAddGuaranteeService, AddGuaranteeService>();
            services.AddScoped<IAddAuthorizedServiceService, AddAuthorizedServiceService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IAdminReplacementService, AdminReplacementService>();




            services.AddTransient<ClaimsPrincipal>(s =>
            s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix,
                opts => { opts.ResourcesPath = "Resources"; })
            .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("tr"),
                     new CultureInfo("de"),
                     new CultureInfo("es"),
                     new CultureInfo("fr")
                };

                opts.DefaultRequestCulture = new RequestCulture("en");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
                opts.RequestCultureProviders.Add(new QueryStringRequestCultureProvider() { QueryStringKey = "lang", UIQueryStringKey = "ui-lang" });
            });
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            });
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("JWTSettings:Issuer").Value,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["UserToken"];
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddHostedService<TimedHostedService>();


            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Globals.SetGlobals(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseRequestLocalization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "area/{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "LocalizedDefault",
                    template: "{lang:lang=tr}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{*catchall}",
                    defaults: new { controller = "Home", action = "RedirectToDefaultLanguage" });

                routes.MapRoute(
                  name: "areas",
                             template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                              );
            });


            app.UseCookiePolicy();
        }
    }
}
