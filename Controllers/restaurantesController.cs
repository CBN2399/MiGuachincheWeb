using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    public class restaurantesController : Controller
    {
        private readonly guachincheContext _context;

        public restaurantesController(guachincheContext context)
        {
            _context = context;
        }

        // GET: restaurantes
        public async Task<IActionResult> Index()
        {
            var guachincheContext = _context.restaurantes.Include(r => r.Id_tipoNavigation).Include(r => r.zona);
            return View(await guachincheContext.ToListAsync());
        }

        // GET: restaurantes/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: restaurantes/Create
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
        public async Task<IActionResult> Create([Bind("RestauranteId,Nombre,Rest_Url,telefono,valoracion,Id_tipo,zonaId")] restaurante restaurante)
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
        public async Task<IActionResult> Edit(int id, [Bind("RestauranteId,Nombre,Rest_Url,telefono,valoracion,Id_tipo,zonaId")] restaurante restaurante)
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
