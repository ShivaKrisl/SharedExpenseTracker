using ExpenseTracker.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace ExpenseTracker.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<SharedExpense> SharedExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SharedExpense>()
            .HasOne(s => s.CreatedByUser)
            .WithMany(u => u.SharedExpenses)
            .HasForeignKey(s => s.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SharedExpense>()
            .Property(s => s.UserIds)
            .HasConversion(
                v => JsonSerializer.Serialize<List<Guid>>(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>()
            )
            .Metadata
            .SetValueComparer(new ValueComparer<List<Guid>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            ));
        }
    }
}
