﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services.Dtos
{
    public record UpdateTaskItemDto(string? Title, string? Description, int BoardId, int StatusId, int AssigneeId);

    public static class UpdateTaskItemDtoExtensions
    {
        public static UpdateTaskItemDto FromTaskItemToUpdateDto(this TaskItem taskItem) => new
         (
             Title: taskItem.Title,
             Description: taskItem.Description,
             BoardId: taskItem.BoardId,
             StatusId: taskItem.StatusId,
             AssigneeId: taskItem.AssigneeId
         );

        public static void FromUpdateDtoToTaskItem(this UpdateTaskItemDto taskItemDto, TaskItem taskItem)
        {
            taskItem.Title = taskItemDto.Title;
            taskItem.Description = taskItemDto.Description;
            taskItem.BoardId = taskItemDto.BoardId;
            taskItem.StatusId = taskItemDto.StatusId;
            taskItem.AssigneeId = taskItemDto.AssigneeId;
        }
    }
}