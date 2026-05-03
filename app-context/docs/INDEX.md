# Tài liệu dự án — Employee Management System
## Nhóm 29K1 — Môn Lập trình Web .NET

---

## 1. URD — Yêu cầu người dùng
- [URD-HRM-Module.md](urd/URD-HRM-Module.md) — User Requirements Document đầy đủ

## 2. User Stories
| File | Mô tả |
|------|-------|
| [EP-001.md](user-stories/EP-001.md) | Epic: Xác thực & Phân quyền |
| [EP-002.md](user-stories/EP-002.md) | Epic: Quản lý Phòng ban & Chức vụ |
| [EP-003.md](user-stories/EP-003.md) | Epic: Quản lý Nhân viên |
| [EP-004.md](user-stories/EP-004.md) | Epic: Quản lý Hợp đồng |
| [EP-005.md](user-stories/EP-005.md) | Epic: Quản lý Phụ cấp & Người phụ thuộc |
| [US-001.md](user-stories/US-001.md) | Đăng nhập hệ thống |
| [US-002.md](user-stories/US-002.md) | Đăng xuất |
| [US-003.md](user-stories/US-003.md) | Quản lý Phòng ban (CRUD) |
| [US-004.md](user-stories/US-004.md) | Quản lý Chức vụ (CRUD) |
| [US-005.md](user-stories/US-005.md) | Xem danh sách Nhân viên |
| [US-006.md](user-stories/US-006.md) | Thêm Nhân viên mới |
| [US-007.md](user-stories/US-007.md) | Sửa Nhân viên (HR) |
| [US-008.md](user-stories/US-008.md) | Vô hiệu hóa Nhân viên |
| [US-009.md](user-stories/US-009.md) | Nhân viên xem & sửa hồ sơ cá nhân |
| [US-010.md](user-stories/US-010.md) | Xem danh sách Hợp đồng |
| [US-011.md](user-stories/US-011.md) | Thêm Hợp đồng mới |
| [US-012.md](user-stories/US-012.md) | Sửa Hợp đồng |
| [US-013.md](user-stories/US-013.md) | Kết thúc Hợp đồng |
| [US-014.md](user-stories/US-014.md) | Nhân viên xem Hợp đồng của mình |
| [US-015.md](user-stories/US-015.md) | Quản lý danh mục Phụ cấp |
| [US-016.md](user-stories/US-016.md) | Gán Phụ cấp cho Nhân viên |
| [US-017.md](user-stories/US-017.md) | Nhân viên xem Phụ cấp của mình |
| [US-018.md](user-stories/US-018.md) | Quản lý Người phụ thuộc |
| [US-019.md](user-stories/US-019.md) | Nhân viên xem Người phụ thuộc |

## 3. High Level Design
- [HLD-HRM-Module.md](hld/HLD-HRM-Module.md) — Kiến trúc hệ thống, cấu trúc code, luồng xử lý

## 4. UI/UX — Sample HTML
| File | Màn hình |
|------|---------|
| [01-login.html](uiux/01-login.html) | Trang đăng nhập |
| [02-dashboard-hrm.html](uiux/02-dashboard-hrm.html) | Dashboard HR Manager |
| [03-employee-list.html](uiux/03-employee-list.html) | Danh sách Nhân viên |
| [04-employee-form.html](uiux/04-employee-form.html) | Form thêm Nhân viên |
| [05-employee-profile.html](uiux/05-employee-profile.html) | Hồ sơ cá nhân (EMPLOYEE) |
| [06-contract-list.html](uiux/06-contract-list.html) | Danh sách Hợp đồng |

## 5. Test
| File | Mô tả |
|------|-------|
| [TestPlan.md](test/TestPlan.md) | Kế hoạch kiểm thử |
| [TestCases.md](test/TestCases.md) | 25 test cases chi tiết |

## 6. SQL Migration
| File | Mô tả |
|------|-------|
| [V1__init_hrm_schema.sql](sql/V1__init_hrm_schema.sql) | DDL: Tạo 8 bảng + seed data (không FK) |
| [V1__rollback.sql](sql/V1__rollback.sql) | Rollback: DROP toàn bộ bảng theo đúng thứ tự |

## 7. ERD (đã có sẵn)
- [ERD-HRM-Module.md](erd/ERD-HRM-Module.md) — Sơ đồ quan hệ thực thể
