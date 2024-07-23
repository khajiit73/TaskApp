using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class ApplicationBaseException : Exception
    {
        public HttpStatusCode Status { get; }

        public ApplicationBaseException(string message, HttpStatusCode status) : base(message)
        {
            Status = status;
        }
    }
}
