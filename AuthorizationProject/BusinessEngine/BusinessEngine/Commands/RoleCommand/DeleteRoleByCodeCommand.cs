using MediatR;

namespace BusinessEngine.Commands.RoleCommand;

public class DeleteRoleByCodeCommand : IRequest<bool>
{
    public int RoleId { get; set; }
}