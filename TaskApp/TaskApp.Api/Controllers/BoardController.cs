﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Data.Models;
using TaskApp.Services.Dtos;
using TaskApp.Services.Interfaces;
using TaskApp.Services.Services;

namespace TaskApp.Api.Controllers
{
    [Authorize]
    [Route("api/board")]
    [ApiController]
    public class BoardController(IBoardService service) : ControllerBase
    {
        private readonly IBoardService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetAllAsync()
        {
            var boards = await _service.GetAllAsync();
            return Ok(boards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetAsync(int id)
        {
            var board = await _service.GetAsync(id);
            return Ok(board);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateBoardDto boardDto)
        {
            await _service.CreateAsync(boardDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNameAsync(UpdateBoardDto boardDto)
        {
            await _service.UpdateNameAsync(boardDto);
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
