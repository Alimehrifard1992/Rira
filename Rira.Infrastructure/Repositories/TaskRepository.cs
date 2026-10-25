using Microsoft.EntityFrameworkCore;
using Rira.Domain.Contracts;
using Rira.Domain.Entities;
using Rira.Infrastructure.Data;

namespace Rira.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectDbContext _context;

        #region [-ctor-]
        public TaskRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [-GetAll-]
        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItem.AsNoTracking().ToListAsync();
        }
        #endregion

        #region [-GetById-]
        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItem.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region [-Add-]
        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.TaskItem.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region [-Update-]
        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItem.Update(taskItem);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region [-Delete-]
        public async Task DeleteAsync(TaskItem taskItem)
        {
            _context.TaskItem.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
