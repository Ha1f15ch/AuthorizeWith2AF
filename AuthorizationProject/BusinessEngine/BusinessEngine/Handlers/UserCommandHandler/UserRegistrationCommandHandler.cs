using AutoMapper;
using BusinessEngine.Commands.UserCommand;
using BusinessEngine.Services.Interfaces;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using DTO.UserModels;
using MediatR;

namespace BusinessEngine.Handlers.UserCommandHandler;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, bool>
{
    private readonly IPasswordService _passwordService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserRegistrationCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
    {
        _passwordService = passwordService;
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
            
            user.UserPassword = _passwordService.HashPassword(user.UserPassword);//Присваиваем захэшированный пароль
            
            var savedUser = await _userRepository.CreateNewUser(user);

            if (savedUser != null) // Если пользователь создался корректно - отправляем ему код на почту
            {
                
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
