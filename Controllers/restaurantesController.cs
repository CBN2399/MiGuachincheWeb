using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    [Authorize]
    public class restaurantesController : Controller
    {
        private readonly guachincheContext _context;

        public restaurantesController(guachincheContext context)
        {
            _context = context;
        }

        // GET: restaurantes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string restZone, List<SelectListItem> zonaRest)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["ValSortParm"] = sortOrder == "Val" ? "Val_asc" : "Val";
            ViewData["CurrentFilter"] = searchString;

            var restaurantes = (IQueryable<Restaurante>)_context.restaurantes.Include(r => r.Id_tipoNavigation).Include(r => r.zona);

            if(restaurantes.Count() != 0)
            {
                if (restZone == null || restZone == "Todos")
                {
                    zonaRest.Add(new SelectListItem { Text = "Todos", Value = "Todos", Selected = true });
                }
                else
                {
                    zonaRest.Add(new SelectListItem { Text = "Todos", Value = "Todos" });
                }

                foreach (string zona in restaurantes.Select(o => o.zona.nombre).Distinct().ToList())
                {
                    if (restZone == zona)
                    {
                        zonaRest.Add(new SelectListItem { Text = zona, Value = zona, Selected = true });
                    }
                    else
                    {
                        zonaRest.Add(new SelectListItem { Text = zona, Value = zona });
                    }
                }
            }

            ViewBag.restZone = zonaRest;

            if (!String.IsNullOrEmpty(searchString))
            {
                restaurantes = restaurantes.Where(s => s.Nombre.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(restZone) && !restZone.Equals("Todos"))
            {
                restaurantes = restaurantes.Where(s => s.zona.nombre.Equals(restZone));
            }

            switch (sortOrder)
            {
                case "Nombre":
                    restaurantes = restaurantes.OrderBy(s => s.Nombre);
                    break;
                case "Val_asc":
                    restaurantes = restaurantes.OrderBy(s => s.valoracion);
                    break;
                case "Val":
                    restaurantes = restaurantes.OrderByDescending(s => s.valoracion);
                    break;
                default:
                    restaurantes = restaurantes.OrderByDescending(s => s.Nombre);
                    break;
            }

            return View(await restaurantes.AsNoTracking().ToListAsync());
        }

        // GET: restaurantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.restaurantes.Include(p => p.platos).ThenInclude(i => i.tipo)
                .Include(e => e.zona).Include(e => e.Id_tipoNavigation).FirstOrDefaultAsync(i => i.RestauranteId == id);
           
            if (restaurante == null)
            {
                return NotFound();
            }

            var platorest = from a in _context.plato_restaurantes
                            where a.restaurante_Id == id
                            select a;
            List<PlatoRestaurante> platos = new List<PlatoRestaurante>();
            if(platorest != null)
            {
                foreach (var a in platorest)
                {
                    platos.Add(a);
                }
            }
            ViewBag.PlatoRest = platos;
            return View(restaurante);
        }

        // GET: restaurantes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre");
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre");
            return View();
        }

        // POST: restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("RestauranteId,Nombre,Rest_Url,telefono,valoracion,Id_tipo,zonaId")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre", restaurante.Id_tipo);
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre", restaurante.zonaId);
            return View(restaurante);
        }

        // GET: restaurantes/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre", restaurante.Id_tipo);
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre", restaurante.zonaId);
            return View(restaurante);
        }

        // POST: restaurantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("RestauranteId,Nombre,Rest_Url,telefono,valoracion,Id_tipo,zonaId")] Restaurante restaurante)
        {
            if (id != restaurante.RestauranteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!restauranteExists(restaurante.RestauranteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre", restaurante.Id_tipo);
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre", restaurante.zonaId);
            return View(restaurante);
        }

        // GET: restaurantes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.restaurantes
                .Include(r => r.Id_tipoNavigation)
                .Include(r => r.zona)
                .FirstOrDefaultAsync(m => m.RestauranteId == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // POST: restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.restaurantes == null)
            {
                return Problem("Entity set 'guachincheContext.restaurantes'  is null.");
            }
            var restaurante = await _context.restaurantes.FindAsync(id);
            if (restaurante != null)
            {
                _context.restaurantes.Remove(restaurante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool restauranteExists(int id)
        {
          return (_context.restaurantes?.Any(e => e.RestauranteId == id)).GetValueOrDefault();
        }
    }
}
