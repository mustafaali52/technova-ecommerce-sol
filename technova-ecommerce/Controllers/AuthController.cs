using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using technova_ecommerce.Models;
using technova_ecommerce.Models.Entities;

namespace technova_ecommerce.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user) { 
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u=> u.UserName.ToLower().Equals(user.UserName.ToLower())))
                {
                    var loggedInUser = await  _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(user.UserName.ToLower()));
                    if (BCrypt.Net.BCrypt.Verify(user.HashedPassword, loggedInUser.HashedPassword)) {
                        var token = GenerateToken(loggedInUser);
                        Response.Cookies.Append("jwt_token", token);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ViewBag.ErrorMessage = "Invalid Password!";
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Username!";
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
            string userName = user.UserName;

            if (_context.Users.Any(u=>u.UserName.ToLower().Equals(userName.ToLower())))
            {
                ViewBag.ErrorMessage = "Username already exists! Please try another Username";
                return View(user);
            }

            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            return RedirectToAction("Login", "Auth");
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
                new System.Security.Claims.Claim(ClaimTypes.Role, user.Role ?? "Public")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(
                "class-work-5B"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
