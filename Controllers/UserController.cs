﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiGuachincheWeb.Data;
using MiGuachincheWeb.Models;
using System.Data;
using System.Linq;

namespace MiGuachincheWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly guachincheContext _guachincheContext;
        private readonly UserManager<CustomUser> _userManager;

        public UserController(guachincheContext guachincheContext, UserManager<CustomUser> userManager)
        {
            _guachincheContext = guachincheContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _guachincheContext.Users.ToListAsync();
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            users.RemoveAll(u => u.Id == currentUser.Result.Id);
            return View(users);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Details(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }

            var user = await _guachincheContext.custom_users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }

            return View(user);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Edit(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }
            return View(user);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> RestList(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if(!user.Id.Equals(currentUser.Result.Id)) 
            {
                return BadRequest();
            }

            List<restaurante> restList = user.restaurantes.ToList();

            return View(restList);
        }

        [Authorize(Roles = "Default")]
        public async Task<IActionResult> PlatoList(String? id)
        {
            if (id == null || _guachincheContext.Users == null)
            {
                return NotFound();
            }
            var user = await _guachincheContext.custom_users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            if (!user.Id.Equals(currentUser.Result.Id))
            {
                return BadRequest();
            }

            List<plato_restaurante> platos = user.platos.ToList();

            return View(platos);
        }

        public async Task<IActionResult> AddRestaurante(int? id)
        {
            if (id == null || _guachincheContext.restaurantes == null)
            {
                return NotFound();
            }

            var restaurante =  await _guachincheContext.restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(HttpContext.User);
            var user =  await _guachincheContext.custom_users.FindAsync(currentUser.Result.Id);
            if (user == null)
            {
                return NotFound();
            }

            user_restaurante rest = new user_restaurante();
            
            rest.restaurante_Id = restaurante.RestauranteId;
            rest.usuario_Id = user.Id;
            rest.restaurante = restaurante;
            rest.customUser = user;
            

            _guachincheContext.Add(rest);
            await _guachincheContext.SaveChangesAsync();
            return RedirectToAction("RestList");

        }
    }
}
