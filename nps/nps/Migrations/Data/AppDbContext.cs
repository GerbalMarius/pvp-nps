using Microsoft.EntityFrameworkCore;

namespace nps.Migrations.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
     :base(options)
    {
        
    }
}