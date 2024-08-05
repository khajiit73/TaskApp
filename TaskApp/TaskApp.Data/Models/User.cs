using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<TaskItem> Tasks { get; set; }

        public ICollection<Board> Boards { get; set; }
    }
}
