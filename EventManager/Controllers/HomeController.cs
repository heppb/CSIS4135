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
        public IActionResult EventSub()
        {
            string userID = _userManager.GetUserId(User);
            foreach (var testUserEvent in _context.UserEvents)
            {
                if (testUserEvent.UserID == userID)
                {
                    if (testUserEvent.Events == null)
                    {
                        testUserEvent.Events = new List<Events>();
                    }
                    return View(testUserEvent);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [Authorize]
        public IActionResult EventSub(int? id)
        {
            Events eventName = _context.Events.SingleOrDefault(a => a.EventsID == id);
            if (eventName == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            string userID = _userManager.GetUserId(User);
            if (userID == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            foreach (var testUserEvent in _context.UserEvents)
            {
                if (testUserEvent.UserID == userID)
                {
                    if (testUserEvent.Events == null)
                    {
                        testUserEvent.Events = new List<Events>();
                    }
                    testUserEvent.Events.Add(eventName);
                    /*_context.UserEvents.Update(testUserEvent);
                    _context.SaveChanges();*/
                    return View(testUserEvent);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        public IActionResult Follow()
        {
            string userID = _userManager.GetUserId(User);
            foreach (var testFollowArtist in _context.FollowedArtists)
            {
                if (testFollowArtist.UserID == userID)
                {
                    if (testFollowArtist.Artists == null)
                    {
                        testFollowArtist.Artists = new List<ApplicationUser>();
                    }
                    return View(testFollowArtist);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [Authorize]
        public IActionResult Follow(int? id)
        {
            ApplicationUser artist = new ApplicationUser();
            Events events = _context.Events.SingleOrDefault(a => a.EventsID == id);
            string artistName = events.ArtistName;
            foreach (var findArtist in _context.Users)
            {
                if (findArtist.UserName == artistName)
                {
                    artist = findArtist;
                }
            }
            string userID = _userManager.GetUserId(User);
            foreach (var testFollowArtist in _context.FollowedArtists)
            {
                if (testFollowArtist.UserID == userID)
                {
                    if (testFollowArtist.Artists == null)
                    {
                        testFollowArtist.Artists = new List<ApplicationUser>();
                    }
                    testFollowArtist.Artists.Add(artist);
                    /*_context.FollowedArtists.Update(testFollowArtist);
                    _context.SaveChanges();*/
                    return View(testFollowArtist);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        public IActionResult Unfollow(string artistName)
        {
            {
                ApplicationUser artist = new ApplicationUser();
                foreach (var findArtist in _context.Users)
                {
                    if (findArtist.UserName == artistName)
                    {
                        artist = findArtist;
                    }
                }
                string userID = _userManager.GetUserId(User);
                foreach (var testFollowArtist in _context.FollowedArtists)
                {
                    if (testFollowArtist.UserID == userID)
                    {
                        /*_context.FollowedArtists.Remove(testFollowArtist);
                        _context.SaveChanges();*/
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
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
        public IActionResult Index(string searchString, string Sorting)
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
            if(Sorting != null)
            {
                var name = _userManager.GetUserName(User);
                var events = from m in _context.Events
                             select m;

                if (!String.IsNullOrEmpty(name))
                {
                    events = events.Where(s => s.ArtistName.Contains(name));
                }
                if(events != null)
                {
                    return View(events);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
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
