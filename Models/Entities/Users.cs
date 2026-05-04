using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

// Bảng tài khoản đăng nhập — liên kết 1-1 với Employee
[Table("users")]
public class Users
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Mỗi nhân viên chỉ có 1 tài khoản — null nếu là tài khoản hệ thống
    [Column("employee_id")]
    public Guid? EmployeeId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    // Mật khẩu được mã hóa bằng BCrypt — không bao giờ lưu mật khẩu gốc
    [Required]
    [MaxLength(255)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    // HR_MANAGER: toàn quyền | EMPLOYEE: chỉ xem dữ liệu bản thân
    [Required]
    [MaxLength(20)]
    [Column("role")]
    public string Role { get; set; } = UserRole.Employee.ToValue();

    [MaxLength(10)]
    [Column("status")]
    public string Status { get; set; } = UserStatus.Active.ToValue();

    // Khóa tài khoản sau 5 lần nhập sai — bảo mật chống brute-force
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
