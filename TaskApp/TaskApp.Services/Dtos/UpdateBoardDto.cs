using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services.Dtos
{
    public record UpdateBoardDto(string? Name);

    public static class UpdateBoardDtoExtensions
    {
        public static UpdateBoardDto FromBoardToUpdateDto(this Board board) => new
         (
             Name: board.Name
         );

        public static void FromUpdateDtoToBoard(this UpdateBoardDto boardDto, Board board)
        {
            board.Name = boardDto.Name;
        }
    }
}
