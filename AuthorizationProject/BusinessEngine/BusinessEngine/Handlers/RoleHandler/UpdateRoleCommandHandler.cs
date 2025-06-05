using AutoMapper;
using DatabaseEngine.DbModels;
using MediatR;
using BusinessEngine.Commands.RoleCommand;
using DatabaseEngine.RepositoryInterfaces;

namespace BusinessEngine.Handlers.RoleHandler;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role?>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Role?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request.roleForUpdateDtoModel);
        
        var getResultUpdate = await _roleRepository.UpdateAsync(request.RoleCode, role);
        return getResultUpdate;
    }
}