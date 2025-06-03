using AutoMapper;
using BusinessEngine.Commands;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using MediatR;

namespace BusinessEngine.Handleres.RoleHandler;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role?>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<Role?> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var dtoModel = _mapper.Map<Role>(command);
        return await _roleRepository.AddAsync(dtoModel);
    }
}