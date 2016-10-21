using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a bio")]
        public string Bio { get; set; }
    }
}