using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class StatusNotFoundException : ApplicationBaseException
    {
        public StatusNotFoundException() : base("Status not found", HttpStatusCode.NotFound)
        {

        }
    }
}
