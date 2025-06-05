using MediatR;

namespace BusinessEngine.Commands.RoleCommand;

public class DeleteRoleByCodeCommand : IRequest<bool>
{
    public string RoleCode { get; set; }
}