using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
    }
}
