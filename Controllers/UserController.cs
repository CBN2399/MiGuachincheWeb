using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System.Data;

namespace MiGuachincheWeb.Controllers
{
    [Authorize(Roles = "Default")]
    public class UserController : Controller
    {
        private readonly guachincheContext _guachincheContext;
        private readonly UserManager<CustomUser> _userManager;

        public UserController(guachincheContext guachincheContext, UserManager<CustomUser> userManager)
        {
            _guachincheContext = guachincheContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _guachincheContext.Users.ToListAsync();
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            users.RemoveAll(u => u.Id == currentUser.Result.Id);
            return View(users);
        }

        public async Task<IActionResult> Details(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }

            var currentUser =  _userManager.GetUserAsync(HttpContext.User);
            CustomUser user = currentUser.Result;
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            CustomUser user = currentUser.Result;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> restList(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            List<restaurante> restList = user.restaurantes.ToList();

            return View(restList);
        }
    }
}
