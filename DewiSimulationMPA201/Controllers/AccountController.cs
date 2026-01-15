using DewiSimulationMPA201.Models;
using DewiSimulationMPA201.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DewiSimulationMPA201.Controllers;
public class AccountController(SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager) : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var existUser = await _userManager.FindByNameAsync(vm.UserName);

        if (existUser is { })
        {
            ModelState.AddModelError("UserName", "This username is already exist~");
            return View(vm);
        }

        existUser = await _userManager.FindByEmailAsync(vm.Email);
        if (existUser is { })
        {
            ModelState.AddModelError("Email", "This email is already exist!");
            return View(vm);
        }

        AppUser newUser = new()
        {
            FullName = vm.FullName,
            UserName = vm.UserName,
            Email = vm.Email
        };

        var result = await _userManager.CreateAsync(newUser, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }

        await _userManager.AddToRoleAsync(newUser, "Member");
        await _signInManager.SignInAsync(newUser, false);

        return RedirectToAction("Index", "Home");

    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Email or password is wrong");
            return View(vm);
        }

        var result = await _userManager.CheckPasswordAsync(user, vm.Password);

        if (result == false)
        {
            ModelState.AddModelError("", "Email or password is wrong");
            return View(vm);
        }

        await _signInManager.SignInAsync(user, vm.IsRemember);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> CreateRoles()
    {
        await _roleManager.CreateAsync(new IdentityRole()
        {
            Name = "Admin"
        });

        await _roleManager.CreateAsync(new IdentityRole()
        {
            Name = "Member"
        });

        await _roleManager.CreateAsync(new IdentityRole()
        {
            Name = "Moderator"
        });

        return Ok("Roles was created successfully!");
    }

}
