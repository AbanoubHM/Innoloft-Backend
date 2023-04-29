using Innoloft_Backend.Models;

namespace Innoloft_Backend.DTO {
    public class EventDto {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual UserDto User { get; set; }

        public int UserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public Address Address { get; set; }

        public bool IsOnline { get; set; } = false;

        public virtual ICollection<UserInListDto>? ParticipatingUsers { get; set; }

        public virtual ICollection<UserInListDto>? InvitedUsers { get; set; }
    }
}
