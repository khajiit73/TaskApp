﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
