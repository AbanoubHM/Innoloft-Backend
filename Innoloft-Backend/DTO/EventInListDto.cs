using Innoloft_Backend.Models;

namespace Innoloft_Backend.DTO {
    public class EventInListDto {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public Address Address { get; set; }

        public bool IsOnline { get; set; } 
    }
}
