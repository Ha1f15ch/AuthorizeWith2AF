using System.ComponentModel.DataAnnotations;

namespace DTO.RoleModels;

public class CreateRoleDtoModel
{
    [Required(ErrorMessage = "Поле код роли должно быть обязательно заполнено"), MaxLength(10, ErrorMessage = "Число символов в коде роли не должно превышать 10"), ]
    public required string RoleCode { get; set; }

    [MaxLength(100, ErrorMessage = "Длина значения поля описания не может быть больше 100")]
    public string? Description { get; set; }
}