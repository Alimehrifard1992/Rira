using Microsoft.AspNetCore.Mvc;
using Rira.Application.Contracts;
using Rira.Application.Dtos;
using Rira.Application.Errors;

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

        #region [-GetAll-]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksAsync();
            return Ok(result);
        }
        #endregion

        #region [-Get-]
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _taskService.GetTaskAsync(id);
            return Ok(result);
        }
        #endregion

        #region [-Post-]
        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] PostTaskServiceDto dto)
        {
            if (dto.DueDate == default)
            {
                return BadRequest(TaskErrors.ValuesAreNotValid);
            }
            var id = await _taskService.PostTaskAsync(dto);

            return StatusCode(StatusCodes.Status200OK, new { Id = id });
        }
        #endregion

        #region [-Put-]
        [HttpPut("Put")]
        public async Task<IActionResult> Put([FromBody] PutTaskServiceDto dto)
        {
            if (dto == null || !ModelState.IsValid || dto.DueDate == default)
            {
                return BadRequest(TaskErrors.ValuesAreNotValid);
            }
            await _taskService.PutTaskAsync(dto);
            return Ok();
        }
        #endregion

        #region [-Delete-]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }
        #endregion
    }
}
