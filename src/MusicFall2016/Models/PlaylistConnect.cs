using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class PlaylistConnect
    {
        public int PlaylistID { get; set; }

        public Playlist Playlist { get; set; }

        public int AlbumID { get; set; }

        public Album Album { get; set; }
    }
}
