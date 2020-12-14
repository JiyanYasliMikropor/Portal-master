using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mikroportal.DATA;
using Mikroportal.API.Services;
using Mikroportal.MODEL.ServiceContracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Mikroportal.API.Services.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;
using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using Mikroportal.API.Services.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;
using Mikroportal.API.Services.PP.PP_OT;
using Mikroportal.MODEL.ServiceContracts.MES;
using Mikroportal.API.Services.MES;
using Mikroportal.MODEL.ServiceContracts.CustomerPortal;
using Mikroportal.API.Services.CustomerPortal;


namespace Mikroportal.API
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
            var ConnectionString = Configuration.GetConnectionString("MikroConn");
            // buradaki distributed memorycache kýsmýný deðiþtirerek session'ý redistede tutabiliriz
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services
                .AddControllers()
                .AddNewtonsoftJson();

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});



            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddDbContext<SpDataContext>(options => options.UseSqlServer(ConnectionString));
            services.AddScoped<IMikroportalUOW, MikroportalUOW>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IInstallationService, InstallationService>();
            services.AddScoped<IReplacementService, ReplacementService>();
            services.AddScoped<IRepairAndMaintenanceService, RepairAndMaintenanceService>();
            services.AddScoped<ICallCenterService, CallCenterService>();
            services.AddScoped<ITechnicalDocumentService, TechnicalDocumentService>();
            services.AddScoped<IHomeService, HomeService>();

            services.AddScoped<IAdminHomeService, AdminHomeService>();
            services.AddScoped<IAdminReportService, AdminReportService>();
            services.AddScoped<IAdminCallCenterService, AdminCallCenterService>();
            services.AddScoped<IAddGuaranteeService, AddGuaranteeService>();
            services.AddScoped<IAddAuthorizedServiceService, AddAuthorizedServiceService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IAdminReplacementService, AdminReplacementService>();
            services.AddScoped<IReplacementPartService, ReplacementPartService>();


            services.AddScoped<IPP_OTService, PP_OTService>();

            services.AddScoped<IMesHomeService, MesHomeService>();
            services.AddScoped<IAndonService, AndonService>();
            services.AddScoped<IScrapService, ScrapService>();

            services.AddScoped<IMesCogiService, MesCogiService>();  

            services.AddScoped<ISalesOrdersService, SalesOrdersService>();
       

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
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(options => options.AllowAnyOrigin());

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                //x.AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                //var allowedOrigins = Configuration.GetSection("AllowedOrigins").Value.ToString().Split(";");
                //x.WithOrigins(allowedOrigins);
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
