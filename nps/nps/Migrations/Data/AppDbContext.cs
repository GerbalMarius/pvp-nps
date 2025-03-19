using Microsoft.EntityFrameworkCore;
using nps.Models;
using nps.Models.SurveyQuestions;

namespace nps.Migrations.Data;

public class AppDbContext : DbContext
{
    public DbSet<Survey> Surveys { get; set; }  
    public DbSet<Question> Questions { get; set; }
    public DbSet<Response> Responses { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //postgres only stores utc, this converts to our timezone when reading.
        modelBuilder.Entity<Order>()
            .Property(order => order.OrderDate)
            .HasConversion
            (
                date => date.ToUniversalTime(),
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius"))
            );

        modelBuilder.Entity<Order>()
            .Property(order => order.DeliveryDate)
            .HasConversion
            (
                date => date.ToUniversalTime(),
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius"))
            );
        
        modelBuilder.Entity<Question>()
            .HasDiscriminator<int>("question_type")
            .SetQuestionTypes();
        
        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(joinEntity => joinEntity.ToTable("user_roles"));
    }
}