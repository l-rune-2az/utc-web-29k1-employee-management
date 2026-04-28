# HRM System — Hệ thống Quản lý Nhân sự

Bài tập lớn môn **Lập trình Web với .NET** — Nhóm 29K1

---

## Thông tin nhóm

| | |
|--|--|
| **Nhóm** | 29K1 |
| **Môn học** | Lập trình Web với .NET |
| **Năm học** | 2025 – 2026 |

---

## Giới thiệu

Hệ thống Quản lý Nhân sự (HRM) là ứng dụng web nội bộ giúp doanh nghiệp số hóa toàn bộ quy trình quản lý nhân sự — từ hồ sơ nhân viên, hợp đồng lao động đến phụ cấp và người phụ thuộc.

Hệ thống phục vụ **2 nhóm người dùng**:

- **HR Manager** — phòng nhân sự, có toàn quyền quản lý dữ liệu: thêm, xem, sửa, vô hiệu hóa nhân viên và các thông tin liên quan.
- **Nhân viên** — có thể tra cứu hồ sơ cá nhân, hợp đồng lao động và các khoản phụ cấp đang hưởng của mình, không xem được dữ liệu người khác.

> **Nguyên tắc soft delete:** Hệ thống không bao giờ xóa dữ liệu khỏi database. Mọi thao tác "xóa" chỉ đặt trường `status = INACTIVE`, giữ nguyên lịch sử.

---

## Tính năng chi tiết

### HR Manager

**Quản lý Phòng ban**
- Xem danh sách phòng ban, tìm kiếm theo tên
- Thêm phòng ban mới với mã, tên, mô tả; hỗ trợ cấu trúc phân cấp cha-con
- Sửa thông tin, vô hiệu hóa phòng ban
- Phòng ban INACTIVE không xuất hiện trong dropdown khi tạo nhân viên

**Quản lý Chức vụ**
- CRUD danh mục chức vụ với 3 cấp độ: JUNIOR / MIDDLE / SENIOR
- Vô hiệu hóa chức vụ không còn dùng

**Quản lý Nhân viên**
- Xem danh sách với bộ lọc theo tên, phòng ban, trạng thái
- Thêm nhân viên mới — mã nhân viên tự sinh theo format `NVYYMMDD###` (ví dụ: `NV260428001`)
- Sửa thông tin cá nhân: họ tên, ngày sinh, giới tính, CMND/CCCD, email, số điện thoại, địa chỉ, phòng ban, chức vụ
- Vô hiệu hóa nhân viên khi nghỉ việc

**Quản lý Hợp đồng**
- Xem toàn bộ lịch sử hợp đồng của từng nhân viên
- Ký hợp đồng mới: 3 loại (PROBATION — thử việc, OFFICIAL — chính thức, SEASONAL — thời vụ)
- Lưu 2 mức lương: lương cơ bản (tính BHXH) và lương thỏa thuận (tính thực tế)
- Kết thúc hợp đồng khi nhân viên nghỉ việc hoặc hết hạn

**Quản lý Phụ cấp**
- Định nghĩa danh mục loại phụ cấp (ăn trưa, điện thoại, xăng xe…) với số tiền gợi ý
- Gán phụ cấp cụ thể cho nhân viên theo từng hợp đồng, với số tiền và ngày hiệu lực thực tế
- Vô hiệu hóa phụ cấp khi không còn áp dụng

**Quản lý Người phụ thuộc**
- Thêm thông tin người phụ thuộc của nhân viên (vợ/chồng, con, cha mẹ)
- Lưu họ tên, ngày sinh, quan hệ, số CMND/CCCD
- Dữ liệu này phục vụ tính giảm trừ gia cảnh thuế TNCN

### Nhân viên

- Xem toàn bộ hồ sơ cá nhân (chỉ đọc đối với thông tin quan trọng)
- Xem danh sách hợp đồng lao động của bản thân theo thời gian
- Xem các khoản phụ cấp đang hưởng
- Xem danh sách người phụ thuộc đã đăng ký
- Hệ thống chặn truy cập nếu cố xem dữ liệu của nhân viên khác (403 Forbidden)

---

## Công nghệ sử dụng

| Thành phần | Công nghệ | Lý do chọn |
|-----------|-----------|------------|
| Framework | ASP.NET Core 10 MVC | Chuẩn môn học, tách biệt rõ Controller / View |
| Ngôn ngữ | C# | Mạnh về kiểu dữ liệu, phù hợp enterprise |
| Database | PostgreSQL 15+ | Miễn phí, mạnh, hỗ trợ tốt UUID và kiểu dữ liệu phong phú |
| ORM | Entity Framework Core 10 (Npgsql) | Viết truy vấn bằng C# thay SQL thủ công |
| Migration | DbUp | Chạy file `.sql` tự động khi khởi động, không cần lệnh ef |
| Frontend | Bootstrap 5, jQuery | Responsive, sẵn có, phù hợp sinh viên |
| Xác thực | Cookie Authentication | Tích hợp sẵn trong ASP.NET Core, hỗ trợ `[Authorize]` |
| Mã hóa mật khẩu | BCrypt.Net-Next | Không lưu plaintext, an toàn với cost factor 12 |
| Kiến trúc | Repository + Service Pattern | Tách biệt trách nhiệm, dễ bảo trì |

