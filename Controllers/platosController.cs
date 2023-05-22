using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System;

namespace MiGuachincheWeb.Controllers
{
    [Authorize(Roles = "Admin,Default")]
    public class platosController : Controller
    {
        private readonly guachincheContext _context;
        private readonly IWebHostEnvironment _webEnvironment;

        public platosController(guachincheContext context, IWebHostEnvironment environment)
        {
            _webEnvironment = environment;
            _context = context;
        }

        // GET: platos
        public async Task<IActionResult> Index(string sortOrder, string searchString, string plateType, List<SelectListItem> tipoPlato)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["TypeSortParm"] = sortOrder == "Tipo" ? "Tipo_desc" : "Tipo";
            ViewData["CurrentFilter"] = searchString;


            var platos = (IQueryable<Plato>)_context.platos.Include(p => p.tipo);

            if (tipoPlato.Count() == 0)
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
        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.platos == null)
            {
                return NotFound();
            }

            var plato = await _context.platos
                .Include(p => p.tipo)
                .Include(r => r.restaurantes)
                .ThenInclude(e => e.zona)
                .Include(r => r.restaurantes)
                .ThenInclude(e => e.Id_tipoNavigation)
                .FirstOrDefaultAsync(m => m.PlatoId == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: platos/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("PlatoId,Nombre,Descripcion,tipoId")] Plato plato, [FromForm] IFormFile Image)
        {
            string filePath = Path.Combine(_webEnvironment.WebRootPath, "img");
            string restPath = Path.Combine(filePath, "platos");

            if (ModelState.IsValid && Image != null)
            {
                if (Directory.Exists(restPath))
                {
                    using (Stream fileStream = new FileStream(Path.Combine(restPath, Image.FileName), FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                }
                plato.ImagenURL = Image.FileName;
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["error"] = "No se ha subido la imagen del plato";
            }
            ViewData["tipoId"] = new SelectList(_context.tipos, "id", "nombre", plato.tipoId);
            return View(plato);
        }

        // GET: platos/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PlatoId,Nombre,Descripcion,tipoId")] Plato plato, [FromForm] IFormFile Image)
        {
            if (id != plato.PlatoId)
            {
                return NotFound();
            }

            string filePath = Path.Combine(_webEnvironment.WebRootPath, "img");
            string restPath = Path.Combine(filePath, "restaurantes");

            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                var plate = await _context.platos.FirstOrDefaultAsync(e => e.PlatoId == plato.PlatoId);
                if (plate == null)
                {
                    return NotFound();
                }
                try
                {
                    if (Directory.Exists(restPath) && Image != null)
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(restPath, Image.FileName), FileMode.Create))
                        {
                            await Image.CopyToAsync(fileStream);
                        }
                        plate.ImagenURL = Image.FileName;
                    }
                    plate.Descripcion = plato.Descripcion;
                    plate.Nombre = plato.Nombre;
                    plate.tipoId = plato.tipoId;
                    plate.tipo = plato.tipo;
                    plate.restaurantes = plato.restaurantes;

                    _context.Update(plate);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
