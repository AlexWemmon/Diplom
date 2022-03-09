using Diplom.Domain.Repositories.Abstract;
using Diplom.Domain.Repositories.EntityFramework;
using Diplom.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQuestionsItemsRepository, EFQuestionsRepository>();
            services.AddTransient<IStudentsRepository, EFStudentsRepository>();
            services.AddTransient<ITestRepository, EFTestRepository>();
            services.AddTransient<DataManager>();
            services.AddDbContext<test_CursachContext>(x => x.UseSqlServer("Server=DESKTOP-KN71N92\\BD_SQL;Database=test_Cursach;Trusted_Connection=True;"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime.
		// Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