---

## Cài đặt và chạy

### Yêu cầu

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 15+](https://www.postgresql.org/download/)

### Các bước

**1. Clone dự án**

```bash
git clone <url>
cd utc-web-29k1-employee-management
```

**2. Tạo database trống trong PostgreSQL**

```sql
CREATE DATABASE hrms;
```

**3. Cấu hình connection string**

Mở `appsettings.json`, sửa `Username` và `Password` cho khớp máy của bạn:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=hrms;Username=postgres;Password=postgres"
}
```

**4. Chạy ứng dụng**

```bash
dotnet run
```

Lần đầu chạy, **DbUp tự động** thực thi các file SQL trong `db/migrations/` theo đúng thứ tự:
- `V1__create_tables.sql` — tạo 8 bảng, sequence, index, constraint
- `V2__seed_admin_user.sql` — tạo tài khoản admin mặc định

Không cần chạy `dotnet ef database update` hay bất kỳ lệnh thủ công nào.

**5. Truy cập**

Mở trình duyệt: `http://localhost:5173`

---

## Tài khoản mặc định

| Username | Password | Vai trò |
|----------|----------|---------|
| `admin` | `Admin@123` | HR Manager |

Để tạo tài khoản nhân viên (EMPLOYEE): đăng nhập bằng `admin`, tạo nhân viên mới, sau đó tạo tài khoản liên kết với nhân viên đó.

---

## Cấu trúc thư mục

```
EmployeeManagement/
├── Controllers/                    # Nhận request, gọi Service, trả kết quả về View
│   ├── AccountController.cs        # Đăng nhập / Đăng xuất / Trang Forbidden
│   ├── HomeController.cs           # Dashboard tổng quan
│   ├── DepartmentController.cs     # CRUD Phòng ban
│   ├── PositionController.cs       # CRUD Chức vụ
│   ├── EmployeeController.cs       # CRUD Nhân viên
│   ├── ContractController.cs       # CRUD Hợp đồng
│   ├── AllowanceConfigController.cs # CRUD Danh mục phụ cấp
│   ├── EmployeeAllowanceController.cs # CRUD Phụ cấp nhân viên
│   ├── DependentController.cs      # CRUD Người phụ thuộc
│   └── ProfileController.cs        # Hồ sơ cá nhân (chỉ EMPLOYEE)
│
├── Models/
│   ├── Entities/                   # Class C# ánh xạ 1-1 với bảng trong database
│   │   ├── Department.cs           # Phòng ban, có navigation property Children, Employees
│   │   ├── Position.cs             # Chức vụ với cấp độ JUNIOR/MIDDLE/SENIOR
│   │   ├── Employee.cs             # Nhân viên — bảng trung tâm
│   │   ├── Users.cs                # Tài khoản đăng nhập, lưu BCrypt hash
│   │   ├── Contract.cs             # Hợp đồng với BaseSalary và OfferSalary
│   │   ├── EmployeeDependent.cs    # Người phụ thuộc
│   │   ├── AllowanceConfig.cs      # Danh mục loại phụ cấp
│   │   └── EmployeeAllowance.cs    # Phụ cấp gán theo hợp đồng
│   └── ViewModels/                 # Class chứa dữ liệu truyền vào View, có Data Annotations
│       ├── LoginViewModel.cs
│       ├── DepartmentViewModel.cs  # DepartmentFormViewModel, DepartmentIndexViewModel
│       ├── PositionViewModel.cs
│       ├── EmployeeViewModel.cs
│       ├── ContractViewModel.cs
│       ├── AllowanceConfigViewModel.cs
│       ├── EmployeeAllowanceViewModel.cs
│       └── DependentViewModel.cs
│
├── Data/
│   └── ApplicationDbContext.cs     # DbContext — định nghĩa DbSet, cấu hình quan hệ và constraint
│
├── Repositories/                   # Lớp truy cập database, gọi DbContext
│   ├── Interfaces/                 # IRepository cho từng Entity
│   └── Implementations/            # Class cài đặt thực tế
│
├── Services/                       # Lớp nghiệp vụ — validate, sinh mã NV, hash BCrypt...
│   ├── Interfaces/                 # IService cho từng module
│   └── Implementations/
│
├── Views/                          # Giao diện Razor (.cshtml)
│   ├── Shared/
│   │   ├── _Layout.cshtml          # Layout chính: navbar + sidebar động theo role
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Account/
│   │   ├── Login.cshtml            # Trang đăng nhập (split-screen, gradient)
│   │   └── Forbidden.cshtml        # Trang 403
│   ├── Home/Index.cshtml           # Dashboard: stat cards + bảng nhân viên mới
│   ├── Department/                 # Index.cshtml, Create.cshtml, Edit.cshtml
│   ├── Position/
│   ├── Employee/
│   ├── Contract/
│   ├── AllowanceConfig/
│   ├── EmployeeAllowance/
│   ├── Dependent/
│   └── Profile/Index.cshtml        # Hồ sơ nhân viên với tab Xem / Cập nhật
│
├── wwwroot/
│   ├── css/site.css
│   ├── js/site.js
│   └── lib/                        # Bootstrap 5, jQuery, jQuery Validation
│
└── db/
    └── migrations/
        ├── V1__create_tables.sql   # DDL: 8 bảng + 2 sequence + index + CHECK constraint
        └── V2__seed_admin_user.sql # Tài khoản admin mặc định (BCrypt hashed)
```

