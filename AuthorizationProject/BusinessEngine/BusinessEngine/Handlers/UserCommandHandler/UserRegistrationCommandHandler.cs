using AutoMapper;
using BusinessEngine.Commands.UserCommand;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using DTO.UserModels;
using MediatR;

namespace BusinessEngine.Handlers.UserCommandHandler;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserRegistrationCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userDto = _mapper.Map<UserRegistrationDtoModel>(request);
            var user = _mapper.Map<User>(userDto);
            
            var savedUser = await _userRepository.CreateNewUser(user);

            if (savedUser != null) // Если пользователь создался корректно - генерируем токены
            {
                Console.WriteLine($"Пользователь создан. userId = {savedUser.Id}");
            }
            
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"При выполнении команды регистрации возникла ошибка - {e.Message}");
            throw;
        }
    }
}
