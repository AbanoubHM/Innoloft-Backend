using AutoMapper;
using Innoloft_Backend.DTO;
using Innoloft_Backend.Models;

namespace Innoloft_Backend.Config {
    public class AutoMapperConfig: Profile {
        public AutoMapperConfig() {
            CreateMap<Event,EventDto>().ReverseMap();
            CreateMap<Event,EventPostDto>().ReverseMap();
            CreateMap<Event,EventPutDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User,UserInListDto>().ReverseMap();
        }

    }
}
