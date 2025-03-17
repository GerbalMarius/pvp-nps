using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;

namespace nps;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
		//Note: additional config for sessions

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // "Cookies"
				.AddCookie(options =>
				{
					options.LoginPath = "/Login/Index";  
					options.AccessDeniedPath = "/Login/Index";
					options.Cookie.Name = "AuthCookie"; 
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
					options.SlidingExpiration = true;
				});

		builder.Services.AddAuthorization();
		builder.Services.AddDistributedMemoryCache();

		builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30d);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        
        builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        

		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		var app = builder.Build();
        
        

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            
            app.UseHsts();
        }
        app.UseSession();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseSession();

		app.MapRazorPages();
        app.Run();
    }
}