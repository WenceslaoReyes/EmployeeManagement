using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration conf)
        {
            _config = conf;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            /*Confugure SqlServer as database provider*/
            services.AddDbContextPool<AppDbContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            /*Adding Identity service*/
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.Configure<IdentityOptions>(options => options.Password.RequiredLength = 8);
            /**** It is to add MVC services in ASP.NET CORE 2.2   ****/
            Action<MvcOptions> opt = o => o.EnableEndpointRouting = false;
            services.AddMvc(opt);
            /*Registering a service to implement dependecy injection*/
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            /****   ****/
            /**** It is to add MVC services in ASP.NET CORE 3   ****/
            //services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions() { SourceCodeLineCount = 10 };
                app.UseDeveloperExceptionPage(/*developerExceptionPageOptions*/);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //app.UseDefaultFiles();
            app.UseStaticFiles();

            //Combination of UseDefaultFiles and UseStaticFiles middlewares
            //app.UseFileServer();

            /**** It is to add MVC middleware in ASP.NET CORE 2.2 "Conventional routing" ****/
            //app.UseMvcWithDefaultRoute();

            //Use Authentication middleware once Identity is done
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc();
            app.UseRouting();


            #region app.Use
            //app.Use( async (context,next) => {
            //    //await context.Response.WriteAsync("Hi from first run middleware");
            //    logger.LogInformation("Hi from first logger");
            //    await next();
            //    logger.LogInformation("Hi from first logger (2)");
            //});

            //app.Use(async (context,next) => {
            //    logger.LogInformation("Hi from second logger");
            //    //await context.Response.WriteAsync("Hi from second run middleware");
            //    await next();
            //    logger.LogInformation("Hi from second logger(2)");
            //});

            //app.Run(async (context) => {
            //    await context.Response.WriteAsync("Hi from third run middleware");
            //    logger.LogInformation("Hi from third logger(2)");
            //});
            #endregion

            #region RunMiddlewareSyntax
            //app.Run(async (context) =>
            //{
            //    //throw new Exception("An error occurred in this mvc application");
            //    await context.Response.WriteAsync(env.EnvironmentName);
            //});
            #endregion

            /**** It is to add MVC middleware in ASP.NET CORE 3   ****/
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //    //throw new Exception("An error occurred in this mvc application");
            //    //endpoints.MapGet("/", async context =>
            //    //{

            //    //    //It is in order to get current process which hosts an runs web app.
            //    //    //System.Diagnostics.Process.GetCurrentProcess().ProcessName
            //    //    await context.Response.WriteAsync(_config["MyKey"]);
            //    //});
            //});
        }
    }
}
