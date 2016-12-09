using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class Events
    {
        public int EventsID { get; set; }
        [Required(ErrorMessage = "Please enter an event")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string ArtistName { get; set; }
        [Required(ErrorMessage = "Please enter a date")]
        public DateTime EventDate { get; set; }
        [Required(ErrorMessage = "Please enter a location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter a genre")]
        public string Genre { get; set; }
    }
}
