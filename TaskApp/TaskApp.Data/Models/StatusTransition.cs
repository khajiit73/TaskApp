using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Data.Models
{
    public class StatusTransition
    {
        public int Id { get; set; }
        public int CurrentStatusId { get; set; }
        public int NextStatusId { get; set; }
        public Status? CurrentStatus { get; set; }
        public Status? NextStatus { get; set; }
    }
}
