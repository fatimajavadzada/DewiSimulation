using DewiSimulationMPA201.Contexts;
using DewiSimulationMPA201.ViewModels.TeamMemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DewiSimulationMPA201.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var members = await _context.TeamMembers.Select(x => new TeamMemberGetVM()
            {
                Id = x.Id,
                FullName = x.FullName,
                ImagePath = x.ImagePath,
                PositionName = x.Position.Name
            }).ToListAsync();
            return View(members);
        }
    }
}
