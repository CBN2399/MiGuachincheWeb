using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<CustomUserDTO> userList = new List<CustomUserDTO>();
            foreach(CustomUser user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userList.Add(new CustomUserDTO
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Role = role[0].ToString()
                });
            }
            
            return View(userList);
        }

        [Authorize(Roles = "Default,Manager")]
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

        [Authorize(Roles = "Default,Manager")]
        public async Task<IActionResult> Edit(String? id)
        {
            if (id == null || _guachincheContext.custom_users == null)
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
            CustomUserDTO userDTO = new CustomUserDTO(user.Id,user.Nombre,user.Apelllidos,user.Telefono,user.Email);

            return View(userDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Default,Manager")]
        public async Task<IActionResult> Edit(String? id, [Bind("Id,Nombre,Apellidos,Telefono")] CustomUserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            var userSelected = await _guachincheContext.custom_users.FindAsync(id);
            if (userSelected == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
               
                try
                {
                    userSelected.Nombre = user.Nombre;
                    userSelected.Apelllidos = user.Apellidos;
                    userSelected.Telefono = user.Telefono;
                    _guachincheContext.Update(userSelected);
                    await _guachincheContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userSelected.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","User", new{ id = user.Id });
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(String? id)
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
            var adminUser = _userManager.GetUserAsync(HttpContext.User);
            if (user.Id.Equals(adminUser.Result.Id))
            {
                return BadRequest();
            }
            _guachincheContext.custom_users.Remove(user);
            await _guachincheContext.SaveChangesAsync();

            return RedirectToAction("Index","User");
        }




        [Authorize(Roles = "Default")]
        public async Task<IActionResult> RestList(String? id)
        {
            if (id == null || _guachincheContext.custom_users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users
                .Include(r => r.restaurantes)
                .ThenInclude(i => i.Id_tipoNavigation)
                .Include(r => r.restaurantes)
                .ThenInclude(i => i.zona)
                .FirstOrDefaultAsync(u => u.Id == id);
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
            if (id == null || _guachincheContext.custom_users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users
                .Include(r => r.platos)
                .ThenInclude(e => e.restaurante)
                .Include(r => r.platos)
                .ThenInclude(e => e.plato)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }

            List<PlatoRestaurante> platos = new List<PlatoRestaurante>();
            if(user.platos != null)
            {
                platos = user.platos.ToList();
            }

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

        public async Task<IActionResult> AddPlato(int? id)
        {
            if (id == null || _guachincheContext.plato_restaurantes == null)
            {
                return NotFound();
            }

            var platoRest = await _guachincheContext.plato_restaurantes.FindAsync(id);
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var user = await _guachincheContext.custom_users.Include(p => p.platos).FirstOrDefaultAsync(e => e.Id == currentUser.Result.Id);
            if ((platoRest == null) || (user == null))
            {
                return NotFound();
            }

            if(user.platos == null)
            {
                user.platos = new List<PlatoRestaurante>();
            }

            if(!user.platos.Contains(platoRest))
            {
                user.platos.Add(platoRest);
                await _guachincheContext.SaveChangesAsync();
            }
            return NoContent();
        }

        public async Task<IActionResult> DeletePLato(int? id)
        {
            if (id == null || _guachincheContext.plato_restaurantes == null)
            {
                return NotFound();
            }

            var platoRest = await _guachincheContext.plato_restaurantes.FindAsync(id);
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var user = await _guachincheContext.custom_users.Include(p => p.platos).FirstOrDefaultAsync(e => e.Id == currentUser.Result.Id);
            if ((platoRest == null) || (user == null))
            {
                return NotFound();
            }

            if(user.platos != null) 
            {
                if (!user.platos.Contains(platoRest))
                {
                    return BadRequest();
                }
                user.platos.Remove(platoRest);
                await _guachincheContext.SaveChangesAsync();
            }
            return RedirectToAction("PlatoList", "User", new { id = user.Id });
        }

        private bool UserExists(String id)
        {
            return (_guachincheContext.custom_users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
