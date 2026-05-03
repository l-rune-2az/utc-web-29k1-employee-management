# URD — User Requirements Document
# Hệ thống Quản lý Nhân sự (HRM Module)

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã tài liệu** | URD-HRM-v1.1 |
| **Phiên bản** | 1.1 |
| **Ngày** | 2026-04-28 |
| **Nhóm** | 29K1 — Môn Lập trình Web .NET |
| **Trạng thái** | Draft |

---

## Mục lục

1. [Mục tiêu dự án](#1-mục-tiêu-dự-án)
2. [Phạm vi hệ thống](#2-phạm-vi-hệ-thống)
3. [Các bên liên quan](#3-các-bên-liên-quan)
4. [Yêu cầu chức năng](#4-yêu-cầu-chức-năng)
5. [Yêu cầu phi chức năng](#5-yêu-cầu-phi-chức-năng)
6. [Ràng buộc hệ thống](#6-ràng-buộc-hệ-thống)
7. [Bảng chú giải](#7-bảng-chú-giải)

---

## 1. Mục tiêu dự án

Xây dựng hệ thống **Quản lý Nhân sự (HRM)** dạng web, cho phép:

- **HR Manager** thêm, xem, sửa, vô hiệu hóa thông tin nhân viên, phòng ban, chức vụ, hợp đồng và phụ cấp.
- **Nhân viên** tra cứu thông tin cá nhân, hợp đồng và phụ cấp của mình.

**Kết quả mong đợi:** Dữ liệu nhân sự được quản lý tập trung, thay thế bảng tính Excel thủ công.

---

## 2. Phạm vi hệ thống

### Trong phạm vi

| STT | Tính năng |
|-----|-----------|
| 1 | Đăng nhập / Đăng xuất |
| 2 | Quản lý Phòng ban (CRUD) |
| 3 | Quản lý Chức vụ (CRUD) |
| 4 | Quản lý Nhân viên (CRUD + tìm kiếm) |
| 5 | Quản lý Hợp đồng lao động (CRUD) |
| 6 | Quản lý Phụ cấp (CRUD) |
| 7 | Quản lý Người phụ thuộc (CRUD) |
| 8 | Phân quyền theo vai trò (HR_MANAGER / EMPLOYEE) |

### Ngoài phạm vi

- Tính lương, tính thuế (Payroll — dự án riêng)
- Chấm công, nghỉ phép
- Tuyển dụng, đào tạo

---

## 3. Các bên liên quan

| Vai trò | Mô tả | Quyền hạn |
|---------|-------|-----------|
| **HR_MANAGER** | Nhân viên phòng Nhân sự | Toàn quyền CRUD trên mọi dữ liệu |
| **EMPLOYEE** | Nhân viên bình thường | Chỉ xem dữ liệu của bản thân |
| **Giảng viên** | Người đánh giá bài tập | — |

---

## 4. Yêu cầu chức năng

### 4.1 Đăng nhập / Đăng xuất

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-01 | Đăng nhập bằng username + password | ALL |
| FR-02 | Đăng xuất khỏi hệ thống | ALL |
| FR-03 | Sau đăng nhập, HR_MANAGER thấy menu quản lý; EMPLOYEE chỉ thấy menu cá nhân | System |

### 4.2 Quản lý Phòng ban

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-04 | Xem danh sách phòng ban (tìm kiếm theo tên) | HR_MANAGER |
| FR-05 | Thêm phòng ban mới (mã, tên, mô tả, phòng ban cha) | HR_MANAGER |
| FR-06 | Sửa thông tin phòng ban | HR_MANAGER |
| FR-07 | Vô hiệu hóa phòng ban (soft delete — đặt status = INACTIVE) | HR_MANAGER |

### 4.3 Quản lý Chức vụ

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-08 | Xem danh sách chức vụ | HR_MANAGER |
| FR-09 | Thêm chức vụ mới (mã, tên, cấp độ) | HR_MANAGER |
| FR-10 | Sửa thông tin chức vụ | HR_MANAGER |
| FR-11 | Vô hiệu hóa chức vụ | HR_MANAGER |

### 4.4 Quản lý Nhân viên

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-12 | Xem danh sách nhân viên (lọc theo tên, phòng ban, trạng thái) | HR_MANAGER |
| FR-13 | Xem chi tiết nhân viên | HR_MANAGER |
| FR-14 | Thêm nhân viên mới | HR_MANAGER |
| FR-15 | Sửa thông tin nhân viên | HR_MANAGER |
| FR-16 | Vô hiệu hóa nhân viên | HR_MANAGER |
| FR-17 | Nhân viên xem thông tin cá nhân của bản thân | EMPLOYEE |

### 4.5 Quản lý Hợp đồng

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-18 | Xem danh sách hợp đồng của một nhân viên | HR_MANAGER |
| FR-19 | Thêm hợp đồng mới | HR_MANAGER |
| FR-20 | Sửa hợp đồng | HR_MANAGER |
| FR-21 | Vô hiệu hóa / kết thúc hợp đồng | HR_MANAGER |
| FR-22 | Nhân viên xem hợp đồng của bản thân | EMPLOYEE |

### 4.6 Quản lý Phụ cấp

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-23 | Xem, thêm, sửa, vô hiệu hóa danh mục loại phụ cấp | HR_MANAGER |
| FR-24 | Gán phụ cấp cho nhân viên theo hợp đồng | HR_MANAGER |
| FR-25 | Sửa / vô hiệu hóa phụ cấp đã gán | HR_MANAGER |
| FR-26 | Nhân viên xem phụ cấp của bản thân | EMPLOYEE |

### 4.7 Quản lý Người phụ thuộc

| Mã | Yêu cầu | Vai trò |
|----|---------|---------|
| FR-27 | Thêm, sửa, vô hiệu hóa người phụ thuộc của nhân viên | HR_MANAGER |
| FR-28 | Nhân viên xem người phụ thuộc của bản thân | EMPLOYEE |

---

## 5. Yêu cầu phi chức năng

| Mã | Loại | Mô tả |
|----|------|-------|
| NFR-01 | Bảo mật | Mật khẩu lưu dạng hash BCrypt, không lưu plaintext |
| NFR-02 | Bảo mật | EMPLOYEE không truy cập được trang quản lý HR |
| NFR-03 | Giao diện | Responsive với Bootstrap 5, dùng được trên desktop |
| NFR-04 | Dữ liệu | Không xóa cứng — dùng soft delete qua trường `status` |
| NFR-05 | Bảo trì | Code theo mô hình MVC, mỗi tầng có trách nhiệm rõ ràng |

---

## 6. Ràng buộc hệ thống

| Ràng buộc | Giá trị |
|-----------|---------|
| Ngôn ngữ | C# (.NET 10) |
| Framework | ASP.NET Core MVC |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Giao diện | Bootstrap 5, jQuery |
| Xác thực | ASP.NET Core Cookie Authentication |

---

## 7. Bảng chú giải

| Thuật ngữ | Giải thích |
|-----------|-----------|
| CRUD | Create, Read, Update, Delete — 4 thao tác cơ bản |
| Soft delete | Đặt `status = INACTIVE` thay vì xóa khỏi DB |
| Role | Vai trò phân quyền (HR_MANAGER / EMPLOYEE) |
| MVC | Model-View-Controller — mô hình tổ chức code |

---

**END OF DOCUMENT**
