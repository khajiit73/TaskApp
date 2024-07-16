using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Context;
using TaskApp.Data.Models;

namespace TaskApp.Api.Controllers
{
    [Route("api/task-items")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskAppDbContext _context;

        public TaskItemsController(TaskAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllAsync()
        {
            var items = await _context.Tasks.ToListAsync();
            return Ok(items);
        }

    }
}
