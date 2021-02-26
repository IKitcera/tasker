using Microsoft.EntityFrameworkCore;
using System;

namespace tasker.Models.TaskModel
{
    [Owned]
    public class Task : IComparable<Task>
    {
        public int Id { get; set; }
        public string taskName { get; set; }
        public bool isDone { get; set; }
        public Task()
        {

        }
        public Task(string taskName, bool isDone)
        {
            this.taskName = taskName;
            this.isDone = isDone;
        }

        public int CompareTo(Task other)
        {
            return taskName.CompareTo(other.taskName);
        }
    }
}
