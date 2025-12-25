using FitnessActivityTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessActivityTracker.Data
{
    public class FitnessDbContext : DbContext, IDbContext
    {
        public DbSet<Workout> Workouts { get; set; }

        public FitnessDbContext()
        {
        }

        private static FitnessDbContext Context = null;
        public static FitnessDbContext GetContext()
        {
            if (Context == null) Context = new FitnessDbContext();
            Context.Database.EnsureCreated();
            return Context;
        }
        public async static Task<FitnessDbContext> GetContextAsync()
        {
            if (Context == null) Context = new FitnessDbContext();
            await Context.Database.EnsureCreatedAsync();
            return Context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Fitness.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Type).HasConversion<string>();
                entity.Property(x => x.Intensity).HasConversion<string>();
                entity.Property(x => x.Status).HasConversion<string>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
