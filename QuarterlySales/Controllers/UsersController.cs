using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuarterlySales.Models;
using QuarterlySales.Models.DomainModels;

namespace QuarterlySales.Controllers
{
    public class UsersController : Controller
    {
        private readonly SalesContext _context;

        public UsersController(SalesContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("username,password,isAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                user.isAdmin = false;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IFormCollection data)
        {
            String username = data["username"];
            String password = data["password"];

            var loggedUser = await _context.Users
                .FirstOrDefaultAsync(m => m.username == username && m.password == password);
            if (loggedUser != null)
            {
                HttpContext.Session.SetObject("user", loggedUser);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.message = "Invalid login credentials";
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddToAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            user.isAdmin = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(nameof(Index), await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            user.isAdmin = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(nameof(Index), await _context.Users.ToListAsync());
        }



        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.username == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            if (user == null)
            {
                return NotFound();
            }

            return View(nameof(Index), await _context.Users.ToListAsync());
        }


        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.username == id);
        }
    }
}
