using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services.Dtos
{
    public record GetBoardDto(string? Name, DateTime CreatedAt, string UserId);

    public static class GetBoardDtoExtensions
    {
        public static GetBoardDto FromBoardToGetDto(this Board board) => new
         (
             Name: board.Name,
             CreatedAt: board.CreatedAt,
             UserId: board.OwnerId
         );

        public static void FromGetDtoToBoard(this GetBoardDto boardDto, Board board)
        {
            board.Name = boardDto.Name;
            board.CreatedAt = boardDto.CreatedAt;
            board.OwnerId = boardDto.UserId;
        }
    }
}
