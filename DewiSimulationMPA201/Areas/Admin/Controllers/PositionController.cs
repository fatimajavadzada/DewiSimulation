using DewiSimulationMPA201.Contexts;
using DewiSimulationMPA201.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DewiSimulationMPA201.Areas.Admin.Controllers;
[Area("Admin")]
public class PositionController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var positions = await _context.Positions.ToListAsync();
        return View(positions);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Position position)
    {
        if (!ModelState.IsValid)
        {
            return View(position);
        }

        Position newPosition = new()
        {
            Id = position.Id,
            Name = position.Name,
        };

        await _context.Positions.AddAsync(newPosition);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var existPosition = await _context.Positions.FindAsync(id);

        if (existPosition is null)
        {
            return NotFound();
        }

        return View(existPosition);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Position position)
    {
        if (!ModelState.IsValid)
        {
            return View(position);
        }

        var existPosition = await _context.Positions.FindAsync(position.Id);

        if (existPosition is null)
        {
            return NotFound();
        }

        existPosition.Name = position.Name;

        _context.Positions.Update(existPosition);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var existPosition = await _context.Positions.FindAsync(id);

        if (existPosition is null)
        {
            return NotFound();
        }

        _context.Positions.Remove(existPosition);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
