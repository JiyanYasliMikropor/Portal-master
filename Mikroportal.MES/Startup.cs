using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Mikroportal.MES.Services;
using Mikroportal.MODEL.ServiceContracts.MES;
using Mikroportal.UI;
using Mikroportal.UI.Services;

namespace Mikroportal.MES
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
                options.IdleTimeout = TimeSpan.FromDays(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
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

            services.AddScoped<IScrapService, ScrapService>();
            services.AddScoped<IAndonService, AndonService>();
            services.AddScoped<IMesHomeService, MesHomeService>();
            services.AddScoped<IMesCogiService, MesCogiService>();

            services.AddHttpContextAccessor();
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

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseRequestLocalization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=MesHome}/{action=Index}/{id?}");
            });

        }
    }
}
