using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using tasker.Data;
using tasker.Models.TaskModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Diagnostics;

namespace tasker.Controllers
{
    public class TaskController : Controller
    {
        private ApplicationContext db;
        TaskManager taskManager;
        public TaskController(ApplicationContext db)
        {
            this.db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            
            if (User.Identity != null)
            {
             
                taskManager = GetTaskManager();
            }
            else
            {
                taskManager = InitTaskManager.firstTM;
            }
            return View(taskManager);
           
        }
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = GetTaskManager().categories.Where(c => c.Id == categoryId).FirstOrDefault();
            GetTaskManager().categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            Random r = new Random();
            string randomColor = "rgb(" + r.Next(0, 255).ToString() + "," + r.Next(0, 255).ToString() + "," + r.Next(0, 255).ToString() + ")";
            Category category = new Category { Name = categoryName, Color = randomColor, Tasks = null };
            GetTaskManager().categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id, string newName)
        {
            GetTaskManager().categories.Where(c => c.Id == id).FirstOrDefault().Name = newName;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTask(int categoryId, int taskId)
        {
            var category = GetTaskManager().categories.Where(c => c.Id == categoryId).FirstOrDefault();
            category.Tasks.Remove(category.Tasks.Find(task => task.Id == taskId));

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(int catId, string taskName)
        {
            try
            {
                Models.TaskModel.Task task = new Models.TaskModel.Task { taskName = taskName, isDone = false };
                var category = GetTaskManager().categories.Where(c => c.Id == catId).FirstOrDefault();
                category.Tasks.Add(task);
                await db.SaveChangesAsync();
            }
            catch
            {
                ModelState.AddModelError("", "any category was selected");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTask(int catId, int taskId, string newName)
        {
            GetTaskManager().categories.Where(c => c.Id == catId).FirstOrDefault().Tasks.Where(t => t.Id == taskId).FirstOrDefault().taskName = newName;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CheckTask(int categoryId, int taskId)
        {
            var category = GetTaskManager().categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var task = category.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            if (task.isDone)
                task.isDone = false;
            else
                task.isDone = true;

            Sort(category.Tasks);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> TimerStop(int minutes)
        {
            GetTaskManager().stopWatcher.totalMinutes += minutes;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> TimerReset()
        {
            GetTaskManager().stopWatcher = new StopWatcher();
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private TaskManager GetTaskManager()
        {
            return db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().taskManager;
        }
       
        private void Sort(List<tasker.Models.TaskModel.Task> tasks)
        {
            List<tasker.Models.TaskModel.Task> Checked = new List<tasker.Models.TaskModel.Task>();
            List<tasker.Models.TaskModel.Task> Unchecked = new List<tasker.Models.TaskModel.Task>();

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
    }
}