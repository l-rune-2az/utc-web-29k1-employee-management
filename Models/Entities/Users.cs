using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

[Table("users")]
public class Users
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    
    [Column("employee_id")]
    public Guid? EmployeeId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    
    [Required]
    [MaxLength(255)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    
    [Required]
    [MaxLength(20)]
    [Column("role")]
    public string Role { get; set; } = UserRole.Employee.ToValue();

    [MaxLength(10)]
    [Column("status")]
    public string Status { get; set; } = UserStatus.Active.ToValue();

    
    [Column("failed_attempts")]
    public int FailedAttempts { get; set; } = 0;

    [Column("locked_until")]
    public DateTime? LockedUntil { get; set; }

    [Column("last_login")]
    public DateTime? LastLogin { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
}
