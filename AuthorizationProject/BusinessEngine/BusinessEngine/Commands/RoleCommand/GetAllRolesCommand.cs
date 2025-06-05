using MediatR;
using DatabaseEngine.DbModels;

namespace BusinessEngine.Commands.RoleCommand;

public class GetAllRolesCommand : IRequest<List<Role>>
{
    
}