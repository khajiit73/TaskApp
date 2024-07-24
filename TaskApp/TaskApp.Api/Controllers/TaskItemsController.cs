using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Models;
using TaskApp.Services.Interfaces;

namespace TaskApp.Api.Controllers
{
    [Route("api/task-items")]
    [ApiController]
    public class TaskItemsController(ITaskItemService service) : ControllerBase
    {
        private readonly ITaskItemService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllAsync()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

    }
}
