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
    public class tiposController : Controller
    {
        private readonly guachincheContext _context;

        public tiposController(guachincheContext context)
        {
            _context = context;
        }

        // GET: tipos
        public async Task<IActionResult> Index()
        {
              return _context.tipos != null ? 
                          View(await _context.tipos.ToListAsync()) :
                          Problem("Entity set 'guachincheContext.tipos'  is null.");
        }

        // GET: tipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tipos == null)
            {
                return NotFound();
            }

            var tipo = await _context.tipos
                .FirstOrDefaultAsync(m => m.id == id);
            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }

        // GET: tipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre")] tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        // GET: tipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tipos == null)
            {
                return NotFound();
            }

            var tipo = await _context.tipos.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        // POST: tipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre")] tipo tipo)
        {
            if (id != tipo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tipoExists(tipo.id))
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
            return View(tipo);
        }

        // GET: tipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tipos == null)
            {
                return NotFound();
            }

            var tipo = await _context.tipos
                .FirstOrDefaultAsync(m => m.id == id);
            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }

        // POST: tipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tipos == null)
            {
                return Problem("Entity set 'guachincheContext.tipos'  is null.");
            }
            var tipo = await _context.tipos.FindAsync(id);
            if (tipo != null)
            {
                _context.tipos.Remove(tipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tipoExists(int id)
        {
          return (_context.tipos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
