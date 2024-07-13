using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Data.Models
{
    internal class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BoardId { get; set; }

        public int StatusId { get; set; }

        public int AssigneeId { get; set; }

        public Board Board { get; set; }

        public Status Status { get; set; }

        public User Assignee { get; set; }
    }
}
