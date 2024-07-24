using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class InvalidStatusChangeException : ApplicationBaseException
    {
        public InvalidStatusChangeException() : base("Invalid status change", HttpStatusCode.BadRequest)
        {
            
        }
    }
}