---

## Database

Migration chạy tự động qua **DbUp** — không cần lệnh `dotnet ef`.

### 8 bảng

| Bảng | Mô tả | Điểm đặc biệt |
|------|-------|----------------|
| `department` | Phòng ban | Self-referential qua `parent_id` — hỗ trợ cây phân cấp |
| `position` | Chức vụ | Enum `level`: JUNIOR / MIDDLE / SENIOR |
| `employee` | Nhân viên | Mã tự sinh `NVYYMMDD###` qua `seq_employee_code` |
| `users` | Tài khoản | Mật khẩu BCrypt, có `failed_attempts` và `locked_until` |
| `contract` | Hợp đồng | 2 trường lương: `base_salary` (BHXH) và `offer_salary` (thực tế) |
| `employee_dependent` | Người phụ thuộc | Quan hệ: SPOUSE / CHILD / PARENT |
| `allowance_config` | Danh mục phụ cấp | Template với `default_amount` gợi ý |
| `employee_allowance` | Phụ cấp NV | Liên kết 3 chiều: employee + contract + allowance_config |

### Sequence

| Sequence | Dùng cho |
|----------|---------|
| `seq_employee_code` | Số thứ tự trong mã nhân viên `NVYYMMDD###` |
| `seq_contract_number` | Số thứ tự trong số hợp đồng `HĐ-YYYY-NNN` |

---

## Kiến trúc & Luồng xử lý

```
Trình duyệt
    │  HTTP Request
    ▼
Controller          ← [Authorize(Roles = "...")] chặn sai role
    │  gọi
    ▼
Service Layer       ← validate, nghiệp vụ (sinh mã NV, BCrypt, soft delete...)
    │  gọi
    ▼
Repository          ← truy vấn database qua DbContext (LINQ / EF Core)
    │
    ▼
PostgreSQL
    │  kết quả
    ▼
Controller → ViewModel → View (.cshtml)
    │  render HTML
    ▼
Trình duyệt
```

### Xác thực & Phân quyền (RBAC)

1. Người dùng đăng nhập → `AccountController` kiểm tra DB + `BCrypt.Verify`
2. Thành công → tạo `ClaimsPrincipal` chứa `Username`, `Role`, `EmployeeId`
3. ASP.NET mã hóa Claims → lưu vào **Cookie** trình duyệt
4. Mọi request sau: ASP.NET tự đọc Cookie → `[Authorize(Roles = "HR_MANAGER")]` tự chặn nếu sai role
5. EMPLOYEE cố xem dữ liệu người khác → Controller so sánh `EmployeeId` từ Claims → trả `403 Forbidden`

### Sidebar động theo role

`_Layout.cshtml` kiểm tra `User.IsInRole("HR_MANAGER")`:
- HR Manager thấy: Phòng ban, Chức vụ, Nhân viên, Hợp đồng, Phụ cấp
- Nhân viên thấy: Hồ sơ của tôi, Hợp đồng, Phụ cấp, Người phụ thuộc

---

## Quy ước code

| Quy ước | Áp dụng |
|---------|---------|
| Soft delete | Không `DELETE` — chỉ set `status = "INACTIVE"` |
| UUID | Tất cả Primary Key đều dùng `Guid` |
| Audit trail | Mọi bảng có `created_at`, `created_by`, `updated_at`, `updated_by` |
| Không FK ở DB | Quan hệ được kiểm tra ở tầng Service, không dùng `FOREIGN KEY` constraint |
| Validation | Data Annotations trên ViewModel + kiểm tra `ModelState.IsValid` trong Controller |
| Mật khẩu | BCrypt cost = 12, không bao giờ lưu plaintext |
