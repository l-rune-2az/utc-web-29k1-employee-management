# HLD — High Level Design
# Hệ thống Quản lý Nhân sự (HRM Module)

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã tài liệu** | HLD-HRM-v1.1 |
| **Phiên bản** | 1.1 |
| **Ngày** | 2026-04-28 |
| **Tham chiếu** | URD-HRM-v1.1, ERD-HRM-Module.md |

---

## Mục lục

1. [Tổng quan kiến trúc](#1-tổng-quan-kiến-trúc)
2. [Mô hình MVC — Phân tầng](#2-mô-hình-mvc--phân-tầng)
3. [Cấu trúc thư mục Source Code](#3-cấu-trúc-thư-mục-source-code)
4. [Thiết kế Database Layer](#4-thiết-kế-database-layer)
5. [Thiết kế Controllers](#5-thiết-kế-controllers)
6. [Thiết kế Views](#6-thiết-kế-views)
7. [Phân quyền](#7-phân-quyền)
8. [Sơ đồ trang (Sitemap)](#8-sơ-đồ-trang-sitemap)

---

## 1. Tổng quan kiến trúc

Hệ thống dùng **ASP.NET Core MVC** — kiến trúc 3 tầng đơn giản:

```
┌──────────────────────────────────────────────┐
│                  BROWSER                     │
└─────────────────────┬────────────────────────┘
                      │ HTTP
┌─────────────────────▼────────────────────────┐
│            ASP.NET Core MVC App              │
│                                              │
│  View (.cshtml) ◄── Controller ──► Model     │
│                          │                   │
│                   DbContext (EF Core)        │
└─────────────────────┬────────────────────────┘
                      │ SQL
┌─────────────────────▼────────────────────────┐
│              PostgreSQL Database             │
└──────────────────────────────────────────────┘
```

**Stack công nghệ:**

| Tầng | Công nghệ |
|------|-----------|
| Giao diện | Razor Views (.cshtml) + Bootstrap 5 |
| Xử lý | ASP.NET Core MVC (C#) |
| Dữ liệu | Entity Framework Core + Npgsql |
| Database | PostgreSQL |
| Xác thực | Cookie Authentication |

---

## 2. Mô hình MVC — Phân tầng

```
[1] Người dùng gửi HTTP Request
         ↓
[2] Controller — nhận request, gọi DbContext
         ↓
[3] DbContext — truy vấn PostgreSQL
         ↓
[4] Controller — đưa kết quả vào ViewModel
         ↓
[5] View (.cshtml) — render HTML trả về browser
```

**Nguyên tắc:**

| Tầng | Làm gì | Không làm gì |
|------|--------|-------------|
| **Model** | Khai báo cấu trúc dữ liệu, validation | Không chứa HTML |
| **Controller** | Nhận request, gọi DB, trả View | Không chứa HTML |
| **View** | Hiển thị dữ liệu | Không gọi DB trực tiếp |
| **DbContext** | Giao tiếp với DB | — |

---

## 3. Cấu trúc thư mục Source Code

```
EmployeeManagement/
├── Controllers/
│   ├── AccountController.cs          # Đăng nhập / Đăng xuất
│   ├── HomeController.cs             # Dashboard
│   ├── DepartmentController.cs       # CRUD Phòng ban
│   ├── PositionController.cs         # CRUD Chức vụ
│   ├── EmployeeController.cs         # CRUD Nhân viên (HR)
│   ├── ProfileController.cs          # Xem hồ sơ (EMPLOYEE)
│   ├── ContractController.cs         # CRUD Hợp đồng
│   ├── AllowanceConfigController.cs  # CRUD Danh mục phụ cấp
│   ├── EmployeeAllowanceController.cs # CRUD Phụ cấp NV
│   └── DependentController.cs        # CRUD Người phụ thuộc
│
├── Models/
│   ├── Entities/                     # Map 1:1 với bảng DB
│   │   ├── Department.cs
│   │   ├── Position.cs
│   │   ├── Employee.cs
│   │   ├── Users.cs
│   │   ├── Contract.cs
│   │   ├── EmployeeDependent.cs
│   │   ├── AllowanceConfig.cs
│   │   └── EmployeeAllowance.cs
│   └── ViewModels/                   # Dữ liệu truyền cho View
│       ├── LoginViewModel.cs
│       ├── EmployeeCreateViewModel.cs
│       ├── EmployeeEditViewModel.cs
│       └── ContractCreateViewModel.cs
│
├── Data/
│   ├── ApplicationDbContext.cs       # EF Core DbContext
│   └── Migrations/
│
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml            # Layout chung
│   ├── Account/
│   │   └── Login.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Department/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│   ├── Position/        (tương tự)
│   ├── Employee/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Details.cshtml
│   ├── Profile/
│   │   └── Index.cshtml
│   ├── Contract/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│   └── ...
│
├── wwwroot/
│   ├── css/site.css
│   └── lib/ (Bootstrap, jQuery)
│
├── Program.cs
└── appsettings.json
```

---

## 4. Thiết kế Database Layer

### DbContext — 8 bảng

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Department>        Departments        { get; set; }
    public DbSet<Position>          Positions          { get; set; }
    public DbSet<Employee>          Employees          { get; set; }
    public DbSet<Users>             Users              { get; set; }
    public DbSet<Contract>          Contracts          { get; set; }
    public DbSet<EmployeeDependent> EmployeeDependents { get; set; }
    public DbSet<AllowanceConfig>   AllowanceConfigs   { get; set; }
    public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }
}
```

### Quan hệ giữa các bảng (ứng dụng tự quản lý, không dùng FK trong DB)

```
department ◄── employee ──► position
               │    │
               ▼    ▼
             users  contract ──► employee_allowance ◄── allowance_config
                    │
               employee_dependent
```

---

## 5. Thiết kế Controllers

Mỗi Controller chỉ có 4 actions CRUD chuẩn + Deactivate (soft delete):

| Action | HTTP | Mô tả |
|--------|------|-------|
| `Index` | GET | Xem danh sách (có tìm kiếm) |
| `Create` | GET | Hiển thị form thêm mới |
| `Create` | POST | Xử lý lưu bản ghi mới |
| `Edit` | GET | Hiển thị form sửa |
| `Edit` | POST | Xử lý cập nhật |
| `Deactivate` | POST | Soft delete (status = INACTIVE) |

### Bảng Controllers theo tính năng

| Controller | Role | FR liên quan |
|-----------|------|-------------|
| `AccountController` | ALL | FR-01, FR-02 |
| `DepartmentController` | HR_MANAGER | FR-04 → FR-07 |
| `PositionController` | HR_MANAGER | FR-08 → FR-11 |
| `EmployeeController` | HR_MANAGER | FR-12 → FR-16 |
| `ProfileController` | EMPLOYEE | FR-17 |
| `ContractController` | HR/EMPLOYEE | FR-18 → FR-22 |
| `AllowanceConfigController` | HR_MANAGER | FR-23 |
| `EmployeeAllowanceController` | HR/EMPLOYEE | FR-24 → FR-26 |
| `DependentController` | HR/EMPLOYEE | FR-27, FR-28 |

---

## 6. Thiết kế Views

### Layout chung `_Layout.cshtml`

```
┌─────────────────────────────────────────────────┐
│  LOGO  │  Menu (theo role)   │  Tên user + Logout│
├────────┬────────────────────────────────────────┤
│        │                                        │
│ Sidebar│   Widget Card (nội dung trang)         │
│ (HR)   │                                        │
│        │                                        │
└────────┴────────────────────────────────────────┘
```

### Menu theo role

| Menu | HR_MANAGER | EMPLOYEE |
|------|:----------:|:--------:|
| Dashboard | ✓ | ✓ |
| Phòng ban | ✓ | ✗ |
| Chức vụ | ✓ | ✗ |
| Nhân viên | ✓ | ✗ |
| Hợp đồng (tất cả) | ✓ | ✗ |
| Phụ cấp (danh mục) | ✓ | ✗ |
| Hồ sơ của tôi | ✗ | ✓ |
| Hợp đồng của tôi | ✗ | ✓ |
| Phụ cấp của tôi | ✗ | ✓ |

### ViewModel — Phân biệt Entity vs ViewModel

| Loại | Dùng cho | Ví dụ |
|------|----------|-------|
| **Entity** | Map với bảng DB | `Employee` — 17 fields |
| **ViewModel** | Truyền dữ liệu cho View | `EmployeeCreateViewModel` — chỉ các fields trên form + danh sách dropdown |

```csharp
public class EmployeeCreateViewModel
{
    [Required] public string Name { get; set; }
    [Required] public DateTime Dob { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public string IdCard { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    [Required] public Guid DeptId { get; set; }
    [Required] public Guid PositionId { get; set; }
    [Required] public DateTime HireDate { get; set; }

    // Dùng để render dropdown
    public List<SelectListItem> Departments { get; set; } = new();
    public List<SelectListItem> Positions   { get; set; } = new();
}
```

---

## 7. Phân quyền

### Cấu hình Cookie Auth

```csharp
// Program.cs
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath   = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });
```

### Attribute trên Controller

```csharp
[Authorize(Roles = "HR_MANAGER")]
public class EmployeeController : Controller { ... }

[Authorize(Roles = "EMPLOYEE")]
public class ProfileController : Controller { ... }

[Authorize]   // cả 2 role đều vào được
public class ContractController : Controller { ... }
```

### Bảo vệ dữ liệu EMPLOYEE

EMPLOYEE chỉ xem được dữ liệu của mình — kiểm tra trong Controller trước khi trả View:

```csharp
var myId = User.FindFirst("EmployeeId")?.Value;
if (record.EmpId.ToString() != myId) return Forbid();
```

---

## 8. Sơ đồ trang (Sitemap)

```
/Account/Login
    │
    ├─ [HR_MANAGER] → /Home/Index (Dashboard)
    │       ├── /Department         → Index / Create / Edit
    │       ├── /Position           → Index / Create / Edit
    │       ├── /Employee           → Index / Create / Edit / Details
    │       │       └── /Contract?empId=...       → Index / Create / Edit
    │       │       └── /EmployeeAllowance?empId= → Index / Create / Edit
    │       │       └── /Dependent?empId=         → Index / Create / Edit
    │       └── /AllowanceConfig    → Index / Create / Edit
    │
    └─ [EMPLOYEE] → /Home/Index (Dashboard)
            ├── /Profile/Index          (chỉ xem)
            ├── /Contract/MyContracts   (chỉ xem)
            ├── /EmployeeAllowance/Mine (chỉ xem)
            └── /Dependent/Mine         (chỉ xem)
```

---

**END OF DOCUMENT**
