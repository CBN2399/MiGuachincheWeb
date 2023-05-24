using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Migrations;
using MiGuachincheWeb.Models;
using System.Data;

namespace MiGuachincheWeb.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin,Default")]
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

        [Authorize(Roles = "Manager,Default")]
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

            var platorest = from t1 in _context.plato_restaurantes
                            join t2 in _context.platos on t1.plato_Id equals t2.PlatoId
                            where t1.restaurante_Id == id
                            select new PlatoDTO
                            {
                                id = t1.id,
                                nombre = t2.Nombre,
                                tipo = t2.tipo.nombre,
                                valoracion = t1.valoracion
                            };


            List<PlatoDTO> platos = new List<PlatoDTO>();
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create(String? id)
        {
            ViewData["Id_tipo"] = new SelectList(_context.tipoRestaurantes, "id", "nombre");
            ViewData["zonaId"] = new SelectList(_context.zonas, "Zona_id", "nombre");
            ViewData["managerId"] = id;
            return View();
        }

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

        [HttpPost]
        public async Task<IActionResult> Valorate(int restauranteId, int rating)
        {
            var restaurante = await _context.restaurantes.Include(e => e.valoraciones).FirstOrDefaultAsync(i => i.RestauranteId == restauranteId);
            if (restaurante == null) 
            {
                return NotFound();
            }
            if (rating != 0) 
            {
                if(restaurante.valoraciones == null)
                {
                    restaurante.valoraciones = new List<ValoracionRestaurante>();
                }
                restaurante.valoraciones.Add(new ValoracionRestaurante { valoracion = rating });
                int total = 0;
                foreach(ValoracionRestaurante val in restaurante.valoraciones)
                {
                    total += val.valoracion;
                }
                restaurante.valoracion = total/restaurante.valoraciones.Count;
                _context.Update(restaurante);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Reserva", "user");
        }
        
    }
}
