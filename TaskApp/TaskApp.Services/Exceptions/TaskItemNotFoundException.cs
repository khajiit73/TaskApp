using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class TaskItemNotFoundException : ApplicationBaseException
    {
        public TaskItemNotFoundException() : base("Task item not found", HttpStatusCode.NotFound)
        {
            
        }
    }
}
