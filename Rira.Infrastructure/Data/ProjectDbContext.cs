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
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
