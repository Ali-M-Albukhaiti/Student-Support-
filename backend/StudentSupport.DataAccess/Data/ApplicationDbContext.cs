using Microsoft.EntityFrameworkCore;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerUpvote> AnswerUpvotes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Test User
            var testPassword = "1234";
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(testPassword);
            var passwordHash = Convert.ToBase64String(passwordBytes); // simple test hash

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                UserName = "testuser",
                Email = "testuser@student.fontys.nl",
                FullName = "Test User",
                StudyProgram = "ICT",
                Semester = 3,
                Points = 0,
                Level = 1,
                IsAdmin = false,
                PasswordHash = passwordHash
            });
            var testPassword2 = "12345";
            var passwordBytes2 = System.Text.Encoding.UTF8.GetBytes(testPassword2);
            var passwordHash2 = Convert.ToBase64String(passwordBytes2);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                UserName = "testuser2",
                Email = "testuser2@student.fontys.nl",
                FullName = "Test User2",
                StudyProgram = "Media",
                Semester = 4,
                Points = 0,
                Level = 1,
                IsAdmin = false,
                PasswordHash = passwordHash2
            });
            // Seed Test Posts
            modelBuilder.Entity<Post>().HasData(
    new Post
    {
        Id = 1,
        Title = "My Frontend Development Journey",
        Content = "Just completed semester 3 focusing on React and modern web development. Here are some insights...",
        Semester = 3,
        AuthorId = 1, // the seeded user
        CreatedAt = DateTime.Parse("2025-10-10T10:00:00Z")
    },
    new Post
    {
        Id = 2,
        Title = "Database Design Best Practices",
        Content = "Working with SQL Server and Entity Framework taught me valuable lessons about database design...",
        Semester = 4,
        AuthorId = 1,
        CreatedAt = DateTime.Parse("2025-10-09T14:30:00Z")
    },
    new Post
    {
        Id = 3,
        Title = "Business Analysis Tools That Actually Work",
        Content = "After trying multiple approaches to business analysis, here are the tools and methodologies that delivered results...",
        Semester = 5,
        AuthorId = 1,
        CreatedAt = DateTime.Parse("2025-10-08T09:15:00Z")
    }
);


            // 🧩 PostLike composite key
            modelBuilder.Entity<PostLike>()
                .HasKey(pl => new { pl.PostId, pl.UserId });

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧩 Post ↔ User
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🧩 Comment ↔ User, Post, Question, Answer
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Question)
                .WithMany()
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Answer)
                .WithMany()
                .HasForeignKey(c => c.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🧩 Question ↔ User
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Author)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🧩 Answer ↔ User & Question
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧩 PointHistory ↔ User
            modelBuilder.Entity<PointHistory>()
                .HasOne(ph => ph.User)
                .WithMany(u => u.PointHistories)
                .HasForeignKey(ph => ph.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧩 AnswerUpvote composite key
            modelBuilder.Entity<AnswerUpvote>()
                .HasIndex(au => new { au.AnswerId, au.UserId })
                .IsUnique();
        }


    }
}
