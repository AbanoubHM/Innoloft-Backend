using Innoloft_Backend.Models;

namespace Innoloft_Backend.DTO {
    public class EventPostDto {

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public Address Address { get; set; }

        public bool IsOnline { get; set; } 
    }
}
