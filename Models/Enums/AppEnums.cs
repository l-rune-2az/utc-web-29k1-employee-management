namespace EmployeeManagement.Models.Enums;

// ============================================================
// ENUMS + EXTENSION METHODS
// Quy ước: PascalCase cho enum value, ToValue() trả về string DB
// ============================================================

public enum EmployeeStatus   { Active, Inactive }
public enum DepartmentStatus { Active, Inactive }
public enum PositionStatus   { Active, Inactive }
public enum ContractStatus   { Active, Expired, Terminated }
public enum AllowanceStatus  { Active, Inactive }
public enum DependentStatus  { Active, Inactive }
public enum UserStatus       { Active, Inactive, Locked }

public enum UserRole       { HrManager, Employee }
public enum Gender         { Male, Female, Other }
public enum PositionLevel  { Junior, Middle, Senior }
public enum ContractType   { Probation, Official, Seasonal }
public enum SalaryType     { Gross, Net }
public enum Relationship   { Spouse, Child, Parent }

// ============================================================
// EXTENSION METHODS — gọi như: status.ToValue(), role.ToValue()
// FromValue nhận string? — tránh NullReferenceException khi dữ liệu DB null
// ============================================================

public static class EmployeeStatusExtensions
{
    public static string ToValue(this EmployeeStatus s) => s switch
    {
        EmployeeStatus.Active   => "ACTIVE",
        EmployeeStatus.Inactive => "INACTIVE",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static EmployeeStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => EmployeeStatus.Active,
        "INACTIVE" => EmployeeStatus.Inactive,
        null       => EmployeeStatus.Active,   // default khi DB null
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class UserRoleExtensions
{
    public static string ToValue(this UserRole r) => r switch
    {
        UserRole.HrManager => "HR_MANAGER",
        UserRole.Employee  => "EMPLOYEE",
        _ => throw new ArgumentOutOfRangeException(nameof(r), r, null)
    };

    public static UserRole FromValue(string? v) => v switch
    {
        "HR_MANAGER" => UserRole.HrManager,
        "EMPLOYEE"   => UserRole.Employee,
        null         => throw new ArgumentNullException(nameof(v), "Role không được null"),
        _            => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class UserStatusExtensions
{
    public static string ToValue(this UserStatus s) => s switch
    {
        UserStatus.Active   => "ACTIVE",
        UserStatus.Inactive => "INACTIVE",
        UserStatus.Locked   => "LOCKED",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static UserStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => UserStatus.Active,
        "INACTIVE" => UserStatus.Inactive,
        "LOCKED"   => UserStatus.Locked,
        null       => UserStatus.Active,
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class GenderExtensions
{
    public static string ToValue(this Gender g) => g switch
    {
        Gender.Male   => "MALE",
        Gender.Female => "FEMALE",
        Gender.Other  => "OTHER",
        _ => throw new ArgumentOutOfRangeException(nameof(g), g, null)
    };

    public static Gender FromValue(string? v) => v switch
    {
        "MALE"   => Gender.Male,
        "FEMALE" => Gender.Female,
        "OTHER"  => Gender.Other,
        null     => Gender.Male,
        _        => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class PositionLevelExtensions
{
    public static string ToValue(this PositionLevel l) => l switch
    {
        PositionLevel.Junior => "JUNIOR",
        PositionLevel.Middle => "MIDDLE",
        PositionLevel.Senior => "SENIOR",
        _ => throw new ArgumentOutOfRangeException(nameof(l), l, null)
    };

    public static PositionLevel FromValue(string? v) => v switch
    {
        "JUNIOR" => PositionLevel.Junior,
        "MIDDLE" => PositionLevel.Middle,
        "SENIOR" => PositionLevel.Senior,
        null     => PositionLevel.Junior,
        _        => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class ContractTypeExtensions
{
    public static string ToValue(this ContractType t) => t switch
    {
        ContractType.Probation => "PROBATION",
        ContractType.Official  => "OFFICIAL",
        ContractType.Seasonal  => "SEASONAL",
        _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
    };

    public static ContractType FromValue(string? v) => v switch
    {
        "PROBATION" => ContractType.Probation,
        "OFFICIAL"  => ContractType.Official,
        "SEASONAL"  => ContractType.Seasonal,
        null        => ContractType.Official,
        _           => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class ContractStatusExtensions
{
    public static string ToValue(this ContractStatus s) => s switch
    {
        ContractStatus.Active     => "ACTIVE",
        ContractStatus.Expired    => "EXPIRED",
        ContractStatus.Terminated => "TERMINATED",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static ContractStatus FromValue(string? v) => v switch
    {
        "ACTIVE"     => ContractStatus.Active,
        "EXPIRED"    => ContractStatus.Expired,
        "TERMINATED" => ContractStatus.Terminated,
        null         => ContractStatus.Active,
        _            => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class SalaryTypeExtensions
{
    public static string ToValue(this SalaryType t) => t switch
    {
        SalaryType.Gross => "GROSS",
        SalaryType.Net   => "NET",
        _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
    };

    public static SalaryType FromValue(string? v) => v switch
    {
        "GROSS" => SalaryType.Gross,
        "NET"   => SalaryType.Net,
        null    => SalaryType.Gross,
        _       => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class RelationshipExtensions
{
    public static string ToValue(this Relationship r) => r switch
    {
        Relationship.Spouse => "SPOUSE",
        Relationship.Child  => "CHILD",
        Relationship.Parent => "PARENT",
        _ => throw new ArgumentOutOfRangeException(nameof(r), r, null)
    };

    public static Relationship FromValue(string? v) => v switch
    {
        "SPOUSE" => Relationship.Spouse,
        "CHILD"  => Relationship.Child,
        "PARENT" => Relationship.Parent,
        null     => throw new ArgumentNullException(nameof(v), "Relationship không được null"),
        _        => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class DepartmentStatusExtensions
{
    public static string ToValue(this DepartmentStatus s) => s switch
    {
        DepartmentStatus.Active   => "ACTIVE",
        DepartmentStatus.Inactive => "INACTIVE",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static DepartmentStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => DepartmentStatus.Active,
        "INACTIVE" => DepartmentStatus.Inactive,
        null       => DepartmentStatus.Active,
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class PositionStatusExtensions
{
    public static string ToValue(this PositionStatus s) => s switch
    {
        PositionStatus.Active   => "ACTIVE",
        PositionStatus.Inactive => "INACTIVE",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static PositionStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => PositionStatus.Active,
        "INACTIVE" => PositionStatus.Inactive,
        null       => PositionStatus.Active,
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class AllowanceStatusExtensions
{
    public static string ToValue(this AllowanceStatus s) => s switch
    {
        AllowanceStatus.Active   => "ACTIVE",
        AllowanceStatus.Inactive => "INACTIVE",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static AllowanceStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => AllowanceStatus.Active,
        "INACTIVE" => AllowanceStatus.Inactive,
        null       => AllowanceStatus.Active,
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}

public static class DependentStatusExtensions
{
    public static string ToValue(this DependentStatus s) => s switch
    {
        DependentStatus.Active   => "ACTIVE",
        DependentStatus.Inactive => "INACTIVE",
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    };

    public static DependentStatus FromValue(string? v) => v switch
    {
        "ACTIVE"   => DependentStatus.Active,
        "INACTIVE" => DependentStatus.Inactive,
        null       => DependentStatus.Active,
        _          => throw new ArgumentOutOfRangeException(nameof(v), v, null)
    };
}
