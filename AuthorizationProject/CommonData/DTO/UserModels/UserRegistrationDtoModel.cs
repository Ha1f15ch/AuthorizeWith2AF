namespace DTO.UserModels;

public class UserRegistrationDtoModel
{
    public required string UserName { get; set; }
    public required string UserEmail { get; set; }
    public required string Password { get; set; }
}