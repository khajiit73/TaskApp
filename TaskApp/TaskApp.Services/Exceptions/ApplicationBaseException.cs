using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class ApplicationBaseException(string message, HttpStatusCode status) : Exception(message)
    {
        public HttpStatusCode Status { get; } = status;
    }
}
