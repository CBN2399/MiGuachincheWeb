using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Controllers
{
    public class ReservasController : Controller
    {
        private readonly guachincheContext _context;
        private readonly UserManager<CustomUser> _userManager;

        public ReservasController(guachincheContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var guachincheContext = _context.reservas.Include(r => r.CustomUser).Include(r => r.estado).Include(r => r.restaurante);
            return View(await guachincheContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.reservas
                .Include(r => r.CustomUser)
                .Include(r => r.estado)
                .Include(r => r.restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public async Task<IActionResult> Create(int? id)
        {
            if ((id == null || _context.restaurantes == null))
            {
                return NotFound();

            }
            var restaurante = await _context.restaurantes.FindAsync(id);
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if ((restaurante == null) || (currentUser.Result == null))
            {
                return NotFound();
            }
            if (currentUser.Result.Nombre == null)
            {
                return BadRequest();
            }
            ReservaDTO reserva = new ReservaDTO(currentUser.Result.Id, restaurante.RestauranteId, currentUser.Result.Nombre, restaurante.Nombre);

            return View(reserva);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("numeroComensales,fechaReserva,nombreUsuario,nombreRestaurante,userId,restId")] ReservaDTO reserva)
        {
            if (ModelState.IsValid)
            {
                if ((reserva.userId == null) || (_context.reservas == null))
                {
                    return NotFound();
                }

                var user = await _context.custom_users.FirstOrDefaultAsync(e => e.Id == reserva.userId);
                var restaurante =  await _context.restaurantes.FirstOrDefaultAsync(e => e.RestauranteId == reserva.restId);
                var estado = await _context.estadoReservas.FirstOrDefaultAsync(e => e.Name == "Pendiente");
                if ((user == null) || (restaurante == null) || (estado == null))
                {
                    return NotFound();
                }

                Reserva reservation = new Reserva();
                reservation.numeroComensales = reserva.numeroComensales;
                reservation.FechaReserva = reserva.fechaReserva;
                reservation.restauranteId = reserva.restId;
                reservation.restaurante = restaurante;
                reservation.customerUserId = reserva.userId;
                reservation.CustomUser = user;
                reservation.estado = estado;
                reservation.estadoReservaId = estado.Id;

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "restaurantes");
            }

            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaReserva,numeroComensales")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var estado = await _context.estadoReservas.FirstOrDefaultAsync(e => e.Name == "Pendiente");
                    if (estado == null)
                    {
                        return BadRequest();
                    }
                    reserva.estado = estado;
                    reserva.estadoReservaId = estado.Id;
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.reservas
                .Include(r => r.CustomUser)
                .Include(r => r.estado)
                .Include(r => r.restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservas == null)
            {
                return Problem("Entity set 'guachincheContext.reservas'  is null.");
            }
            var reserva = await _context.reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return (_context.reservas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
