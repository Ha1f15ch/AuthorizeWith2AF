using AutoMapper;
using BusinessEngine.Commands;
using DatabaseEngine.DbModels;
using DTO.RoleModels;

namespace BusinessEngine.Mappings.Mapping;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<CreateRoleCommand, CreateRoleDtoModel>();
        CreateMap<CreateRoleDtoModel, Role>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? "Описание не задано"));
        
        CreateMap<Role, ResponseRoleDtoModel>();
    }
}