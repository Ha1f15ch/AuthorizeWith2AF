using DatabaseEngine.DbModels;
using DTO.RoleModels;
using MediatR;

namespace BusinessEngine.Commands.RoleCommand;

public class UpdateRoleCommand : IRequest<Role?>
{
    public int RoleId { get; set; }
    public RoleForUpdateDtoModel roleForUpdateDtoModel { get; set; }
}