using Microsoft.EntityFrameworkCore;
using nps.Models;
using nps.Models.SurveyQuestions;

namespace nps.Migrations.Data;

public class AppDbContext : DbContext
{
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

        //pre-seeding enums
        modelBuilder.Entity<QuestionType>().HasData(
            new QuestionType { Id = 1, Name = "Single Choice" },
            new QuestionType { Id = 2, Name = "Multiple Choice" },
            new QuestionType { Id = 3, Name = "Short Answer" },
            new QuestionType { Id = 4, Name = "Essay" },
            new QuestionType { Id = 5, Name = "NPS Scale" },//full scale 1 - 10
            new QuestionType { Id = 6, Name = "NPS Smileys" }, //smiley faces, negative, neutral, positive.
            new QuestionType { Id = 7, Name = "Likert" },
            new QuestionType { Id = 8, Name = "Ranking" } //ranking from best to worst, may sometimes be usefull
        );
    }
}