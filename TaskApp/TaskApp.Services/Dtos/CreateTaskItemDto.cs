using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services.Dtos
{
    public record CreateTaskItemDto(string? Title, string? Description, int BoardId, int StatusId)
    {
    };

    public static class CreateTaskItemDtoExtensions
    {
        public static CreateTaskItemDto FromTaskItemToCreateDto(this TaskItem taskItem) => new
         (
             Title: taskItem.Title,
             Description: taskItem.Description,
             BoardId: taskItem.BoardId,
             StatusId: taskItem.StatusId
         );

        public static TaskItem FromCreateDtoToTaskItem(this CreateTaskItemDto taskItemDto, ICurrentUserService _currentUserService)
        {
            return new TaskItem
            {
                Title = taskItemDto.Title,
                Description = taskItemDto.Description,
                BoardId = taskItemDto.BoardId,
                StatusId = taskItemDto.StatusId,
                AssigneeId = _currentUserService.UserId
            };
        }
    }
}
