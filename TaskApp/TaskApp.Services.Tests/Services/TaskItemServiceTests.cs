using Microsoft.EntityFrameworkCore;
using Moq;
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
using TaskApp.Services.Services;

namespace TaskApp.Services.Tests.Services
{
    public class TaskItemServiceTests
    {
        private const string UserId = "1";

        [Fact]
        public async Task GetTaskItemAsync_WithNonExistingItem_ThrowsTaskItemNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.GetAsync(1);

            // Assert
            await Assert.ThrowsAsync<TaskItemNotFoundException>(act);
        }

        [Fact]
        public async Task GetTaskItemAsync_TaskHasDifferentOwner_ThrowsTaskItemHasDifferentOwnerException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new TaskItem
            {
                Id = 1,
                Title = "test",
                Description = "test",
                AssigneeId = "2",
            };
            context.Tasks.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.GetAsync(1);

            // Assert
            await Assert.ThrowsAsync<TaskItemHasDifferentOwnerException>(act);
        }

        [Fact]
        public async Task CreateTaskItemAsync_WithNonExistingBoard_ThrowsBoardNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new CreateTaskItemDto
            (
                Title: "test",
                Description: "test",
                BoardId: 2,
                StatusId: 1
            );
            context.Tasks.Add(item.FromCreateDtoToTaskItem(currentUserService.Object));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.CreateAsync(item);

            // Assert
            await Assert.ThrowsAsync<BoardNotFoundException>(act);
        }

        [Fact]
        public async Task CreateTaskItemAsync_WithNonExistingStatus_ThrowsStatusNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();

            var board = new CreateBoardDto
            (
                Name: "test"
            );

            var item = new CreateTaskItemDto
            (
                Title: "test",
                Description: "test",
                BoardId: 1,
                StatusId: 4
            );

            context.Tasks.Add(item.FromCreateDtoToTaskItem(currentUserService.Object));
            context.Boards.Add(board.FromCreateDtoToBoard(currentUserService.Object));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.CreateAsync(item);

            // Assert
            await Assert.ThrowsAsync<StatusNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateTaskItemAsync_WithNonExistingItem_ThrowsTaskItemNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new UpdateTaskItemDto
            (
                Id: 1,
                Title: "test",
                Description: "test",
                BoardId: 2,
                StatusId: 1,
                AssigneeId: "1"
            );
            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.UpdateAsync(item);

            // Assert
            await Assert.ThrowsAsync<TaskItemNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateTaskItemAsync_TaskHasDifferentOwner_ThrowsTaskItemNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();

            var createItem = new TaskItem
            {
                Id = 1,
                Title = "test",
                Description = "test",
                BoardId = 1,
                StatusId = 1,
                AssigneeId = "2"
            };

            var updateItem = createItem.FromTaskItemToUpdateDto();

            context.Tasks.Add(createItem);
            
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.UpdateAsync(updateItem);

            // Assert
            await Assert.ThrowsAsync<TaskItemHasDifferentOwnerException>(act);
        }

        [Fact]
        public async Task UpdateAsync_InvalidStatusChange_ThrowsInvalidStatusChangeException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();

            var taskItem = new CreateTaskItemDto
            (
                Title: "test",
                Description: "test",
                BoardId: 1,
                StatusId: 1
            );

            context.Tasks.Add(taskItem.FromCreateDtoToTaskItem(currentUserService.Object));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var itemDto = new UpdateTaskItemDto
            (
                Id:1,
                Title: "test",
                Description: "test",
                BoardId: 1,
                StatusId: 57,
                AssigneeId: "1"
            );

            var taskService = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await taskService.UpdateAsync(itemDto);

            // Assert
            await Assert.ThrowsAsync<InvalidStatusChangeException>(act);
        }

        [Fact]
        public async Task DeleteTaskItemAsync_WithNonExistingItem_ThrowsTaskItemNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.DeleteAsync(1);

            // Assert
            await Assert.ThrowsAsync<TaskItemNotFoundException>(act);
        }

        [Fact]
        public async Task DeleteTaskItemAsync_TaskHasDifferentOwner_ThrowsTaskItemHasDifferentOwnerException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new TaskItem
            {
                Id = 1,
                Title = "test",
                Description = "test",
                AssigneeId = "2",
            };
            context.Tasks.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new TaskItemService(context, currentUserService.Object);

            // Act
            async Task act() => await service.DeleteAsync(1);

            // Assert
            await Assert.ThrowsAsync<TaskItemHasDifferentOwnerException>(act);
        }

        private static TaskAppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TaskAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TaskAppDbContext(options);
        }
    }
}
