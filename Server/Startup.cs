using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Models.Contexts;
using Microsoft.Extensions.Hosting;

namespace Server
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
            services.AddCors(corsOptions => {
                corsOptions.AddPolicy("AllowAllOriginsForAngularOops",
                            builder=>
                            {
                                builder.AllowAnyOrigin();
                            });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<WLMDbContext,WLMDbContext>();

            //SEARCHED "AddMvc() api" in google.
            
            //DELETED below code line.
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //ADD below code

            //MVCOptions.EnableEndpointRouting = false; https://apisof.net/catalog/Microsoft.AspNetCore.Mvc.MvcOptions
            //Startup.cs(47,13): error CS0103: The name 'MVCOptions' does not exist in the current context [C:\Users\ydvsjq\Desktop\Projects\Allocator3\Server\Server.csproj]
            //then few more errors which got me to write below code.
            
            //ADD 1 try - failed
            //MvcOptions mvcOptions = new MvcOptions();
            //mvcOptions.EnableEndpointRouting = false;
            //MvcServiceCollectionExtensions.AddMvc(services,mvcOptions);

            //ERR:
            //Startup.cs(55,60): error CS1503: Argument 2: cannot convert from 'Microsoft.AspNetCore.Mvc.MvcOptions' to 'System.Action<Microsoft.AspNetCore.Mvc.MvcOptions>' [C:\Users\ydvsjq\Desktop\Projects\Allocator3\Server\Server.csproj]
            //ADD 2 try - succeeded

            //Action<MvcOptions> myAction = new Action<MvcOptions>()
            Action<MvcOptions> myAction = (MvcOptions)=>{MvcOptions.EnableEndpointRouting = false;};
            MvcServiceCollectionExtensions.AddMvc(services,myAction);

            //wow donwe
            /*Build succeeded.
            C:\Prgm\dotnet\sdk\3.0.100\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(149,5): warning NETSDK1080: A PackageReference to Microsoft.AspNetCore.App is not necessary when targeting .NET Core 3.0 or higher. If Microsoft.NET.Sdk.Web is used, the shared framework will be referenced automatically. Otherwise, the PackageReference should be replaced with a FrameworkReference. [C:\Users\ydvsjq\Desktop\Projects\Allocator3\Server\Server.csproj]
            C:\Prgm\dotnet\sdk\3.0.100\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.DefaultItems.targets(149,5): warning NETSDK1080: A PackageReference to Microsoft.AspNetCore.App is not necessary when targeting .NET Core 3.0 or higher. If Microsoft.NET.Sdk.Web is used, the shared framework will be referenced automatically. Otherwise, the PackageReference should be replaced with a FrameworkReference. [C:\Users\ydvsjq\Desktop\Projects\Allocator3\Server\Server.csproj]
            Startup.cs(67,56): warning CS0618: 'IHostingEnvironment' is obsolete: 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.' [C:\Users\ydvsjq\Desktop\Projects\Allocator3\Server\Server.csproj]
            3 Warning(s)
            0 Error(s)
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env) obsolete
        //So we added IWbHostEnvironment instead, and found that env.IsDevelopment() is not present.
        //Then got that this extension is needed
        //using Microsoft.Extensions.Hosting; WOW
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseCors("AllowAllOriginsForAngularOops");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //Dont know how to ocnfigure api controllers differently. So m gonna use
        }
    }
}
