using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class BoardHasDifferentOwnerException : ApplicationBaseException
    {
        public BoardHasDifferentOwnerException() : base("Board has different owner", HttpStatusCode.Forbidden)
        {
            
        }
    }
}
