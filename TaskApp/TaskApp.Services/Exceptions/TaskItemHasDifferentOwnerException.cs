using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class TaskItemHasDifferentOwnerException : ApplicationBaseException
    {
        public TaskItemHasDifferentOwnerException() : base("Task item has different owner", HttpStatusCode.Forbidden)
        {
            
        }
    }
}
