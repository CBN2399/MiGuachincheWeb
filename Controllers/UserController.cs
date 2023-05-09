using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System.Data;
using System.Linq;

namespace MiGuachincheWeb.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Details(String? id)
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

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }

            return View(user);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Edit(String? id)
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

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }
            return View(user);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> RestList(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users
                .Include(r => r.restaurantes)
                .ThenInclude(i => i.Id_tipoNavigation)
                .Include(e => e.restaurantes)
                .ThenInclude(i => i.zona).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if(!user.Id.Equals(currentUser.Result.Id)) 
            {
                return BadRequest();
            }

            List<Restaurante> restaurantes = new List<Restaurante>();
            if(user.restaurantes != null)
            {
                restaurantes = user.restaurantes.ToList();
            }
            return View(restaurantes);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> PlatoList(String? id)
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

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }

            List<PlatoRestaurante> platos = user.platos.ToList();

            return View(platos);
        }

        public async Task<IActionResult> AddRestaurante(int? id)
        {
            if (id == null || _guachincheContext.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante =  await _guachincheContext.restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var user =  await _guachincheContext.custom_users.Include(r => r.restaurantes).FirstOrDefaultAsync(e => e.Id == currentUser.Result.Id);
            if (user == null)
            {
                return NotFound();
            }


            UserRestaurante userRest = new UserRestaurante();
            if (user.restaurantes != null)
            {
                if (!user.restaurantes.Contains(restaurante))
                {
                    userRest.restaurante = restaurante;
                    userRest.customUser = user;
                    userRest.usuario_Id = user.Id;
                    userRest.restaurante_Id = restaurante.RestauranteId;
                }
            }
            
            _guachincheContext.Add(userRest);
            await _guachincheContext.SaveChangesAsync();
            return NoContent();

        }

        public async Task<IActionResult> DeleteRestaurante(int? id)
        {
            if (id == null || _guachincheContext.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _guachincheContext.restaurantes.FindAsync(id);
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var user = await _guachincheContext.custom_users.Include(r => r.restaurantes).FirstOrDefaultAsync(e => e.Id == currentUser.Result.Id);
            if ((user == null) || (restaurante == null))
            {
                return NotFound();
            }

            if(user.restaurantes != null)
            {
                if (!user.restaurantes.Contains(restaurante))
                {
                    return BadRequest();
                }

                user.restaurantes.Remove(restaurante);
                await _guachincheContext.SaveChangesAsync();

            }

            return RedirectToAction("RestList","User", new { id = user.Id});
        }


    }
}
