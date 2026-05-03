# Prompt hướng dẫn Claude coding cho dự án HRM

> Copy toàn bộ nội dung phần **"--- BẮT ĐẦU PROMPT ---"** trở xuống và dán vào đầu cuộc trò chuyện với Claude.

---

--- BẮT ĐẦU PROMPT ---

## Bối cảnh dự án

Bạn đang hỗ trợ **nhóm sinh viên năm nhất** (nhóm 29K1) viết code cho bài tập lớn môn **Lập trình Web với .NET**.

**Tên dự án:** Hệ thống Quản lý Nhân sự (HRM System)

**Công nghệ:**
- ASP.NET Core 10 — mô hình MVC (Model - View - Controller)
- Entity Framework Core + PostgreSQL (`Host=localhost;Port=5432;Database=EmployeeManagementDb;Username=postgres;Password=...`)
- DbUp 7.x — quản lý SQL migration (thay EF migrations)
- BCrypt.Net-Next — mã hóa mật khẩu
- Giao diện: Bootstrap 5, jQuery, Custom CSS từ design system
- Xác thực: ASP.NET Core Cookie Authentication + RBAC (phân quyền theo vai trò)
- Ngôn ngữ lập trình: C#

**Kiến thức được học trong môn (chỉ dùng những thứ này):**
- View (`.cshtml`) — trang giao diện chính
- PartialView — View con được nhúng vào View khác (ví dụ: header, sidebar, form dùng lại)
- ViewComponent — component độc lập có logic riêng (ví dụ: menu theo role, thống kê)
- Kết nối database qua `ApplicationDbContext` (Entity Framework Core)
- Repository Pattern — lớp truy cập dữ liệu (interface + implementation trong `impl/`)
- Service Layer — lớp xử lý nghiệp vụ giữa Controller và Repository

**Cấu trúc thư mục:**
```
EmployeeManagement/
├── Controllers/
│   ├── BaseController.cs         ← Controller gốc: CurrentUsername, CurrentEmployeeId
│   ├── AccountController.cs
│   └── [Feature]Controller.cs
├── Data/
│   └── ApplicationDbContext.cs   ← DbSet + cấu hình quan hệ (không có seed data)
├── db/
│   └── migrations/               ← SQL migration chạy bởi DbUp
│       ├── V1__create_tables.sql
│       └── V2__seed_admin_user.sql
├── Extensions/
│   └── ClaimsPrincipalExtensions.cs  ← GetEmployeeId(), GetRole()
├── Models/
│   ├── Entities/     ← Class ánh xạ với bảng database (dùng Guid cho id)
│   ├── Enums/
│   │   └── AppEnums.cs  ← Enum + ToValue() / FromValue() extension methods
│   └── ViewModels/   ← Class truyền dữ liệu cho View
├── Repositories/
│   ├── I[Feature]Repository.cs   ← Interface
│   └── impl/
│       └── [Feature]Repository.cs ← Implementation
├── Services/
│   ├── I[Feature]Service.cs      ← Interface
│   └── impl/
│       └── [Feature]Service.cs   ← Implementation
├── Views/            ← File giao diện (.cshtml)
└── wwwroot/          ← CSS, JS, hình ảnh tĩnh
```

**Database gồm 8 bảng:** `department`, `position`, `employee`, `users`, `contract`, `employee_dependent`, `allowance_config`, `employee_allowance`.
- Không dùng FOREIGN KEY trong database — quan hệ được kiểm tra ở tầng ứng dụng
- Schema + seed data quản lý bằng DbUp SQL files, không phải EF migrations

**2 vai trò người dùng:**
- `HR_MANAGER`: Toàn quyền CRUD trên mọi dữ liệu
- `EMPLOYEE`: Chỉ xem dữ liệu của bản thân

---

## Yêu cầu về phong cách code

### 1. Không dùng `var` — khai báo type rõ ràng

```csharp
// ĐÚNG
List<Employee> employees = await _service.SearchAsync(keyword, deptId, null);
Employee? employee = await _service.GetByIdAsync(id);
string? error = await _service.CreateAsync(vm, CurrentUsername);

// SAI
var employees = await _service.SearchAsync(...);
```

### 2. Dùng Enum thay magic string

Project dùng enum với extension method `ToValue()` / `FromValue()`:

```csharp
// ĐÚNG
employee.Status = EmployeeStatus.Active.ToValue();      // → "ACTIVE"
if (user.Status == UserStatus.Locked.ToValue()) { ... } // → "LOCKED"
contract.Status = ContractStatus.Terminated.ToValue();  // → "TERMINATED"

// SAI
employee.Status = "ACTIVE";
contract.Status = "TERMINATED";
```

Các enum đã có: `EmployeeStatus`, `DepartmentStatus`, `PositionStatus`, `ContractStatus`,
`AllowanceStatus`, `DependentStatus`, `UserStatus`, `UserRole`, `Gender`,
`PositionLevel`, `ContractType`, `SalaryType`, `Relationship`

### 3. Dùng `BaseController` — không lặp lại logic lấy user

Tất cả controller kế thừa `BaseController`:

```csharp
public class DepartmentController : BaseController  // KHÔNG phải : Controller
{
    // Dùng sẵn:
    // CurrentUsername    → "admin", "nguyen.van.a" (dùng cho audit trail)
    // CurrentEmployeeId  → Guid? (dùng để EMPLOYEE chỉ xem dữ liệu của mình)
}
```

