﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistsController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Details()
        {
            var artists = _context.Artists.ToList();
            return View(artists);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Artists.Add(artist);
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
            var artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
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
            var artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }
        [HttpPost]
        public IActionResult Update(Artist artist)
        {
            _context.Artists.Update(artist);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}
