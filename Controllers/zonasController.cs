using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    [Authorize(Roles = "Default,Admin")]
    public class zonasController : Controller
    {
        private readonly guachincheContext _context;

        public zonasController(guachincheContext context)
        {
            _context = context;
        }

        // GET: zonas
        public async Task<IActionResult> Index()
        {
              return _context.zonas != null ? 
                          View(await _context.zonas.ToListAsync()) :
                          Problem("Entity set 'guachincheContext.zonas'  is null.");
        }

        // GET: zonas/Details/5

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.zonas == null)
            {
                return NotFound();
            }

            var zona = await _context.zonas
                .Include(e=> e.restaurantes)
                .ThenInclude(i =>i.Id_tipoNavigation)
                .FirstOrDefaultAsync(m => m.Zona_id == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // GET: zonas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: zonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Zona_id,nombre,descripcion")] Zona zona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        // GET: zonas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.zonas == null)
            {
                return NotFound();
            }

            var zona = await _context.zonas.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }
            return View(zona);
        }

        // POST: zonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Zona_id,nombre,descripcion")] Zona zona)
        {
            if (id != zona.Zona_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!zonaExists(zona.Zona_id))
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
            return View(zona);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.zonas == null)
            {
                return NotFound();
            }

            var zona = await _context.zonas
                .FirstOrDefaultAsync(m => m.Zona_id == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // POST: zonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.zonas == null)
            {
                return Problem("Entity set 'guachincheContext.zonas'  is null.");
            }
            var zona = await _context.zonas.FindAsync(id);
            if (zona != null)
            {
                _context.zonas.Remove(zona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool zonaExists(int id)
        {
          return (_context.zonas?.Any(e => e.Zona_id == id)).GetValueOrDefault();
        }
    }
}
