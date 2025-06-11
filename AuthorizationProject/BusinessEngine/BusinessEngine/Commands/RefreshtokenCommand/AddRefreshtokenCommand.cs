using DatabaseEngine.DbModels;
using DTO.RefreshtokenModels;
using MediatR;

namespace BusinessEngine.Commands.RefreshtokenCommand
{
    public class AddRefreshtokenCommand : IRequest<RefreshToken?>
    {
        public required CreateRefreshtokenDtoModel CreateRefreshtokenDtoModel { get; set; }
    }
}
