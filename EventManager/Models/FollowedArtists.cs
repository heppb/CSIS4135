using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class FollowedArtists
    {
        public int FollowedArtistsID { get; set; }
        public string UserOfList { get; set; }
        public List<ApplicationUser> Artists { get; set; }
        //public List<Events> Events { get; set; }
    }
}
