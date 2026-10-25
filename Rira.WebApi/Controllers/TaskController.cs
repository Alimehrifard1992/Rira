using Microsoft.AspNetCore.Mvc;
using Rira.Application.Contracts;
using Rira.Application.Dtos;

namespace Rira.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        #region [-ctor-]
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        #endregion

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _taskService.GetAllTasks());
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            return new ObjectResult(await _taskService.GetTask(id));
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] PostTaskServiceDto dto)
        {
            return new ObjectResult(await _taskService.PostTask(dto));
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put([FromBody] PutTaskServiceDto dto)
        {
            return new ObjectResult(await _taskService.PutTask(dto));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return new ObjectResult(await _taskService.DeleteTask(id));
        }
    }
}
