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
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(Artist artist)
        {
            return View();
        }
        public IActionResult Read()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = "???";
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
