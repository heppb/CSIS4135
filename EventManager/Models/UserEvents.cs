using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class UserEvents
    {
        public int UserEventsID { get; set; }
        public List<Events> Events { get; set; }
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }
    }
}
