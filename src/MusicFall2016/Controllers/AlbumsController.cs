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
        public IActionResult Index()
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
            Album a = new Album
            {
                AlbumID = 1,
                Title = "Hey",
                Price = (System.Decimal) 3.33,
                ArtistID = 3,
                Artist = new Artist
                {
                    ArtistID = 351,
                    Name = "john",
                    Bio = "heyo",
                },
                GenreID = 6,
                Genre = new Genre
                {
                    GenreID = 1,
                    Name = "MY GENRE",
                },
            };
            /*if (ModelState.IsValid)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }*/
            _context.Albums.Add(a);
            _context.SaveChanges();
            return View();
        }
        public IActionResult Read()
        {
            return View();
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albums = "???";
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
    }
}
