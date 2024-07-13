﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Data.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
