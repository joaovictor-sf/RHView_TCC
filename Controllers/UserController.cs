using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHView_TCC.Data;
using RHView_TCC.Models;
using RHView_TCC.Models.DTOs;
using RHView_TCC.Models.Enuns;

namespace RHView_TCC.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            var usuariosAtivos = _context.Users
                .Where(u => u.IsActive && u.Role == UserRole.DEV)
                .Select(u => new {
                    u.Id,
                    u.Name,
                    u.LastName,
                    u.Role
                })
                .AsEnumerable()
                .Select(u => new UserListDTO
                {
                    Id = u.Id,
                    Nome = u.Name,
                    Sobrenome = u.LastName,
                    Role = u.Role.ToString()
                })
                .ToList();

            return View(usuariosAtivos);
        }

        public async Task<IActionResult> Detalhes(int id, DateTime? dataSelecionada = null) {
            var datas = await _context.DailyWorkLogs
                .Where(l => l.UserId == id)
                .OrderByDescending(l => l.Date)
                .Select(l => l.Date)
                .ToListAsync();

            if (!datas.Any()) {
                ViewBag.Mensagem = "Nenhum dado encontrado para esse usuário.";
                return View("Detalhes", new DailyWorkLog());
            }

            var data = dataSelecionada ?? datas.First(); // usa a data mais recente se nenhuma for escolhida

            var log = await _context.DailyWorkLogs
                .FirstOrDefaultAsync(l => l.UserId == id && l.Date == data);

            ViewBag.Datas = datas;
            ViewBag.DataSelecionada = data;
            ViewBag.UserId = id;

            return View(log);
        }

        public IActionResult DetalhesPorData(int userId, DateTime dataSelecionada) {
            // Por enquanto só redireciona para Detalhes novamente
            return RedirectToAction("Detalhes", new { id = userId });
        }

    }
}
