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
    public class BoardServiceTests
    {
        private readonly int UserId = 1;

        [Fact]
        public async Task GetBoardAsync_WithNonExistingBoard_ThrowsBoardNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var service = new BoardService(context, currentUserService.Object);

            // Act
            async Task act() => await service.GetAsync(1);

            // Assert
            await Assert.ThrowsAsync<BoardNotFoundException>(act);
        }

        [Fact]
        public async Task GetBoardAsync_BoardHasDifferentOwner_ThrowsBoardHasDifferentOwnerException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new Board 
            { 
                Id = 1,
                Name = "Test",
                OwnerId = 2
            };

            context.Boards.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new BoardService(context, currentUserService.Object);

            // Act
            async Task act() => await service.GetAsync(1);

            // Assert
            await Assert.ThrowsAsync<BoardHasDifferentOwnerException>(act);
        }

        [Fact]
        public async Task DeleteBoardAsync_WithNonExistingBoard_ThrowsBoardNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var service = new BoardService(context, currentUserService.Object);

            // Act
            async Task act() => await service.DeleteAsync(1);

            // Assert
            await Assert.ThrowsAsync<BoardNotFoundException>(act);
        }

        [Fact]
        public async Task DeleteBoardAsync_BoardHasDifferentOwner_ThrowsBoardHasDifferentOwnerException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new Board
            {
                Id = 1,
                Name = "Test",
                OwnerId = 2
            };

            context.Boards.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new BoardService(context, currentUserService.Object);

            // Act
            async Task act() => await service.DeleteAsync(1);

            // Assert
            await Assert.ThrowsAsync<BoardHasDifferentOwnerException>(act);
        }

        [Fact]
        public async Task UpdateBoardAsync_WithNonExistingBoard_ThrowsBoardNotFoundException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var service = new BoardService(context, currentUserService.Object);

            var boardDto = new UpdateBoardDto
            (
                Id: 1,
                Name: "Test",
                OwnerId: 1
            );

            // Act
            async Task act() => await service.UpdateNameAsync(boardDto);

            // Assert
            await Assert.ThrowsAsync<BoardNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateBoardAsync_BoardHasDifferentOwner_ThrowsBoardHasDifferentOwnerException()
        {
            // Arrange
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(x => x.UserId).Returns(UserId);

            var context = GetDbContext();
            var item = new Board
            {
                Id = 1,
                Name = "Test",
                OwnerId = 2
            };

            var boardDto = item.FromBoardToUpdateDto();

            context.Boards.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var service = new BoardService(context, currentUserService.Object);

            // Act
            async Task act() => await service.UpdateNameAsync(boardDto);

            // Assert
            await Assert.ThrowsAsync<BoardHasDifferentOwnerException>(act);
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
