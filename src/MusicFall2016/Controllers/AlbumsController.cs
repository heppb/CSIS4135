using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumsController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Details()
        {
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre).ToList();
            return View(albums);
        }
        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }
        public IActionResult Read(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);
            if (albums == null)
            {
                return NotFound();
            }
            return View(albums);
        }
        /*public IActionResult Update()
        {
            return View();
        }*/
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);
            if (albums == null)
            {
                return NotFound();
            }

            return View(albums);
        }
        [HttpPost]
        public IActionResult Update(Album album)
        {
            _context.Albums.Update(album);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);
            if (albums == null)
            {
                return NotFound();
            }

            return View(albums);
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return RedirectToAction("Details");
        }
        /*public ActionResult Test()
        {
            ViewBag.MyMessageToUsers = "Hello from me.";
            ViewBag.AnswerText = "Your answer goes here.";

            return View();
        }*/
    }
}
