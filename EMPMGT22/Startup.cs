using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMPMGT22.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace EMPMGT22
{
    public class Startup
    {
        private IConfiguration _config;

        //private IConfiguration _config;

        //public Startup( IConfiguration config )
        //{
        //    _config = config;
        //}

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            //services.AddIdentity<IdentityUser, IdentityRole>().
            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })                
                .AddXmlSerializerFormatters();




            //services.AddSingleton<IEmployeeRpository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRpository, MockEmployeeRepository>();
            //services.AddTransient<IEmployeeRpository, MockEmployeeRepository>();
            //services.AddSingleton<IEmployeeRpository, SQLEmployeeRepository>();
            //services.AddTransient<IEmployeeRpository, SQLEmployeeRepository>();
            services.AddScoped<IEmployeeRpository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    //app.UseStatusCodePages();
            //    app.UseStatusCodePagesWithRedirects("/Error/{0}");
            //}
            else
            {
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");



                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
                //app.UseExceptionHandler("/Error/{0}");
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            //if (env.IsDevelopment())
            //{
            //    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();

            //    developerExceptionPageOptions.SourceCodeLineCount = 10;
            //    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            //}

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer(fileServerOptions);






            //FileServerOptions filesrveroptions = new FileServerOptions();
            //filesrveroptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //filesrveroptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer();

            //DefaultFilesOptions defaultfileoptions = new DefaultFilesOptions();
            //defaultfileoptions.DefaultFileNames.Clear();
            //defaultfileoptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultfileoptions);
            //app.UseStaticFiles();


            //DefaultFilesOptions defaultfileoptions = new DefaultFilesOptions();
            //defaultfileoptions.DefaultFileNames.Clear();
            //defaultfileoptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer();







            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add


            //app.UseDefaultFiles();



            //DefaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer();
            //app.UseDefaultFiles(defaultFilesOptions);
            //app.UseStaticFiles();

            //  app.Use(async (context, next) =>
            // {
            //    logger.LogInformation("MW1: Incoming Request");
            //     await next();
            //    logger.LogInformation("MW1:OUTGOING REQ ");

            // });


            //   app.Use(async (context, next) =>
            //  {
            //     logger.LogInformation("MW2: Incoming Request");
            //    await next();
            //     logger.LogInformation("MW2:OUTGOING REQ ");

            // });




            // app.Use(async (context,next) =>
            // {
            // await context.Response.WriteAsync
            // (System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            // await context.Response.WriteAsync ("Hello from first middleware world !!");
            //  await next();
            //await context.Response.WriteAsync(_config["MyKey"]);
            // });

            //app.UseStaticFiles();
            //app.UseFileServer();

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute("Default", "{Controller=Home}/{action=Index}/{id?}"));

            //app.UseMvc(routes => routes.MapRoute("Default", "samikshya/{Controller=Home}/{action=Index}/{id?}"));

            //app.Run(async (context) =>
            //{
            //    // await context.Response.WriteAsync
            //    // (System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            //    //throw new Exception("some error processing request");
            //    //await context.Response.WriteAsync("Hello from second middleware world !!");
            //    //await context.Response.WriteAsync("Hosting environment :: !!"+env.EnvironmentName);

            //    await context.Response.WriteAsync("Hello World");




            //    //  logger.LogInformation("MW3: Request received and Response Produced ");
            //    //await context.Response.WriteAsync(_config["MyKey"]);
            //});



        }



    }
}
