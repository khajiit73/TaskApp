using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services.Dtos
{
    public record CreateBoardDto(string? Name);

    public static class CreateBoardDtoExtensions
    {
        public static CreateBoardDto FromBoardToCreateDto(this Board board) => new
         (
             Name: board.Name
         );

        public static Board FromCreateDtoToBoard(this CreateBoardDto boardDto, ICurrentUserService _currentUserService)
        {
            return new Board
            {
                Name = boardDto.Name,
                OwnerId = _currentUserService.UserId
            };
        }
    }
}
