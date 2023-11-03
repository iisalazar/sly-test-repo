using AutoMapper;
using Entities.DTOs.Authentication;
using Entities.Models;

namespace SlydynBackend;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<RegisterUserDto, User>();
    CreateMap<User, UserDto>()
      .ForMember(destination => destination.Id, opt => opt.MapFrom(source => source.Id));
  }
}