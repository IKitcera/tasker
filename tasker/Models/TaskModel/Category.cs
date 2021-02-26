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
        public Category()
        {

        }
        public void Add(string newItem)
        {
            tasks.Add(new Task(newItem, false));
        }
       
        public void Update(string oldItem, string newItem)
        {
            var taskToUpdate = Find(oldItem);
            if (taskToUpdate != null)
            {
                taskToUpdate.taskName = newItem;
            }
        }
        public void ChangeStatus(Task task)
        {
            var t = tasks.Find(t => t == task);
            if (t.isDone)
            {
                t.isDone = false;
            }
            else
            {
                t.isDone = true;
            }
            Sort();
        }
        private void Sort()
        {
            List<Task> Checked = new List<Task>();
            List<Task> Unchecked = new List<Task>();

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].isDone)
                {
                    Checked.Add(tasks[i]);
                }
                else
                {
                    Unchecked.Add(tasks[i]);
                }
            }
            tasks.Clear();
            foreach (var u in Unchecked)
            {
                tasks.Add(u);
            }
            foreach (var c in Checked)
            {
                tasks.Add(c);
            }
        }
        private Task Find(string itemName)
        {
            foreach (var task in tasks)
            {
                if (task.taskName == itemName)
                {
                    return task;
                }
            }
            return null;
        }

    }
}
