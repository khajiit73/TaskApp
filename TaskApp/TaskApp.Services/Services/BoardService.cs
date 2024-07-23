using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Context;
using TaskApp.Data.Models;
using TaskApp.Services.Dtos;
using TaskApp.Services.Exceptions;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public BoardService(TaskAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
           _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<Board>> GetAllAsync()
        {
            return await _context.Boards
                .Where(x => x.UserId == _currentUserService.UserId)
                .ToListAsync();
        }

        public async Task<Board> GetAsync(int id)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            if (board.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            return board;
        }

        public async Task CreateAsync(CreateBoardDto boardDto)
        {
            var board = new Board
            {
                Name = boardDto.Name,
                UserId = _currentUserService.UserId
            };

            _context.Boards.Add(board);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            var board = await _context.Boards.FindAsync(id);

            if (board is null)
            {
                throw new BoardNotFoundException();
            }

            if (board.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            _context.Boards.Remove(board);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(int id, CreateBoardDto boardDto)
        {
            var existingBoard = await _context.Boards.FindAsync(id);

            if (existingBoard is null)
            {
                throw new BoardNotFoundException();
            }

            if (existingBoard.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            existingBoard.Name = boardDto.Name;

            await _context.SaveChangesAsync();
        }
    }
}
