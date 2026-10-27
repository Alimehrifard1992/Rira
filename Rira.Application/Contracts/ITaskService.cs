using Rira.Application.Dtos;

namespace Rira.Application.Contracts
{
    public interface ITaskService
    {
        Task<GetAllTaskServiceDto> GetAllTasksAsync();
        Task<GetTaskServiceDto> GetTaskAsync(Guid id);
        Task<Guid> PostTaskAsync(PostTaskServiceDto dto);
        Task PutTaskAsync(PutTaskServiceDto dto);
        Task DeleteTaskAsync(Guid id);
    }
}
