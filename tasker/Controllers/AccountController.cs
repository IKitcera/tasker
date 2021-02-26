using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tasker.Data;
using tasker.Models;

namespace tasker.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext appContext;
        public AccountController(ApplicationContext applicationContext)
        {
            appContext = applicationContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                await appContext.Users.LoadAsync();

                var user = await appContext.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (IUser.VerifyHashedPassword(user.Password, model.Password))
                    {
                        await Authenticate(model.Email);
                        return RedirectToAction("Index", "Task");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please, check your data");
                }
            }
            return View(model);
        }

        private async System.Threading.Tasks.Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await appContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    appContext.Users.Add(new User { Email = model.Email, Password = IUser.HashPassword(model.Password), taskManager = Models.TaskModel.InitTaskManager.firstTM });
                    await appContext.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Task");
                }
                else
                    ModelState.AddModelError("", "Incorrect input");
            }
            return View(model);
        }
    }
}
