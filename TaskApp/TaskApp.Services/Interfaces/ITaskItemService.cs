using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using TaskApp.Services.Dtos;

namespace TaskApp.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<GetTaskItemDto>> GetAllAsync();

        Task<GetTaskItemDto> GetAsync(int id);

        Task CreateAsync(CreateTaskItemDto itemDto);

        Task UpdateAsync(UpdateTaskItemDto itemDto);

        Task DeleteAsync(int id);
    }
}
