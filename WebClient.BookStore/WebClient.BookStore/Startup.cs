using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //it will add mvc in our application 
            //services.AddMvc();

            //it will add mvc in our application this is new features added after 3.1 version
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //To use static files in the view like images,css,js;
            app.UseStaticFiles();

            //To use static files from wwwroot folder and with other folders 
            app.UseStaticFiles(
                new StaticFileOptions() { 
                    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"MyStaticFiles")),
                    RequestPath="/MyStaticFiles"
                }
            );
           

           /*app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello from my first middleware 1\n");
                    await next();
                    await context.Response.WriteAsync("Hello from my first middleware response\n");
                }
            );
            app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello from my second middleware 2\n");
                    await next();
                    await context.Response.WriteAsync("Hello from my second middleware response\n");
                }
            );
            app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello from my third middleware 3\n");
                }
            );*/

            app.UseRouting();

            /*app.UseEndpoints(endPoints =>
            {
                endPoints.Map("/",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    }
                );
            });
            app.UseEndpoints(endPoints =>
            {
                endPoints.Map("/ela",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World Mr.Ela!");
                    }
                );
            });

            app.UseEndpoints(endPoints =>
            {
                endPoints.Map("/environment",
                    async context =>
                    {
                        await context.Response.WriteAsync(env.EnvironmentName);
                    }
                );
            });

            app.UseEndpoints(endPoints =>
            {
                endPoints.Map("/check",
                    async context =>
                    {
                        if(env.IsDevelopment())
                            await context.Response.WriteAsync("Your environment is Development");
                        else if(env.IsProduction())
                            await context.Response.WriteAsync("Your environment is Production");
                        else if(env.IsStaging())
                            await context.Response.WriteAsync("Your environment is Staging");
                        else
                            await context.Response.WriteAsync("Your environment is "+env.EnvironmentName);

                    }
                );
            });
            */
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapDefaultControllerRoute();
            });
        }
    }
}
