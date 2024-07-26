using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services.Dtos
{
    public record GetTaskItemDto(string? Title, string? Description, DateTime CreatedAt, int BoardId, int StatusId, int AssigneeId)
    {
    };

    public static class GetTaskItemDtoExtensions
    {
        public static GetTaskItemDto FromTaskItemToGetDto(this TaskItem taskItem) => new 
         (
             Title: taskItem.Title,
             Description: taskItem.Description,
             CreatedAt: taskItem.CreatedAt,
             BoardId: taskItem.BoardId,
             StatusId: taskItem.StatusId,
             AssigneeId: taskItem.AssigneeId
         );

        public static void FromGetDtoToTaskItem(this GetTaskItemDto taskItemDto, TaskItem taskItem)
        {
            taskItem.Title = taskItemDto.Title;
            taskItem.Description = taskItemDto.Description;
            taskItem.CreatedAt = taskItemDto.CreatedAt;
            taskItem.BoardId = taskItemDto.BoardId;
            taskItem.StatusId = taskItemDto.StatusId;
            taskItem.AssigneeId = taskItemDto.AssigneeId;
        }
    }
}
