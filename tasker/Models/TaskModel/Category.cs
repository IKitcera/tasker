using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace tasker.Models.TaskModel
{
    [Owned]
    public class Category
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Id { get; set; }
        List<Task> tasks = new List<Task>();
        public List<Task> Tasks { get => tasks; set => tasks = value; }
    }
}
