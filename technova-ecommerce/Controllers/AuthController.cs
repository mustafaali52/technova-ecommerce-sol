using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using technova_ecommerce.Models.Entities;

namespace technova_ecommerce.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseContext _db;
        public AuthController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.UserName == user.UserName))
                {
                    var loggedInUser = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName));

                    if (BCrypt.Net.BCrypt.Verify(user.HashedPassword, loggedInUser.HashedPassword))
                        return RedirectToAction("Index", "Home");
                    else
                        ViewBag.ErroMessage = "Incorrect Password!";
                }
                else
                {
                    ViewBag.ErroMessage = "Incorrect Username!";
                    return View(user);
                }                    
            }

            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.UserName == user.UserName))
                {
                    ViewBag.ErroMessage = "Username already exists! Please try another Username";
                    return View(user);
                }

                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);

                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login", "Auth");
            }

            return View(user);
        }
    }
}
