using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManager.Data;
using Microsoft.AspNetCore.Authorization;
using EventManager.Models;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "ARTIST")]
        public IActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ARTIST")]
        public IActionResult AddEvent(Events newEvent)
        {
            if (ModelState.IsValid)
            {

                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }
        [Authorize(Roles = "ARTIST")]
        public IActionResult EditEvent(int? id)
        {
            Events events = _context.Events.SingleOrDefault(a => a.EventsID == id);
            if (_userManager.GetUserName(User) == events.ArtistName)
            {
                return View(events);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [Authorize(Roles = "ARTIST")]
        public IActionResult EditEvent(Events events)
        {
            _context.Events.Update(events);
            _context.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize(Roles = "ARTIST")]
        public IActionResult RemoveEvent(int? id)
        {
            Events events = _context.Events.SingleOrDefault(a => a.EventsID == id);
            if (_userManager.GetUserName(User) == events.ArtistName)
            {
                return View(events);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [Authorize(Roles = "ARTIST")]
        public IActionResult RemoveEvent(Events events)
        {
                _context.Events.Remove(events);
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        public IActionResult EventSub(int? id)
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult EventSub(Events EventName)
        {
            return View();
        }
        [Authorize]
        public IActionResult Follow(int? id)
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Follow(Events EventName)
        {
            return View();
        }
        public IActionResult EventDetails(int? id)
        {
            Events events = _context.Events.SingleOrDefault(a => a.EventsID == id);
            if (ModelState.IsValid)
            {
                return View(events);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        /*public IActionResult Index()
        {
            var events = _context.Events;
            ViewBag.EventList = new SelectList(_context.Events, "EventName", "ArtistName");
            return View(events);
        }*/
        public IActionResult Index(string searchString)
        {
            if (searchString != null)
            {
                var events = from m in _context.Events
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    events = events.Where(s => s.EventName.Contains(searchString) || s.ArtistName.Contains(searchString) || s.Genre.Contains(searchString) || s.Location.Contains(searchString));
                }

                return View(events);
            }
            else
            {
                var events = _context.Events;
                ViewBag.EventList = new SelectList(_context.Events, "EventName", "ArtistName");
                return View(events);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
