# Test Plan — Hệ thống Quản lý Nhân sự (HRM Module)

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã tài liệu** | TP-HRM-v1.1 |
| **Phiên bản** | 1.1 |
| **Ngày** | 2026-04-28 |
| **Tham chiếu** | URD-HRM-v1.1 |

---

## 1. Mục tiêu kiểm thử

Xác nhận hệ thống HRM thực hiện đúng các thao tác CRUD cơ bản theo URD, và phân quyền đúng giữa HR_MANAGER và EMPLOYEE.

---

## 2. Phạm vi kiểm thử

### Trong phạm vi

| Nhóm | Tính năng | FR liên quan |
|------|-----------|-------------|
| Xác thực | Đăng nhập / Đăng xuất | FR-01, FR-02, FR-03 |
| Phòng ban | CRUD + Soft Delete | FR-04 → FR-07 |
| Chức vụ | CRUD + Soft Delete | FR-08 → FR-11 |
| Nhân viên | CRUD + Tìm kiếm | FR-12 → FR-17 |
| Hợp đồng | CRUD | FR-18 → FR-22 |
| Phụ cấp | CRUD | FR-23 → FR-26 |
| Người phụ thuộc | CRUD | FR-27, FR-28 |
| Phân quyền | EMPLOYEE không vào được trang HR | NFR-02 |

### Ngoài phạm vi

- Performance testing
- Security penetration testing
- Kiểm thử trên nhiều trình duyệt (chỉ test Chrome)

---

## 3. Loại kiểm thử

| Loại | Mô tả | Cách thực hiện |
|------|-------|----------------|
| **Functional** | Kiểm tra từng CRUD theo AC | Manual |
| **UI/Form** | Form hiển thị đúng, lỗi validation đúng chỗ | Manual |
| **Authorization** | Kiểm tra phân quyền role | Manual |
| **Negative** | Nhập thiếu, nhập sai, trùng dữ liệu | Manual |

---

## 4. Môi trường kiểm thử

| Thành phần | Giá trị |
|------------|---------|
| Trình duyệt | Google Chrome |
| Framework | ASP.NET Core 10 |
| Database | PostgreSQL (local) |
| Dữ liệu ban đầu | Seed data từ `V1__init_hrm_schema.sql` |
| URL | `http://localhost:5173` |

**Tài khoản test:**

| Username | Password | Role |
|----------|----------|------|
| `admin` | `Admin@123` | HR_MANAGER |
| `nv001` | `Emp@123` | EMPLOYEE |

---

## 5. Tiêu chí vào (Entry Criteria)

- [ ] Build ứng dụng thành công (không lỗi compile)
- [ ] Database đã chạy migration + seed data
- [ ] Truy cập được `http://localhost:5173`

---

## 6. Tiêu chí ra (Exit Criteria)

- [ ] 100% test case P0 (Critical) PASS
- [ ] ≥ 90% test case P1 (High) PASS
- [ ] Không còn bug Critical/Blocker

---

## 7. Mức độ lỗi

| Mức | Mô tả | Ví dụ |
|-----|-------|-------|
| **Critical** | Không dùng được hệ thống | Không đăng nhập được |
| **High** | Chức năng chính bị lỗi | Không lưu được nhân viên |
| **Medium** | Chức năng hoạt động nhưng có vấn đề nhỏ | Thông báo lỗi sai nội dung |
| **Low** | Giao diện nhỏ | Căn lề không đều |

---

## 8. Danh sách Test Cases

Xem chi tiết tại [TestCases.md](TestCases.md).

| Mã | Tính năng | Mức độ |
|----|-----------|--------|
| TC-001 | Đăng nhập thành công — HR_MANAGER | P0 |
| TC-002 | Đăng nhập thành công — EMPLOYEE | P0 |
| TC-003 | Đăng nhập sai mật khẩu | P1 |
| TC-004 | Đăng xuất | P0 |
| TC-005 | EMPLOYEE không vào được trang HR | P0 |
| TC-006 | Thêm phòng ban hợp lệ | P1 |
| TC-007 | Thêm phòng ban mã trùng | P1 |
| TC-008 | Sửa phòng ban | P1 |
| TC-009 | Vô hiệu hóa phòng ban | P1 |
| TC-010 | Thêm chức vụ hợp lệ | P1 |
| TC-011 | Xem danh sách nhân viên + lọc | P1 |
| TC-012 | Thêm nhân viên hợp lệ | P1 |
| TC-013 | Thêm nhân viên email trùng | P1 |
| TC-014 | Sửa nhân viên | P1 |
| TC-015 | Vô hiệu hóa nhân viên | P1 |
| TC-016 | EMPLOYEE xem hồ sơ bản thân | P1 |
| TC-017 | Thêm hợp đồng hợp lệ | P1 |
| TC-018 | Sửa hợp đồng | P1 |
| TC-019 | EMPLOYEE xem hợp đồng của mình | P1 |
| TC-020 | EMPLOYEE không xem HĐ của NV khác | P0 |

---

**END OF DOCUMENT**
