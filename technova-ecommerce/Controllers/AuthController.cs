using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using technova_ecommerce.Models;
using technova_ecommerce.Models.Entities;

namespace technova_ecommerce.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseContext _dbContext;
        public AuthController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(User user) {
            if (_dbContext.Users.Any(u => u.UserName.ToLower().Equals(user.UserName.ToLower())))
            {
                ViewBag.ErrorMessage = "Username already exists! Please try another Username";
                return View(user);
            }
            else
            {
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }
        } 

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login (User user)
        {
            if (!_dbContext.Users.Any(u => u.UserName.ToLower().Equals(user.UserName.ToLower()))) {
                ViewBag.ErrorMessage = "User doesn't exists!";
                return View(user);
            }
            else {
                var hashedPassword = _dbContext.Users.FirstOrDefault(u => u.UserName.ToLower().Equals(user.UserName)).HashedPassword;
                if (BCrypt.Net.BCrypt.Verify(user.HashedPassword, hashedPassword))
                {
                    Response.Cookies.Append("jwt_token", GenerateJwtToken(_dbContext.Users.FirstOrDefault(u => u.UserName.ToLower().Equals(user.UserName))));
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ViewBag.ErrorMessage = "Incorrect password! Please enter correct password";
                    return View(user);
                }
            }
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role ?? "Public")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes("class-work-5A"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
