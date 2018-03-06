using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}