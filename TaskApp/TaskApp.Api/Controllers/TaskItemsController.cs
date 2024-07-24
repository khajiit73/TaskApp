﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Models;
using TaskApp.Services.Dtos;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetAsync(int id)
        {
            var taskItem = await _service.GetAsync(id);
            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateTaskItemDto taskItemDto)
        {
            await _service.CreateAsync(taskItemDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateTaskItemDto taskItemDto)
        {
            await _service.UpdateAsync(id, taskItemDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
