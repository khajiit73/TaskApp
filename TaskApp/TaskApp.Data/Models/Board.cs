using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Data.Models
{
    public class Board
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TaskItem>? Tasks { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
