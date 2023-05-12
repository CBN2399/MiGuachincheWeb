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
            ManagerDTO datos = new ManagerDTO(manager.restaurantes.ToList());
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
                    restaurante.platos.Add(platoSelected);
                    await _context.SaveChangesAsync();
                }   
            }
            return RedirectToAction("List","Manager", new {id = plato.managerId});
        }
        

       

        

       
    }
}
