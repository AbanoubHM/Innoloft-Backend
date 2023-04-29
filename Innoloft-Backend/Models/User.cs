using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Innoloft_Backend.Models {
    public class User {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }

        public Address Address { get; set; }
        
        public string Phone { get; set; }
        
        public string Website { get; set; }
        
        public Company Company { get; set; }
        
        public virtual ICollection<Event>? Events { get; set; }

        public virtual ICollection<Event>? ParticipatingEvents { get; set; }

        public virtual ICollection<Event>? InvitedEvents { get; set; }



    }

    [Owned]
    public class Company {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
    }

    
}