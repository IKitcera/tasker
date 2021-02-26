using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using tasker.Data;
using tasker.Models.TaskModel;
using System.Threading.Tasks;

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
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = GetTaskManager().categories.Where(c => c.Id == categoryId).FirstOrDefault();
            GetTaskManager().categories.Remove(category);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddCategory(string categoryName)
        {
            Random r = new Random();
            string randomColor = "rgb(" + r.Next(0, 255).ToString() + "," + r.Next(0, 255).ToString() + "," + r.Next(0, 255).ToString() + ")";
            Category category = new Category { Name = categoryName, Color = randomColor, Tasks = null };
            GetTaskManager().categories.Add(category);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id, string newName)
        {
            GetTaskManager().categories.Where(c => c.Id == id).FirstOrDefault().Name = newName;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int categoryId, int taskId)
        {
            var category = GetTaskManager().categories.Where(c => c.Id == categoryId).FirstOrDefault();
            category.Tasks.Remove(category.Tasks.Find(task => task.Id == taskId));

            db.SaveChangesAsync();
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
        public IActionResult CheckTask(int catId, Models.TaskModel.Task task)
        {
            GetTaskManager().categories.Where(c => c.Id == catId).FirstOrDefault().ChangeStatus(task);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private TaskManager GetTaskManager()
        {
            return db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().taskManager;
        }
    }
}