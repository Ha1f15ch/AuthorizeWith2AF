using AutoMapper;
using BusinessEngine.Commands;
using BusinessEngine.Handlers.BaseHandler;
using DatabaseEngine.RepositoryInterfaces;
using DTO.RoleModels;

namespace BusinessEngine.Handleres.Role;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, ResponseRoleDtoModel>
{
    private readonly IRoleRepository<CreateRoleDtoModel, ResponseRoleDtoModel> _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IRoleRepository<CreateRoleDtoModel, ResponseRoleDtoModel> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseRoleDtoModel> HandleAsync(CreateRoleCommand command)
    {
        var dtoModel = _mapper.Map<CreateRoleDtoModel>(command);
        return await _roleRepository.AddAsync(dtoModel);
    }
}