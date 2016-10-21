using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var albums =  _context.Albums.ToList();
            return View(albums);
        }
        public IActionResult Create()
        {
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
            return View();
        }
        public IActionResult Read(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albums = _context.Albums.SingleOrDefault(a => a.AlbumID == id);
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
            var albums = _context.Albums.SingleOrDefault(a => a.AlbumID == id);
            if (albums == null)
            {
                return NotFound();
            }

            return View(albums);
        }
        public IActionResult Delete()
        {
            return View();
        }
        /*public ActionResult Test()
        {
            ViewBag.MyMessageToUsers = "Hello from me.";
            ViewBag.AnswerText = "Your answer goes here.";

            return View();
        }*/
    }
}
