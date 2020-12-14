using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;
using Mikroportal.PP.Services;
using Mikroportal.UI.Services;

namespace Mikroportal.PP
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
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

            services.AddScoped<IPP_OTService, PP_OTService>();
            //services.AddScoped<ILoginService, LoginService>();

            //services.AddScoped<IDashboardService, DashboardService>();

            services.AddHostedService<TimedHostedService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Globals.SetGlobals(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();


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

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //     FileProvider = new PhysicalFileProvider(@"C:\Files\ACS\Services"),
            //    RequestPath = new PathString("/StaticFiles")
            //});

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PP_OT}/{action=Index}/{id?}");
            });
        }
    }
}
