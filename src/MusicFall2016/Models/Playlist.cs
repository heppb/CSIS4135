using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }

        //public string PlaylistName { get; set; }

        public ApplicationUser User { get; set; }

        //public int UserID { get; set; }

        public List<PlaylistConnect> PlaylistList { get; set; }
    }
}
