using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFeedbacks.Models.FeedbackDb;

namespace MyFeedbacks.Data
{
    public partial class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext()
        {
        }

        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MyFeedbacks.Models.FeedbackDb.Feedback>()
              .Property(p => p.DateSubmitted)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<MyFeedbacks.Models.FeedbackDb.Feedback>()
              .Property(p => p.DateSubmitted)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<MyFeedbacks.Models.FeedbackDb.Feedback> Feedbacks { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}