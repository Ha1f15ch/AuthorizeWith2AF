using System.ComponentModel.DataAnnotations;

namespace DTO.UserModels;

public class UserRegistrationDtoModel
{
    [MaxLength(25, ErrorMessage = "Длина должна быть не меньше 6 и не больше 25 символов"), MinLength(6, ErrorMessage = "Длина должна быть не меньше 6 и не больше 25 символов")]
    public required string UserName { get; set; }
    [EmailAddress]
    public required string UserEmail { get; set; }
    [MaxLength(18, ErrorMessage = "Длина должна быть не меньше 6 и не больше 18 символов"), MinLength(6, ErrorMessage = "Длина должна быть не меньше 6 и не больше 18 символов")]
    public required string Password { get; set; }
}