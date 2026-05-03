# Phân chia công việc — UTC Web 29K1 HRM

> Mỗi thành viên chọn **1 task**. Ai chọn task nào thì phụ trách:
> đọc hiểu code, chạy được, demo được, giải thích được khi bảo vệ.

---

## Phân quyền chung — 2 giao diện riêng biệt

Hệ thống có **2 role với sidebar và tính năng hoàn toàn khác nhau:**

| Role | Sidebar hiển thị | Quyền hạn |
|---|---|---|
| `HR_MANAGER` | Dashboard · Phòng ban · Chức vụ · Nhân viên · Hợp đồng · Người phụ thuộc · Danh mục phụ cấp · Phụ cấp NV | Toàn quyền: xem / tạo / sửa / vô hiệu hóa tất cả dữ liệu |
| `EMPLOYEE` | Dashboard · Hồ sơ của tôi · Hợp đồng của tôi · Phụ cấp của tôi · Người phụ thuộc | Chỉ đọc, chỉ thấy dữ liệu của **chính mình** |

> Các task có dấu ⚠️ = controller xử lý **khác nhau** tùy role — cần giải thích cả 2 luồng.

---

## `task_1` — Xác thực + Dashboard + Hồ sơ cá nhân

**Màn hình demo:** Login · Forbidden · Dashboard · Hồ sơ của tôi (4 màn)

**Files phụ trách:**
```
Program.cs
Controllers/AccountController.cs
Controllers/HomeController.cs
Controllers/ProfileController.cs
Controllers/BaseController.cs
Models/ViewModels/LoginViewModel.cs
Models/ViewModels/DashboardViewModel.cs
Views/Account/Login.cshtml
Views/Account/Forbidden.cshtml
Views/Home/Index.cshtml
Views/Profile/Index.cshtml
Views/Shared/_Layout.cshtml
wwwroot/
```

**Cần giải thích được:**
- Luồng đăng nhập: nhập username/password → kiểm tra DB → tạo cookie claim (username, role, employeeId) → redirect
- Tại sao có 2 role và sidebar thay đổi hoàn toàn tùy role (xem `_Layout.cshtml`)
- `BaseController`: `CurrentUsername` và `CurrentEmployeeId` dùng để làm gì
- Dashboard (`HR_MANAGER`): biểu đồ headcount + tuyển dụng 6 tháng gần nhất
- Profile (`EMPLOYEE`): nhân viên xem thông tin cá nhân của chính mình

---

## `task_2` — Phòng ban + Chức vụ

**Màn hình demo:** Phòng ban (Index/Details) · Chức vụ (Index/Details) · Create/Edit modal cho cả 2 (6 màn)

**Files phụ trách:**
```
Controllers/DepartmentController.cs
Controllers/PositionController.cs
Models/ViewModels/DepartmentViewModel.cs
Models/ViewModels/PositionViewModel.cs
Views/Department/     (Index, Create, Edit, Details, _FormPartial)
Views/Position/       (Index, Create, Edit, Details, _FormPartial)
```

**Cần giải thích được:**
- **Chỉ `HR_MANAGER`** truy cập được — EMPLOYEE không thấy 2 mục này trên sidebar
- Phòng ban có quan hệ cha–con (`ParentId`): trang Details hiển thị phòng ban con + danh sách nhân viên
- Bootstrap modal trên Index: Create và Edit **dùng chung 1 modal**, khi bấm Edit JS điền sẵn data vào form qua `data-*` attributes
- Form POST → Controller kiểm tra `ModelState` → `TempData["Success"/"Error"]` → `RedirectToAction` → hiện thông báo

---

## `task_3` — Nhân viên

**Màn hình demo:** Index (tìm kiếm/lọc/phân trang) · Create modal · Edit page · Details (4 màn, nhưng Details rất phong phú)

**Files phụ trách:**
```
Controllers/EmployeeController.cs
Models/ViewModels/EmployeeViewModel.cs
Views/Employee/       (Index, Create, Edit, Details, _FormPartial)
```

