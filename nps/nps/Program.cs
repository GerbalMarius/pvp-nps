using System.Globalization;

namespace nps;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        //Note: additional config for sessions
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30d);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();
        
        

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            
            app.UseHsts();
        }
        app.UseSession();//enabling usage of session store.

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}