using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System.Data;

namespace MiGuachincheWeb.Controllers
{

    public class restaurantesController : Controller
    {
        private readonly guachincheContext _context;
        private readonly IWebHostEnvironment _webEnvironment;
        private readonly UserManager<CustomUser> _userManager;

        public restaurantesController(guachincheContext context, IWebHostEnvironment environment, UserManager<CustomUser> userManager)
        {
            _context = context;
            _webEnvironment = environment;
            _userManager = userManager;
        }

        // GET: restaurantes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string restZone, List<SelectListItem> zonaRest)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewData["ValSortParm"] = sortOrder == "Val" ? "Val_asc" : "Val";
            ViewData["CurrentFilter"] = searchString;

            var restaurantes = (IQueryable<Restaurante>)_context.restaurantes.Include(r => r.Id_tipoNavigation).Include(r => r.zona);

            if (restaurantes.Count() != 0)
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
            if (platorest != null)
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
        public IActionResult Create(String? id)
        {
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre");
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre");
            ViewData["managerId"] = id;
            return View();
        }

        // POST: restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(String? id, [Bind("RestauranteId,Nombre,telefono,valoracion,Descripcion,Id_tipo,zonaId")] Restaurante restaurante, [FromForm] IFormFile Image)
        {
            if ((id == null) || (_context.custom_users == null))
            {
                return NotFound();
            }
            var manager = await _context.custom_users
                .Include(e => e.restaurantes).FirstOrDefaultAsync(i => i.Id == id);
            if (manager == null)
            {
                return NotFound();
            }
            string filePath = Path.Combine(_webEnvironment.WebRootPath, "img");
            string restPath = Path.Combine(filePath, "restaurantes");
            if (ModelState.IsValid && Image != null)
            {
                if (Directory.Exists(restPath))
                {
                    using (Stream fileStream = new FileStream(Path.Combine(restPath, Image.FileName), FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                }
                restaurante.Rest_Url = Image.FileName;
                _context.Add(restaurante);

                UserRestaurante userRest = new UserRestaurante();
                userRest.usuario_Id = id;
                userRest.restaurante_Id = restaurante.RestauranteId;
                userRest.restaurante = restaurante;
                userRest.customUser = manager;
                _context.Add(userRest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["error"] = "No se ha subido la imagen del restaurante";
            }
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre", restaurante.Id_tipo);
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre", restaurante.zonaId);

            return View(restaurante);
        }

        // GET: restaurantes/Edit/5
        [Authorize(Roles = "Manager")]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("RestauranteId,Nombre,telefono,valoracion,Descripcion,Id_tipo,zonaId")] Restaurante restaurante, [FromForm] IFormFile Image)
        {
            if (id != restaurante.RestauranteId)
            {
                return NotFound();
            }

            string filePath = Path.Combine(_webEnvironment.WebRootPath, "img");
            string restPath = Path.Combine(filePath, "restaurantes");



            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                var restaurant = await _context.restaurantes.FirstOrDefaultAsync(e => e.RestauranteId == restaurante.RestauranteId);
                if (restaurant == null)
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
                        restaurant.Rest_Url = Image.FileName;
                    }
                    restaurant.Descripcion = restaurante.Descripcion;
                    restaurant.Nombre = restaurante.Nombre;
                    restaurant.telefono = restaurante.telefono;
                    restaurant.Id_tipoNavigation = restaurante.Id_tipoNavigation;
                    restaurant.Id_tipo = restaurante.Id_tipo;

                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                var currentUser = _userManager.GetUserAsync(HttpContext.User);
                return RedirectToAction("List", "Manager", new { id = currentUser.Result.Id });
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
