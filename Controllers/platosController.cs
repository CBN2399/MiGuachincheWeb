﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    public class platosController : Controller
    {
        private readonly guachincheContext _context;

        public platosController(guachincheContext context)
        {
            _context = context;
        }

        // GET: platos
        public async Task<IActionResult> Index(string sortOrder, string searchString, string plateType, List<SelectListItem> tipoPlato)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["TypeSortParm"] = sortOrder == "Tipo" ? "Tipo_desc" : "Tipo";
            ViewData["CurrentFilter"] = searchString;


            var platos = (IQueryable<plato>)_context.platos.Include(p => p.tipo);

            if(tipoPlato.Count() == 0)
            {
                if (plateType == null || plateType == "Todos")
                {
                    tipoPlato.Add(new SelectListItem { Text = "Todos", Value = "Todos", Selected = true });
                }
                else
                {
                    tipoPlato.Add(new SelectListItem { Text = "Todos", Value = "Todos" });
                }

                foreach (string tipo in platos.Select(o => o.tipo.nombre).Distinct().ToList())
                {
                    if (plateType == tipo)
                    {
                        tipoPlato.Add(new SelectListItem { Text = tipo, Value = tipo, Selected = true });
                    }
                    else
                    {
                        tipoPlato.Add(new SelectListItem { Text = tipo, Value = tipo });
                    }
                }
            }
            
            ViewBag.plateType = tipoPlato;

            if (!String.IsNullOrEmpty(searchString))
            {
                platos = platos.Where(s => s.Nombre.Contains(searchString));
                
            }

            if (!String.IsNullOrEmpty(plateType) && !plateType.Equals("Todos"))
            {
                platos = platos.Where(s => s.tipo.nombre.Equals(plateType));
            }

            switch (sortOrder)
            {
                case "Nombre":
                    platos = platos.OrderBy(s => s.Nombre);
                    break;
                case "Tipo":
                    platos = platos.OrderBy(s => s.tipo.nombre);
                    break;
                case "Tipo_desc":
                    platos = platos.OrderByDescending(s => s.tipo.nombre);
                    break;
                default:
                    platos = platos.OrderByDescending(s => s.Nombre);
                    break;
            }

            return View(await platos.AsNoTracking().ToListAsync());
        }

        // GET: platos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.platos == null)
            {
                return NotFound();
            }

            var plato = await _context.platos
                .Include(p => p.tipo)
                .FirstOrDefaultAsync(m => m.PlatoId == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: platos/Create
        public IActionResult Create()
        {
            ViewData["tipoId"] = new SelectList(_context.tipos, "id", "nombre");
            return View();
        }

        // POST: platos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatoId,Nombre,Descripcion,tipoId")] plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["tipoId"] = new SelectList(_context.tipos, "id", "nombre", plato.tipoId);
            return View(plato);
        }

        // GET: platos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.platos == null)
            {
                return NotFound();
            }

            var plato = await _context.platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            ViewData["tipoId"] = new SelectList(_context.tipos, "id", "nombre", plato.tipoId);
            return View(plato);
        }

        // POST: platos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlatoId,Nombre,Descripcion,tipoId")] plato plato)
        {
            if (id != plato.PlatoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!platoExists(plato.PlatoId))
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
            ViewData["tipoId"] = new SelectList(_context.tipos, "id", "nombre", plato.tipoId);
            return View(plato);
        }

        // GET: platos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.platos == null)
            {
                return NotFound();
            }

            var plato = await _context.platos
                .Include(p => p.tipo)
                .FirstOrDefaultAsync(m => m.PlatoId == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.platos == null)
            {
                return Problem("Entity set 'guachincheContext.platos'  is null.");
            }
            var plato = await _context.platos.FindAsync(id);
            if (plato != null)
            {
                _context.platos.Remove(plato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool platoExists(int id)
        {
          return (_context.platos?.Any(e => e.PlatoId == id)).GetValueOrDefault();
        }
    }
}
