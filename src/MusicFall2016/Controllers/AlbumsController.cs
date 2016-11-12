using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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
        public IActionResult Details(string searchString)
        {
            if(searchString != null)
            {
                var album = from m in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                            select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    album = album.Where(s => s.Title.Contains(searchString) || s.Artist.Name.Contains(searchString) || s.Genre.Name.Contains(searchString));
                }

                return View(album.ToList());
            }
            else {
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre).ToList();
            return View(albums);
            }
        }
        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Album album , string artistFill , string genreFill)
        {
            if (ModelState.IsValid)
            {
                if (artistFill != null)
                {
                    foreach (var artistName in _context.Artists.ToList())
                    {
                        string name = artistName.Name;
                        if (name == artistFill)
                        {
                            artistFill = "";
                        }
                    }
                    if(artistFill != "")
                    {
                        Artist artist = new Artist();
                        artist.Name = artistFill;
                        artist.Bio = "";
                        _context.Artists.Add(artist);
                        _context.SaveChanges();
                        album.Artist = _context.Artists.Last();
                    }
                }
                if (genreFill != null)
                {
                    foreach (var genreName in _context.Genres.ToList())
                    {
                        string name = genreName.Name;
                        if (name == genreFill)
                        {
                            genreFill = "";
                        }
                    }
                    if(genreFill != "")
                    {
                        Genre genre = new Genre();
                        genre.Name = genreFill;
                        _context.Genres.Add(genre);
                        _context.SaveChanges();
                        album.Genre = _context.Genres.Last();
                    }
                }
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
            var recAlbumList = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
            var recAlbums = "";
            var count = 0;
            foreach (var album in recAlbumList)
            {
                if (count < 5 && album.AlbumID != albums.AlbumID && album.ArtistID == albums.ArtistID ||  album.GenreID == albums.GenreID)
                {
                    count = count + 1;
                    recAlbums = recAlbums + album.Title + " ";
                }
            }
            ViewData["recAlbums"] = recAlbums;

            return View(albums);
        }
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
        public IActionResult Like(int? id)
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
            albums.Like = albums.Like + 1;
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
        [HttpPost]
        public string Details(string searchString, bool notUsed)
        {
            return "From [HttpPost]Details: filter on " + searchString;
        }

        //Sorting Method
        //https://docs.asp.net/en/latest/data/ef-mvc/sort-filter-page.html?highlight=sorting

        public async Task<IActionResult> Sort(string sortOrder)
        { 
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "";
            ViewData["ArtistSortParm"] = String.IsNullOrEmpty(sortOrder) ? "artist_asc" : "artist_desc" ;
            ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_asc" : "genre_desc";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_desc";
            ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_asc" : "" ;

            var albums = from s in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                           select s;
            switch (sortOrder)
            {
                case "title_asc":
                    albums = albums.OrderBy(s => s.Title);
                    break;
                case "artist_asc":
                    albums = albums.OrderBy(s => s.Artist.Name);
                    break;
                case "genre_asc":
                    albums = albums.OrderBy(s => s.Genre.Name);
                    break;
                case "price_asc":
                    albums = albums.OrderBy(s => s.Price);
                    break;
                case "like_asc":
                    albums = albums.OrderBy(s => s.Like);
                    break;
                case "artist_desc":
                    albums = albums.OrderByDescending(s => s.Artist.Name);
                    break;
                case "genre_desc":
                    albums = albums.OrderByDescending(s => s.Genre.Name);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(s => s.Price);
                    break;
                default:
                    albums = albums.OrderBy(s => s.Title);
                    break;
            }
            if (sortOrder == "artist_desc" || sortOrder == "genre_desc" || sortOrder == "price_desc")
            {
                sortOrder = "";
                ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "";
                ViewData["ArtistSortParm"] = String.IsNullOrEmpty(sortOrder) ? "artist_asc" : "artist_desc";
                ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_asc" : "genre_desc";
                ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_desc";
                ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_asc" : "";
            }
            return View(await albums.AsNoTracking().ToListAsync());
        }

        //Searching method
        // https://docs.asp.net/en/latest/tutorials/first-mvc-app/search.html
    }
}