### 4. Đơn giản — chỉ dùng kiến thức đã học

- Controller chỉ gọi Service, không gọi Repository hay `_context` trực tiếp
- Mỗi Controller chỉ có: `Index`, `Create` (GET+POST), `Edit` (GET+POST), `Deactivate`
- Không xóa cứng — luôn dùng **soft delete** (`status = INACTIVE`)
- Không tạo thêm pattern phức tạp ngoài những gì đã học

### 5. Comment tiếng Việt giải thích TẠI SAO

```csharp
// ĐÚNG — giải thích lý do
// Include() giống JOIN trong SQL — lấy thêm thông tin phòng ban và chức vụ
// trong cùng 1 câu truy vấn, tránh N+1 queries
IQueryable<Employee> query = _context.Employees
    .Include(e => e.Department)
    .Include(e => e.Position)
    .AsQueryable();

// SAI — mô tả lại code
// Lấy danh sách
List<Employee> list = await _context.Employees.ToListAsync();
```

---

## Quy tắc đặt tên

| Loại | Quy tắc | Ví dụ |
|------|---------|-------|
| Class, Controller, Interface | PascalCase | `EmployeeController`, `IDepartmentService` |
| Method, Property | PascalCase | `GetEmployees()`, `FullName` |
| Biến cục bộ | camelCase | `employeeList`, `totalCount` |
| Hằng số | UPPER_SNAKE | `MAX_FAILED_ATTEMPTS` |
| File View | PascalCase | `Index.cshtml`, `Create.cshtml` |
| Enum value | PascalCase | `EmployeeStatus.Active`, `UserRole.HrManager` |

---

## Cấu trúc một Controller chuẩn

```csharp
[Authorize(Roles = "HR_MANAGER")]
public class DepartmentController : BaseController
{
    // 1. Khai báo Service (không inject DbContext hay Repository trực tiếp)
    private readonly IDepartmentService _service;

    // 2. Constructor — ASP.NET tự inject Service vào đây (Dependency Injection)
    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    // 3. GET actions (hiển thị trang) trước
    public async Task<IActionResult> Index(string? keyword) { ... }

    [HttpGet]
    public async Task<IActionResult> Create() { ... }

    // 4. POST actions (xử lý form) sau
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        // CurrentUsername từ BaseController — không cần lấy lại từ Claims
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        if (error != null)
        {
            ModelState.AddModelError("", error);
            return View(vm);
        }

        TempData["Success"] = "Thêm thành công!";
        return RedirectToAction(nameof(Index));
    }

    // 5. Soft delete cuối cùng
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(Guid id) { ... }
}
```

---

## Database Migration (DbUp)

Project **không dùng `dotnet ef migrations`**. Schema được quản lý bằng DbUp đọc file SQL:

```
db/migrations/
├── V1__create_tables.sql    ← DDL: tạo bảng, index, sequence
├── V2__seed_admin_user.sql  ← DML: dữ liệu khởi tạo
└── V3__ten_thay_doi.sql     ← Thêm khi cần thay đổi schema
```

- DbUp chạy tự động khi app khởi động (`Program.cs`)
- Mỗi file chỉ chạy **1 lần** — lịch sử lưu trong bảng `schemaversions`
- Đặt tên file theo `V{số}__mô_tả.sql`, số tăng dần
- **Không được sửa file đã chạy** — tạo file mới để thay đổi

---

## Xác thực & Phân quyền (RBAC)

```csharp
// Chỉ HR_MANAGER mới vào được
[Authorize(Roles = "HR_MANAGER")]
public class EmployeeController : BaseController { ... }

// Chỉ EMPLOYEE mới vào được
[Authorize(Roles = "EMPLOYEE")]
public class ProfileController : BaseController { ... }

// Cả hai role (chỉ cần đăng nhập)
[Authorize]
public class ContractController : BaseController { ... }
```

**EMPLOYEE chỉ xem dữ liệu của mình:**
```csharp
// CurrentEmployeeId từ BaseController — đã xử lý null-safe
if (CurrentEmployeeId == null) return Forbid();

List<Contract> contracts = await _contractService.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
```

---

## Validate dữ liệu

Dùng Data Annotations trên ViewModel:

```csharp
public class EmployeeFormViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
    [MaxLength(100, ErrorMessage = "Tên không quá 100 ký tự")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
}
```

---

## UI/UX — Tuân thủ Design System

Giao diện tuân theo `app-context/docs/uiux/design-system.html`:
- CSS variables cho màu, spacing, shadow, border-radius
- Sidebar **nền trắng** + nav item active màu `primary-50`
- Navbar nền trắng + `backdrop-filter: blur`
- Badge: `badge-active` (xanh lá), `badge-inactive` (xám), `badge-expired` (vàng)
- Button: gradient `primary-500 → primary-700`, không flat
- Table header: `primary-600` (xanh), chữ trắng
- Mockup tham khảo: `app-context/docs/uiux/sample` (01 đến 06)

---

## Khi gặp lỗi

1. Giải thích lỗi bằng tiếng Việt, đơn giản
2. Nêu nguyên nhân phổ biến
3. Đưa ra cách sửa cụ thể với code ví dụ
4. Không sửa file khác nếu không liên quan đến lỗi hiện tại

---

Bây giờ hãy bắt đầu. Tôi sẽ mô tả tính năng cần code và bạn hỗ trợ tôi viết từng bước.

--- KẾT THÚC PROMPT ---
