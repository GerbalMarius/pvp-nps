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

    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<RatingQuestion> RatingQuestions { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<DropDownQuestion> DropDownQuestions { get; set; }
    public DbSet<CheckBoxQuestion> CheckBoxQuestions { get; set; }
    public DbSet<TextQuestion> TextQuestions { get; set; }
    
    public DbSet<Response> Responses { get; set; }
    public DbSet<ResponseOption> ResponseOptions { get; set; }

    public DbSet<User> Users { get; set; }

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


        modelBuilder.Entity<Survey>()
            .Property(order => order.TakenAt)
            .HasConversion
            (
                date => date.Value.ToUniversalTime(),
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius"))
            );
        modelBuilder.Entity<Survey>()
            .Property(order => order.CreatedAt)
            .HasConversion
            (
                date => date.ToUniversalTime(),
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius"))
            );


        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(joinEntity => joinEntity.ToTable("user_roles"));

        modelBuilder.Entity<Survey>()
            .HasMany(survey => survey.Questions)
            .WithMany(question => question.Surveys)
            .UsingEntity(joinEntity => joinEntity.ToTable("survey_questions"));

        modelBuilder.Entity<Survey>()
            .Property(survey => survey.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@example.com",
                Password = "Admin123",
                TelephoneNumber = "+37069476375"
            }
        );
        

        modelBuilder.Entity<Order>().HasData(
            new Order {
                Id = 1001, 
                Number = "155877AA", 
                OrderDate = new DateTime(2024,1,1), 
                DeliveryDate = new DateTime(2024,5,1),
                UserId = 1
            },
            new Order {
                Id = 1002, 
                Number = "ABSASBSABSSSX", 
                OrderDate = new DateTime(2025,1,1), 
                DeliveryDate = new DateTime(2025,3,1),
                UserId = 1
            }
        );
        
        modelBuilder.Entity<Survey>().HasData(
            new Survey { Id = 1, Name = "Feedback", OrderId = 1001},
            new Survey { Id = 2, Name = "Satisfaction", OrderId = 1002}
        );

       
        modelBuilder.Entity<RatingQuestion>().HasData(
            new RatingQuestion { Id = 1, QuestionText = "How would you rate our service?" },
            new RatingQuestion { Id = 2, QuestionText = "How satisfied are you with your job?" }
        );

        modelBuilder.Entity<SingleChoiceQuestion>().HasData(
            new SingleChoiceQuestion { Id = 3, QuestionText = "Which feature do you use most?" }
        );

        modelBuilder.Entity<DropDownQuestion>().HasData(
            new DropDownQuestion { Id = 4, QuestionText = "How did you hear about us?" }
        );

        modelBuilder.Entity<CheckBoxQuestion>().HasData(
            new CheckBoxQuestion { Id = 5, QuestionText = "Preferred contact methods?" }
        );

        modelBuilder.Entity<TextQuestion>().HasData(
            new TextQuestion { Id = 6, QuestionText = "Any additional feedback?" }
        );

      
        modelBuilder.Entity<AnswerChoice>().HasData(
            // Options for DropDownQuestion (ID: 4)
            new AnswerChoice { Id = 1, QuestionId = 4, Text = "Social Media" },
            new AnswerChoice { Id = 2, QuestionId = 4, Text = "Friend" },
            new AnswerChoice { Id = 3, QuestionId = 4, Text = "Ad" },

            // Options for CheckBoxQuestion (ID: 5)
            new AnswerChoice { Id = 4, QuestionId = 5, Text = "Email" },
            new AnswerChoice { Id = 5, QuestionId = 5, Text = "Phone" },
            new AnswerChoice { Id = 6, QuestionId = 5, Text = "SMS" }
        );
    }
}