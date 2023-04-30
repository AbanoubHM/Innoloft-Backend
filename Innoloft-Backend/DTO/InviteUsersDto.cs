namespace Innoloft_Backend.DTO {
    public class InviteUsersDto {
        public int EventId { get; set; }
        public ICollection<int> UserIds { get; set; }
    }
}
