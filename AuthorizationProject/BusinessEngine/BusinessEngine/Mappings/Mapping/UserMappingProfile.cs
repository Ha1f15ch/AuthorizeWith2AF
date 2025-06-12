using AutoMapper;
using BusinessEngine.Commands.UserCommand;
using DatabaseEngine.DbModels;
using DTO.UserModels;

namespace BusinessEngine.Mappings.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRegistrationCommand, UserRegistrationDtoModel>();
        CreateMap<UserRegistrationDtoModel, User>();
    }
}