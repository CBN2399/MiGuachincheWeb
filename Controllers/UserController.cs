using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System.Data;

namespace MiGuachincheWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly guachincheContext _guachincheContext;
        private readonly UserManager<CustomUser> _userManager;

        public UserController(guachincheContext guachincheContext, UserManager<CustomUser> userManager)
        {
            _guachincheContext = guachincheContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _guachincheContext.Users.ToListAsync();
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            users.RemoveAll(u => u.Id == currentUser.Result.Id);
            Console.WriteLine("hecho");
            return View(users);
        }
    }
}
