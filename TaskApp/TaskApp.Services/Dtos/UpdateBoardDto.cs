using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services.Dtos
{
    public record UpdateBoardDto(int Id, string? Name, int OwnerId);

    public static class UpdateBoardDtoExtensions
    {
        public static UpdateBoardDto FromBoardToUpdateDto(this Board board) => new
         (
             Id: board.Id,
             Name: board.Name,
             OwnerId: board.OwnerId
         );

        public static void FromUpdateDtoToBoard(this UpdateBoardDto boardDto, Board board)
        {
            board.Id = boardDto.Id;
            board.Name = boardDto.Name;
            board.OwnerId = boardDto.OwnerId;
        }
    }
}
