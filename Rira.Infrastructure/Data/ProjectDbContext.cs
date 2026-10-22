using Microsoft.EntityFrameworkCore;
using Rira.Domain.Entities;

namespace Rira.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        #region [-ctors-]
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ProjectDbContext()
        {
        }
        #endregion

        public DbSet<TaskItem> TaskItem { get; set; }

        #region [-OnModelCreating-]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(x => x.IsCompleted)
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.Property(x => x.DueDate)
                    .IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
