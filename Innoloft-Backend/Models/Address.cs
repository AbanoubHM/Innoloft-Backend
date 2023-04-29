using Microsoft.EntityFrameworkCore;

namespace Innoloft_Backend.Models {
    [Owned]
    public class Address {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string? Zipcode { get; set; }
        public GeoLocation? Geo { get; set; }
    }
    [Owned]
    public class GeoLocation {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
