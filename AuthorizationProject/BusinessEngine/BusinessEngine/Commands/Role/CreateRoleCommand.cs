using DatabaseEngine.DbModels;
using MediatR;

namespace BusinessEngine.Commands;

public class CreateRoleCommand : IRequest<Role?>
{
    public string RoleCode { get; set; }
    public string Description { get; set; }
}