using MediatR;

namespace BusinessEngine.Commands.UserCommand;

public class UserRegistrationCommand : IRequest<bool>
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
}