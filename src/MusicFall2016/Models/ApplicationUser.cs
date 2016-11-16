using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class ApplicationUserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; internal set; }
    }
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateJoined { get; set; }
    }
}
