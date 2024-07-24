using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using TaskApp.Services.Dtos;

namespace TaskApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<GetBoardDto>> GetAllAsync();
        Task<GetBoardDto> GetAsync(int id);
        Task CreateAsync(CreateBoardDto board);
        Task UpdateNameAsync(int id, UpdateBoardDto boardDto);
        Task DeleteAsync(int id);
    }
}
