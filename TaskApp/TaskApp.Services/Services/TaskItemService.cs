using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Context;
using TaskApp.Services.Dtos;
using TaskApp.Services.Exceptions;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services.Services
{
    public class TaskItemService(TaskAppDbContext context, ICurrentUserService currentUserService) : ITaskItemService
    {
        private readonly TaskAppDbContext _context = context;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<GetTaskItemDto>> GetAllAsync()
        {
            var taskItems = await _context.Tasks
                .Where(t => t.AssigneeId == _currentUserService.UserId)
                .ToListAsync();

            var taskItemDtos = new List<GetTaskItemDto>();

            foreach(var taskItem in taskItems)
            {
                taskItemDtos.Add(taskItem.FromTaskItemToGetDto());
            }

            return taskItemDtos;
        }

        public async Task<GetTaskItemDto> GetAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id) ?? throw new TaskItemNotFoundException();
            
            if (taskItem.AssigneeId != _currentUserService.UserId)
            {
                throw new TaskItemHasDifferentOwnerException();
            }

            return taskItem.FromTaskItemToGetDto();
        }
        public async Task CreateAsync(CreateTaskItemDto itemDto)
        {
            if(!_context.Boards.Any(b => b.Id == itemDto.BoardId))
            {
                throw new BoardNotFoundException();
            }

            if (!_context.Statuses.Any(b => b.Id == itemDto.StatusId))
            {
                throw new StatusNotFoundException();
            }

            var taskItem = itemDto.FromCreateDtoToTaskItem(_currentUserService);

            _context.Tasks.Add(taskItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTaskItemDto itemDto)
        {
            var existingTaskItem = await _context.Tasks.FindAsync(id) ?? throw new TaskItemNotFoundException();
            
            if (existingTaskItem.AssigneeId != _currentUserService.UserId)
            {
                throw new TaskItemHasDifferentOwnerException();
            }

            if(!ValidateStatusChange(existingTaskItem.StatusId, itemDto.StatusId))
            {
                throw new InvalidStatusChangeException();
            }

            itemDto.FromUpdateDtoToTaskItem(existingTaskItem);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id) ?? throw new TaskItemNotFoundException();
            
            if (taskItem.AssigneeId != _currentUserService.UserId)
            {
                throw new TaskItemHasDifferentOwnerException();
            }

            _context.Tasks.Remove(taskItem);

            await _context.SaveChangesAsync();
        }

        public bool ValidateStatusChange(int currentStatusId, int newStatusId)
        {
            return _context.StatusTransitions.Any(st => st.CurrentStatusId == currentStatusId && st.NextStatusId == newStatusId);
        }
    }
}
