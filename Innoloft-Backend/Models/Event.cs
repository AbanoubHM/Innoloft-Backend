﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Innoloft_Backend.Models {
    public class Event {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }=string.Empty;

        public virtual User User { get; set; }
        
        public int UserId { get; set; }
        
        public DateTimeOffset CreatedDate { get; set;} = DateTimeOffset.Now;
        
        public DateTimeOffset StartTime { get; set; }
        
        public DateTimeOffset EndTime { get; set; }
        
        public Address Address { get; set; }
        
        public bool IsOnline { get; set; } = false;
        
        public virtual ICollection<User>? ParticipatingUsers { get; set; }

        public virtual ICollection<User>? InvitedUsers { get; set; }

    }
}
