using Diplom.Domain.Repositories.Abstract;
using Diplom.Domain.Repositories.EntityFramework;
using Diplom.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Diplom;

public class Startup
{
	private IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<IQuestionsItemsRepository, EFQuestionsRepository>();
		services.AddTransient<IStudentsRepository, EFStudentsRepository>();
		services.AddTransient<ITestRepository, EFTestRepository>();
		services.AddTransient<DataManager>();

		services.AddDbContext<test_CursachContext>(
			x => x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
		);

		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options => { options.LoginPath = new PathString("/Account/Login"); });
		services.AddControllersWithViews().AddSessionStateTempDataProvider();
		services.AddControllersWithViews().AddRazorRuntimeCompilation();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

		app.UseDeveloperExceptionPage();
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
		});
	}
}