using Rira.Application.Dtos;

namespace Rira.Application.Contracts
{
    public interface ITaskService
    {
        Task<GetAllTaskServiceDto> GetAllTasks();
        Task<GetTaskServiceDto> GetTask(Guid id);
        Task<Guid> PostTask(PostTaskServiceDto dto);
        Task<bool> PutTask(PutTaskServiceDto dto);
        Task<bool> DeleteTask(Guid id);
    }
}