**Cần giải thích được:**
- **Chỉ `HR_MANAGER`** truy cập được
- Index: tìm kiếm theo tên/mã, lọc theo phòng ban + trạng thái, phân trang
- Modal trên Index: form nhiều trường (họ tên, CMND, email, giới tính, phòng ban, chức vụ...)
- Trang **Details** là màn phức tạp nhất: hiển thị hồ sơ đầy đủ + nhúng bảng hợp đồng + bảng phụ cấp + bảng người phụ thuộc của nhân viên đó
- Standalone Edit page (`GET /Employee/Edit/{id}`): load sẵn dữ liệu, có validation phía server

---

## `task_4` — Hợp đồng lao động + Người phụ thuộc ⚠️

**Màn hình demo:** Hợp đồng Index · Create modal · Edit page · Details · Người phụ thuộc Index · Create/Edit modal (6 màn)

**Files phụ trách:**
```
Controllers/ContractController.cs
Controllers/DependentController.cs
Models/ViewModels/ContractViewModel.cs
Models/ViewModels/DependentViewModel.cs
Views/Contract/       (Index, Create, Edit, Details, _FormPartial)
Views/Dependent/      (Index, Create, Edit, _FormPartial)
```

**Cần giải thích được:**
- ⚠️ **Contract — 2 luồng theo role:**
  - `HR_MANAGER`: xem tất cả hợp đồng, tạo/sửa/chấm dứt
  - `EMPLOYEE`: chỉ thấy hợp đồng của chính mình, không có nút tạo/sửa
- 3 trạng thái hợp đồng: `ACTIVE` / `EXPIRED` / `TERMINATED`; nút Chỉnh sửa chỉ hiện khi `ACTIVE`
- Trang Details hợp đồng: thông tin + danh sách phụ cấp đính kèm
- ⚠️ **Dependent — 2 luồng theo role:**
  - `HR_MANAGER`: quản lý người phụ thuộc của tất cả nhân viên
  - `EMPLOYEE`: chỉ xem người phụ thuộc của chính mình
- Quan hệ: `EmployeeDependent` → `Employee`

---

## `task_5` — Danh mục phụ cấp + Phụ cấp nhân viên ⚠️

**Màn hình demo:** Danh mục phụ cấp (Index/Details) · Phụ cấp NV Index · Create/Edit modal (5 màn)

**Files phụ trách:**
```
Controllers/AllowanceConfigController.cs
Controllers/EmployeeAllowanceController.cs
Models/ViewModels/AllowanceConfigViewModel.cs
Models/ViewModels/EmployeeAllowanceViewModel.cs
Views/AllowanceConfig/    (Index, Create, Edit, Details, _FormPartial)
Views/EmployeeAllowance/  (Index, Create, Edit, _FormPartial)
```

**Cần giải thích được:**
- `AllowanceConfig`: **chỉ `HR_MANAGER`** — cấu hình danh mục loại phụ cấp (mã, tên, mức mặc định), trang Details hiển thị tất cả nhân viên đang dùng loại đó
- ⚠️ **EmployeeAllowance — 2 luồng theo role:**
  - `HR_MANAGER`: xem/tạo/sửa phụ cấp cho tất cả nhân viên, gán phụ cấp vào 1 hợp đồng cụ thể
  - `EMPLOYEE`: chỉ xem phụ cấp của chính mình
- Quan hệ 3 bảng: `AllowanceConfig` ← `EmployeeAllowance` → `Contract`
- Modal Create cần chọn cả nhân viên + hợp đồng + loại phụ cấp + số tiền

---

## Bảng tóm tắt effort

| Task | Nội dung | Số màn demo | Độ phức tạp |
|---|---|:---:|:---:|
| `task_1` | Auth + Dashboard + Profile | 4 | ★★★★ |
| `task_2` | Phòng ban + Chức vụ | 6 | ★★★ |
| `task_3` | Nhân viên | 4 | ★★★★ |
| `task_4` | Hợp đồng + Người phụ thuộc | 6 | ★★★★ |
| `task_5` | Danh mục phụ cấp + Phụ cấp NV | 5 | ★★★★ |
