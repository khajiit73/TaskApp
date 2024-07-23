using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Exceptions
{
    public class BoardNotFoundException : ApplicationBaseException
    {
        public BoardNotFoundException() : base("Board not found", HttpStatusCode.NotFound)
        {
            
        }
    }
}
