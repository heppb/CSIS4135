using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter a price from 0.01 - 100.00")]

        // Foreign key
        public int ArtistID { get; set; }
        // Navigation property
        public Artist Artist { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
