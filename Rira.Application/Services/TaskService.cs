using Rira.Application.Contracts;
using Rira.Application.Dtos;
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
        public async Task<GetAllTaskServiceDto> GetAllTasks()
        {
            try
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
            catch (Exception)
            {
                throw new Exception("هنگام دریافت لیست تسک ها مشکلی پیش آمد. لطفا دوباره تلاش کنید.");
            }
        }
        #endregion

        #region [-Get-]
        public async Task<GetTaskServiceDto?> GetTask(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("شناسه تسک معتبر نیست.");
                }

                var taskItem = await _taskRepository.GetByIdAsync(id);

                if (taskItem is null)
                {
                    throw new KeyNotFoundException("تسکی با شناسه واردشده پیدا نشد.");
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
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("هنگام دریافت تسک مشکلی رخ داد. لطفاً دوباره تلاش کنید.");
            }
        }
        #endregion

        #region [-Post-]
        public async Task<Guid> PostTask(PostTaskServiceDto dto)
        {
            try
            {
                if (dto is null)
                {
                    throw new ArgumentNullException(nameof(dto), "اطلاعات تسک ارسال نشده است.");
                }

                if (string.IsNullOrWhiteSpace(dto.Title))
                {
                    throw new ArgumentException("عنوان تسک الزامی است.");
                }

                if (dto.Title.Trim().Length > 200)
                {
                    throw new ArgumentException("عنوان تسک نمی‌تواند بیشتر از ۲۰۰ کاراکتر باشد.");
                }

                if (dto.Description is null)
                {
                    throw new ArgumentException("توضیحات تسک نمی‌تواند خالی از مقدار باشد.");
                }

                if (dto.Description.Length > 2000)
                {
                    throw new ArgumentException("توضیحات تسک نمی‌تواند بیشتر از ۲۰۰۰ کاراکتر باشد.");
                }

                if (dto.DueDate == default)
                {
                    throw new ArgumentException("تاریخ سررسید تسک الزامی است.");
                }

                var taskItem = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title.Trim(),
                    Description = dto.Description.Trim(),
                    IsCompleted = false,
                    DueDate = dto.DueDate
                };
                await _taskRepository.AddAsync(taskItem);
                return taskItem.Id;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("هنگام ایجاد تسک مشکلی رخ داد. لطفاً دوباره تلاش کنید.");
            }
        }
        #endregion

        #region [-Put-]
        public async Task<bool> PutTask(PutTaskServiceDto dto)
        {
            try
            {
                if (dto is null)
                {
                    throw new ArgumentNullException(nameof(dto), "اطلاعات ویرایش تسک ارسال نشده است.");
                }

                if (dto.Id == Guid.Empty)
                {
                    throw new ArgumentException("شناسه تسک معتبر نیست.");
                }

                if (string.IsNullOrWhiteSpace(dto.Title))
                {
                    throw new ArgumentException("عنوان تسک الزامی است.");
                }

                if (dto.Title.Trim().Length > 200)
                {
                    throw new ArgumentException("عنوان تسک نمی‌تواند بیشتر از ۲۰۰ کاراکتر باشد.");
                }

                if (dto.Description is null)
                {
                    throw new ArgumentException("توضیحات تسک نمی‌تواند خالی از مقدار باشد.");
                }

                if (dto.Description.Length > 2000)
                {
                    throw new ArgumentException("توضیحات تسک نمی‌تواند بیشتر از ۲۰۰۰ کاراکتر باشد.");
                }

                if (dto.DueDate == default)
                {
                    throw new ArgumentException("تاریخ سررسید تسک الزامی است.");
                }

                var taskItem = await _taskRepository.GetByIdAsync(dto.Id);

                if (taskItem is null)
                {
                    throw new KeyNotFoundException("تسکی که قصد ویرایش آن را دارید پیدا نشد.");
                }

                taskItem.Title = dto.Title.Trim();
                taskItem.Description = dto.Description.Trim();
                taskItem.IsCompleted = dto.IsCompleted;
                taskItem.DueDate = dto.DueDate;

                await _taskRepository.UpdateAsync(taskItem);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("هنگام ویرایش تسک مشکلی رخ داد. لطفاً دوباره تلاش کنید.");
            }
        }
        #endregion

        #region [-Delete-]
        public async Task<bool> DeleteTask(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("شناسه تسک معتبر نیست.");
                }

                var taskItem = await _taskRepository.GetByIdAsync(id);

                if (taskItem is null)
                {
                    throw new KeyNotFoundException("تسکی که قصد حذف آن را دارید پیدا نشد.");
                }
                await _taskRepository.DeleteAsync(taskItem);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("هنگام حذف تسک مشکلی رخ داد. لطفاً دوباره تلاش کنید.");
            }
        }
        #endregion
    }
}
