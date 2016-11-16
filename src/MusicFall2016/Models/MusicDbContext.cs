using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicFall2016.Models
{
    public class MusicDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlaylistConnect>()
                .HasKey(t => new { t.PlaylistID, t.AlbumID });

            modelBuilder.Entity<PlaylistConnect>()
                .HasOne(pt => pt.Playlist)
                .WithMany(p => p.PlaylistList)
                .HasForeignKey(pt => pt.PlaylistID);

            modelBuilder.Entity<PlaylistConnect>()
                .HasOne(pt => pt.Album)
                .WithMany(t => t.PlaylistList);
                //.HasForeignKey(pt => pt.AlbumID);
        }
    }
}
