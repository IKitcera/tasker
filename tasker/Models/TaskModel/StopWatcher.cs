using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace tasker.Models.TaskModel
{
    [Owned]
    public class StopWatcher
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Become a programmer";
        public long totalMinutes { get; set; } = 0;
        
    }
}
