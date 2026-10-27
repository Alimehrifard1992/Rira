using Rira.Application.Contracts;
using Rira.Application.Dtos;
using Rira.Application.Errors;
using Rira.Application.Helper;
using Rira.Domain.Contracts;
using Rira.Domain.Entities;

namespace Rira.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        #region [-ctor-]
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion

        #region [-GetAll-]
        public async Task<GetAllTaskServiceDto> GetAllTasksAsync()
        {
            var taskItems = await _taskRepository.GetAllAsync();

            var result = new GetAllTaskServiceDto();

            foreach (var taskItem in taskItems)
            {
                result.GetTaskServiceDtos.Add(new GetTaskServiceDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    Description = taskItem.Description,
                    IsCompleted = taskItem.IsCompleted,
                    DueDate = taskItem.DueDate
                });
            }
            return result;
        }
        #endregion

        #region [-Get-]
        public async Task<GetTaskServiceDto> GetTaskAsync(Guid id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);

            if (taskItem is null)
            {
                throw new CustomException(TaskErrors.NotFound);
            }

            return new GetTaskServiceDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,
                DueDate = taskItem.DueDate
            };
        }
        #endregion

        #region [-Post-]
        public async Task<Guid> PostTaskAsync(PostTaskServiceDto dto)
        {
            var taskItem = new TaskItem
            {
                Title = dto.Title.Trim(),
                Description = dto.Description.Trim(),
                IsCompleted = false,
                DueDate = dto.DueDate
            };
            await _taskRepository.AddAsync(taskItem);
            return taskItem.Id;
        }
        #endregion

        #region [-Put-]
        public async Task PutTaskAsync(PutTaskServiceDto dto)
        {
            var taskItem = await _taskRepository.GetByIdAsync(dto.Id);

            if (taskItem == null)
            {
                throw new CustomException(TaskErrors.NotFound);
            }

            taskItem.Title = dto.Title.Trim();
            taskItem.Description = dto.Description.Trim();
            taskItem.IsCompleted = dto.IsCompleted;
            taskItem.DueDate = dto.DueDate;

            await _taskRepository.UpdateAsync(taskItem);
        }
        #endregion

        #region [-Delete-]
        public async Task DeleteTaskAsync(Guid id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);

            if (taskItem == null)
            {
                throw new CustomException(TaskErrors.NotFound);
            }
            await _taskRepository.DeleteAsync(taskItem);
        }
        #endregion
    }
}
