using AutoMapper;
using BusinessEngine.Commands.RoleCommand;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using MediatR;

namespace BusinessEngine.Handlers.RoleHandler;

public class GetAllRolesCommandHandler : IRequestHandler<GetAllRolesCommand, List<Role>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetAllRolesCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<Role>> Handle(GetAllRolesCommand request, CancellationToken cancellationToken)
    {
        var getListRoles = await _roleRepository.GetAllAsync();
        return getListRoles;
    }
}