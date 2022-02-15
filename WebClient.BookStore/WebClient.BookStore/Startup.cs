using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Data;
using WebClient.BookStore.Helpers;
using WebClient.BookStore.Models;
using WebClient.BookStore.Repository;
using WebClient.BookStore.Service;

namespace WebClient.BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration = null;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {




            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;

            });

            services.Configure<DataProtectionTokenProviderOptions>(
                options => 
                {
                    options.TokenLifespan = TimeSpan.FromMinutes(1);
                }
            );

            services.ConfigureApplicationCookie(

                config =>
                {

                    //config.LoginPath = "/login";
                    //If we want to get from the appsetting 
                    config.LoginPath = _configuration["Application:LoginPath"];
                }

            );

            //it will add mvc in our application 
            //services.AddMvc();

            //it will add mvc in our application this is new features added after 3.1 version
            services.AddControllersWithViews();

            //if you declare the configuration on BookStoreContext.cs then uncomment the below line.
            //services.AddDbContext<BookStoreContext>();

            //if we don't want to declare the configuration in BookStoreContext.cs then comment the above code and uncomment the below;
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DBConnection"))
                //data source = ELAVARASAN - PC\SQLEXPRESS; initial catalog = EDBMS; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework & quot;
            );

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IEmailService, EmailService>();




            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));


            services.Configure<NewBookAlertMessageConfig>("NewBook", _configuration.GetSection("NewBookAlertMessage"));
            services.Configure<NewBookAlertMessageConfig>("ExternalBook", _configuration.GetSection("ExternalBookAlertMessage"));

            //To enable runtime compilation of view pages
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //to stop client side validation comment the below code:
            /*services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option=>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = false;
            }
            );*/
#endif
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
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                    RequestPath = "/MyStaticFiles"
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

            app.UseAuthentication();

            app.UseAuthorization();

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

                //if we want to add name after domain name;
                //endPoints.MapControllerRoute(
                //    name: "Default",
                //   //pattern: "bookApp/{controller=Home}/{action=Index}/{id?}"
                //   pattern: "{controller=Home}/{action=Index}/{id?}"
                //);

                endPoints.MapControllerRoute(
                    name: "MyArea",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );


            });
        }
    }
}
