using AutoMapper;
using MediatR;
using BusinessEngine.Commands.RoleCommand;
using DatabaseEngine.RepositoryInterfaces;

namespace BusinessEngine.Handlers.RoleHandler;

public class DeleteRoleByCodeCommandHandler : IRequestHandler<DeleteRoleByCodeCommand, bool>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public DeleteRoleByCodeCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteRoleByCodeCommand request, CancellationToken cancellationToken)
    {
        var resultForDelete = await _roleRepository.DeleteAsync(request.RoleCode);
        return resultForDelete;
    }
}