namespace DTO.RoleModels;

public class ResponseRoleDtoModel
{
    public string RoleCode { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}