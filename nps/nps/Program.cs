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

        app.UseAuthorization();

		app.MapRazorPages();
        app.Run();
    }
}