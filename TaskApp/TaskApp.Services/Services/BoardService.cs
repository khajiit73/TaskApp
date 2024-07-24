﻿using Microsoft.EntityFrameworkCore;
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
    public class BoardService(TaskAppDbContext context, ICurrentUserService currentUserService) : IBoardService
    {
        private readonly TaskAppDbContext _context = context;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<GetBoardDto>> GetAllAsync()
        {
            var boards = await _context.Boards
                .Where(x => x.UserId == _currentUserService.UserId)
                .ToListAsync();

            var boardDtos = new List<GetBoardDto>();

            foreach(var board in boards)
            {
                boardDtos.Add(board.FromBoardToGetDto());
            }

            return boardDtos;
        }

        public async Task<GetBoardDto> GetAsync(int id)
        {
            var board = await _context.Boards.FindAsync(id) ?? throw new BoardNotFoundException();
            
            if (board.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            return board.FromBoardToGetDto();
        }

        public async Task CreateAsync(CreateBoardDto boardDto)
        {
            var board = boardDto.FromCreateDtoToBoard(_currentUserService);

            _context.Boards.Add(board);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            var board = await _context.Boards.FindAsync(id) ?? throw new BoardNotFoundException();
            
            if (board.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            _context.Boards.Remove(board);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(int id, UpdateBoardDto boardDto)
        {
            var existingBoard = await _context.Boards.FindAsync(id) ?? throw new BoardNotFoundException();
            
            if (existingBoard.UserId != _currentUserService.UserId)
            {
                throw new BoardHasDifferentOwnerException();
            }

            boardDto.FromUpdateDtoToBoard(existingBoard);

            await _context.SaveChangesAsync();
        }
    }
}
