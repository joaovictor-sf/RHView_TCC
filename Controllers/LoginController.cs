using Microsoft.AspNetCore.Mvc;
using RHView_TCC.Data;
using RHView_TCC.Models;
using RHView_TCC.Models.DTOs;
using RHView_TCC.Models.Enuns;

namespace RHView_TCC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Index(LoginDTO model) {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users
                .Where(u => u.Username == model.Username)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.PasswordHash,
                    u.Name,
                    u.LastName,
                    u.Email,
                    u.IsActive,
                    u.MustChangePassword,
                    Role = u.Role.ToString(),
                    WorkHours = u.WorkHours
                })
                .AsEnumerable()
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    PasswordHash = u.PasswordHash,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    MustChangePassword = u.MustChangePassword,
                    Role = Enum.Parse<UserRole>(u.Role),
                    WorkHours = u.WorkHours
                })
                .FirstOrDefault();

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash)) {
                model.ErrorMessage = "Usuário ou senha inválidos.";
                return View(model);
            }

            if (!user.IsActive) {
                model.ErrorMessage = "Usuário não mais ativo.";
                return View(model);
            }

            if (user.Role == UserRole.DEV) {
                model.ErrorMessage = "Usuários com o papel DEV não podem acessar o sistema.";
                return View(model);
            }

            // Armazenar ID em sessão para login básico
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
