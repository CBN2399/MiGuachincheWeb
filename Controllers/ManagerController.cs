using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    [Authorize (Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly guachincheContext _context;
        private readonly UserManager<CustomUser> _userManager;

        public ManagerController(guachincheContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: ManagerController
        public async Task<IActionResult> List(String? id)
        {
            if((id == null) || (_context.custom_users == null))
            {
                return NotFound();
            }
            var manager = await _context.custom_users
                .Include(e => e.restaurantes)
                .ThenInclude(e => e.zona)
                .Include(e => e.restaurantes)
                .ThenInclude(e => e.Id_tipoNavigation)
                .FirstOrDefaultAsync(e => e.Id == id);
            if(manager == null)
            {
                return NotFound();
            }

            ViewData["platos"] = new SelectList(_context.platos,"PlatoId","Nombre");
            ManagerDTO datos = new ManagerDTO();
            datos.restaurantes = manager.restaurantes.ToList();
            return View(datos);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlato([Bind("restauranteId,platoId,managerId")] ManagerDTO plato) 
        {
            if(ModelState.IsValid)
            {
                var restaurante = await _context.restaurantes
                    .Include(p => p.platos)
                    .FirstOrDefaultAsync(e => e.RestauranteId== plato.restauranteId);
                var platoSelected = await _context.platos.FindAsync(plato.platoId);
                if((restaurante == null) ||(platoSelected == null))
                {
                    return NotFound();
                }

                if(restaurante.platos == null)
                {
                    restaurante.platos = new List<Plato>();
                }

                if (!restaurante.platos.Contains(platoSelected))
                {
                    PlatoRestaurante platoRestaurante = new PlatoRestaurante();
                    platoRestaurante.plato = platoSelected;
                    platoRestaurante.restaurante = restaurante;
                    platoRestaurante.plato_Id = platoSelected.PlatoId;
                    platoRestaurante.restaurante_Id = restaurante.RestauranteId;
                    platoRestaurante.valoracion = 1;
                    platoRestaurante.activo = true;
                    _context.Add(platoRestaurante);
                    await _context.SaveChangesAsync();
                }   
            }
            return RedirectToAction("List","Manager", new {id = plato.managerId});
        }
        public async Task<IActionResult> DeletePlato(int? id)
        {
            if((id == null) || (_context.plato_restaurantes == null))
            {
                return NotFound();
            }
            var plato = await _context.plato_restaurantes.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            var restaurante = await _context.restaurantes
                .Include(e => e.platos)
                .FirstOrDefaultAsync(e => e.RestauranteId == plato.restaurante_Id);
            if (restaurante == null)
            {
                return NotFound();
            }
            restaurante.platos.Remove(plato.plato);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "restaurantes", new { id = plato.restaurante_Id });
        }

        public async Task<IActionResult> Reservas()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var manager = await _context.custom_users.Include(e => e.reservas).FirstOrDefaultAsync(e => e.Id == currentUser.Result.Id);
            if (manager == null)
            {
                return BadRequest();
            }
            return View(manager.reservas);
        }
    }
}
