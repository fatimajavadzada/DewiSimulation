using DewiSimulationMPA201.Contexts;
using DewiSimulationMPA201.Helpers;
using DewiSimulationMPA201.Models;
using DewiSimulationMPA201.ViewModels.TeamMemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DewiSimulationMPA201.Areas.Admin.Controllers;
[Area("Admin")]
public class TeamMemberController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await SendPositionItemsWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeamMemberCreateVM vm)
    {
        await SendPositionItemsWithViewBag();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (!vm.ImagePath.CheckFileSize(2))
        {
            ModelState.AddModelError("ImagePath", "Image size cannot be less than 2MB!");
            return View(vm);
        }

        if (!vm.ImagePath.CheckFileType("image"))
        {
            ModelState.AddModelError("ImagePath", "Image type must be in IMAGE format!");
            return View(vm);
        }

        var existPosition = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);

        if (existPosition is false)
        {
            ModelState.AddModelError("PositionId", "Position is not found!");
            return View(vm);
        }

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");
        string imageName = vm.ImagePath.SaveFile(folderPath);

        TeamMember newMember = new TeamMember()
        {
            FullName = vm.FullName,
            PositionId = vm.PositionId,
            ImagePath = imageName
        };

        await _context.TeamMembers.AddAsync(newMember);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        await SendPositionItemsWithViewBag();
        var existMember = await _context.TeamMembers.FindAsync(id);

        if (existMember is null)
        {
            return NotFound();
        }

        TeamMemberUpdateVM vm = new()
        {
            Id = existMember.Id,
            FullName = existMember.FullName,
            PositionId = existMember.PositionId
        };

        _context.TeamMembers.Update(existMember);
        await _context.SaveChangesAsync();

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeamMemberUpdateVM vm)
    {
        await SendPositionItemsWithViewBag();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (!vm.ImagePath?.CheckFileSize(2) ?? false)
        {
            ModelState.AddModelError("ImagePath", "Image size cannot be less than 2MB!");
            return View(vm);
        }

        if (!vm.ImagePath?.CheckFileType("image") ?? false)
        {
            ModelState.AddModelError("ImagePath", "Image type must be in IMAGE format!");
            return View(vm);
        }

        var existPosition = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);

        if (existPosition is false)
        {
            ModelState.AddModelError("PositionId", "Position is not found!");
            return View(vm);
        }

        var existMember = await _context.TeamMembers.FindAsync(vm.Id);

        if (existMember is null)
        {
            return NotFound();
        }

        existMember.FullName = vm.FullName;
        existMember.PositionId = vm.PositionId;

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");

        if (vm.ImagePath is { })
        {
            string imageName = vm.ImagePath.SaveFile(folderPath);

            if (System.IO.File.Exists(Path.Combine(folderPath, existMember.ImagePath)))
            {
                System.IO.File.Delete(Path.Combine(folderPath, existMember.ImagePath));
            }

            existMember.ImagePath = imageName;
        }

        _context.TeamMembers.Update(existMember);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var existMember = await _context.TeamMembers.FindAsync(id);

        if (existMember is null)
        {
            return NotFound();
        }

        _context.TeamMembers.Remove(existMember);
        await _context.SaveChangesAsync();

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");

        if (System.IO.File.Exists(Path.Combine(folderPath, existMember.ImagePath)))
        {
            System.IO.File.Delete(Path.Combine(folderPath, existMember.ImagePath));
        }

        return RedirectToAction("Index");
    }
    private async Task SendPositionItemsWithViewBag()
    {
        var positions = await _context.Positions.ToListAsync();
        ViewBag.Positions = positions;
    }
}
