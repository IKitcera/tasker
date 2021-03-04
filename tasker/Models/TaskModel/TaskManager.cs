using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace tasker.Models.TaskModel
{


    [Owned]
    public class TaskManager
    {
        public int Id { get; set; }
        public List<Category> categories { get; set; } = new List<Category>();
        public StopWatcher stopWatcher { get; set; } = new StopWatcher();
    }

    public static class InitTaskManager
    {
        public static TaskManager firstTM = new TaskManager
        {
            categories = new List<Category> {
            new Category { Name="Must do", Color="red",Tasks = new List<Task>{new Task{taskName="This is a very important task",isDone=false},
                new  Task{taskName="This is a very important task too",isDone=false}}},
             new Category { Name="Have to do", Color="green", Tasks = new List<Task>{new Task{taskName="This is an important task, I think",isDone=false},
                new  Task{taskName="This is another important task",isDone=false}}},
             new Category { Name="Wish to do", Color="blue", Tasks = new List<Task>{new Task{taskName="This is the task I want to do",isDone=false},
                new  Task{taskName="Task from the same categ",isDone=false}}},
             new Category { Name="May do", Color="dimgrey", Tasks = new List<Task>{new Task{taskName="This is additional task for today",isDone=false},
                new  Task{taskName="Another additional task",isDone=false}}} }
        };
    }
}
