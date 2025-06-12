using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEngine.DbModels;

[Table("VerificationCode", Schema = "dbo")]
public class VerificationCode
{
    public VerificationCode()
    {
        
    }
    
    [Key]
    public int Id { get; set; }
    public required int UserId { get; set; }
    [MaxLength(8)]
    public required string Code { get; set; }
    [MaxLength(100)]
    public required string UserEmail { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public required bool IsUsed { get; set; }
    
    public User User { get; set; }
}