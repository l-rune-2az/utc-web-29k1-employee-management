# Test Cases — Hệ thống Quản lý Nhân sự (HRM Module)

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã tài liệu** | TC-HRM-v1.1 |
| **Phiên bản** | 1.1 |
| **Ngày** | 2026-04-28 |

**Ký hiệu:** ✅ PASS | ❌ FAIL | ⏭ SKIP

---

## Module 1 — Đăng nhập / Đăng xuất

### TC-001 — Đăng nhập thành công với HR_MANAGER

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-01, FR-03 |
| **Mức độ** | P0 — Critical |
| **Điều kiện** | Tài khoản `admin / Admin@123` tồn tại, status ACTIVE |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào `http://localhost:5173` | — |
| 2 | Nhập Username | `admin` |
| 3 | Nhập Password | `Admin@123` |
| 4 | Nhấn "Đăng nhập" | — |

**Kết quả mong đợi:**
- Chuyển về Dashboard
- Menu hiển thị: Phòng ban, Chức vụ, Nhân viên, Hợp đồng, Phụ cấp

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-002 — Đăng nhập thành công với EMPLOYEE

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-01, FR-03 |
| **Mức độ** | P0 — Critical |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào trang Login | — |
| 2 | Nhập Username | `nv001` |
| 3 | Nhập Password | `Emp@123` |
| 4 | Nhấn "Đăng nhập" | — |

**Kết quả mong đợi:**
- Chuyển về Dashboard nhân viên
- Menu chỉ hiện: Hồ sơ của tôi, Hợp đồng, Phụ cấp
- Không có menu Phòng ban / Chức vụ / Nhân viên

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-003 — Đăng nhập sai mật khẩu

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-01 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào trang Login | — |
| 2 | Nhập Username | `admin` |
| 3 | Nhập Password sai | `SaiMatKhau` |
| 4 | Nhấn "Đăng nhập" | — |

**Kết quả mong đợi:**
- Ở lại trang Login
- Hiện thông báo lỗi "Tên đăng nhập hoặc mật khẩu không đúng"

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-004 — Đăng xuất

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-02 |
| **Mức độ** | P0 |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập thành công |
| 2 | Nhấn "Đăng xuất" |
| 3 | Nhấn Back trên trình duyệt |

**Kết quả mong đợi:**
- Bước 2: Redirect về trang Login
- Bước 3: Không vào được trang cũ — redirect về Login

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-005 — EMPLOYEE không vào được trang quản lý HR

| Mục | Nội dung |
|-----|----------|
| **FR** | NFR-02 |
| **Mức độ** | P0 — Critical |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập `nv001` (EMPLOYEE) |
| 2 | Gõ trực tiếp URL `/Employee/Index` |
| 3 | Gõ trực tiếp URL `/Department/Index` |

**Kết quả mong đợi:** 403 Forbidden hoặc redirect về trang lỗi

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 2 — Phòng ban

### TC-006 — Thêm phòng ban hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-05 |
| **Mức độ** | P1 |
| **Điều kiện** | Đăng nhập HR_MANAGER |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào Phòng ban → Thêm mới | — |
| 2 | Mã | `DEPT-SALE` |
| 3 | Tên | `Phòng Kinh doanh` |
| 4 | Nhấn Lưu | — |

**Kết quả mong đợi:** Thông báo thành công, `DEPT-SALE` xuất hiện trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-007 — Thêm phòng ban với mã trùng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-05 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Thêm mới phòng ban | — |
| 2 | Mã | `DEPT-IT` (đã có) |
| 3 | Tên | `Tên bất kỳ` |
| 4 | Nhấn Lưu | — |

**Kết quả mong đợi:** Lỗi "Mã phòng ban đã tồn tại", không lưu

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-008 — Sửa phòng ban

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-06 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Nhấn Sửa phòng ban `DEPT-IT` | — |
| 2 | Đổi tên | `Phòng IT & Phần mềm` |
| 3 | Nhấn Lưu | — |

**Kết quả mong đợi:** Tên mới xuất hiện trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-009 — Vô hiệu hóa phòng ban

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-07 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Nhấn "Vô hiệu hóa" → xác nhận |
| 2 | Mở form tạo nhân viên |

**Kết quả mong đợi:**
- Phòng ban chuyển INACTIVE
- Không còn trong dropdown khi tạo nhân viên

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-010 — Thêm chức vụ hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-09 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào Chức vụ → Thêm mới | — |
| 2 | Mã | `POS-SALE` |
| 3 | Tên | `Nhân viên Kinh doanh` |
| 4 | Cấp độ | `JUNIOR` |
| 5 | Nhấn Lưu | — |

**Kết quả mong đợi:** Tạo thành công, xuất hiện trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 3 — Nhân viên

### TC-011 — Xem danh sách nhân viên và lọc

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-12 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào menu Nhân viên | — |
| 2 | Nhập ô tìm kiếm | `nguyen` |
| 3 | Lọc phòng ban | `Phòng IT` |
| 4 | Nhấn Tìm | — |

**Kết quả mong đợi:**
- Bảng hiện Mã NV, Họ tên, Phòng ban, Chức vụ, Trạng thái
- Kết quả lọc đúng với từ khóa và phòng ban

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-012 — Thêm nhân viên hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-14 |
| **Mức độ** | P1 |

**Dữ liệu đầu vào:**

| Trường | Giá trị |
|--------|---------|
| Họ và tên | Trần Thị C |
| Ngày sinh | 2000-05-15 |
| Giới tính | Nữ |
| CMND/CCCD | 045200012345 |
| Email | tranc@company.com |
| Phòng ban | Phòng IT |
| Chức vụ | Lập trình viên |
| Ngày vào làm | 2026-04-28 |

**Kết quả mong đợi:** Lưu thành công, mã NV tự sinh, xuất hiện trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-013 — Thêm nhân viên với email trùng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-14 |
| **Mức độ** | P1 |

| Bước | Dữ liệu |
|------|---------|
| Điền form, email | `tranc@company.com` (đã có) |

**Kết quả mong đợi:** Lỗi "Email đã được sử dụng", không lưu

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-014 — Sửa nhân viên

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-15 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Nhấn Sửa nhân viên `Trần Thị C` | — |
| 2 | Đổi số điện thoại | `0987654321` |
| 3 | Nhấn Lưu | — |

**Kết quả mong đợi:** Cập nhật thành công, số điện thoại mới hiển thị trong chi tiết

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-015 — Vô hiệu hóa nhân viên

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-16 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Nhấn "Vô hiệu hóa" → xác nhận |
| 2 | Tìm kiếm mặc định trong danh sách |

**Kết quả mong đợi:**
- NV chuyển INACTIVE
- Không xuất hiện trong danh sách mặc định (chỉ hiện khi lọc "Tất cả")

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-016 — EMPLOYEE xem hồ sơ cá nhân

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-17 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập `nv001` |
| 2 | Vào "Hồ sơ của tôi" |
| 3 | Thử truy cập `/Employee/Details/{id_khac}` |

**Kết quả mong đợi:**
- Bước 2: Hiển thị đúng thông tin của `nv001`
- Bước 3: 403 Forbidden

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 4 — Hợp đồng

### TC-017 — Thêm hợp đồng hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-19 |
| **Mức độ** | P1 |

**Dữ liệu đầu vào:**

| Trường | Giá trị |
|--------|---------|
| Số HĐ | HĐ-2026-001 |
| Loại HĐ | PROBATION |
| Ngày bắt đầu | 2026-04-28 |
| Lương cơ bản | 4,680,000 |
| Lương thỏa thuận | 12,000,000 |

**Kết quả mong đợi:** Tạo thành công, status = ACTIVE

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-018 — Sửa hợp đồng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-20 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Nhấn Sửa hợp đồng ACTIVE | — |
| 2 | Sửa lương thỏa thuận | `15,000,000` |
| 3 | Nhấn Lưu | — |

**Kết quả mong đợi:** Cập nhật thành công

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-019 — EMPLOYEE xem hợp đồng của mình

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-22 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập `nv001` |
| 2 | Vào "Hợp đồng của tôi" |

**Kết quả mong đợi:** Chỉ thấy HĐ của `nv001`, không có nút Sửa/Xóa

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-020 — EMPLOYEE không xem được HĐ của NV khác

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-22 |
| **Mức độ** | P0 — Critical |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập `nv001` |
| 2 | Truy cập `/Contract/Details/{id_hop_dong_nv_khac}` |

**Kết quả mong đợi:** 403 Forbidden

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Tóm tắt kết quả

| Module | Tổng TC | PASS | FAIL | SKIP |
|--------|---------|------|------|------|
| Đăng nhập / Đăng xuất | 5 | | | |
| Phòng ban | 4 | | | |
| Nhân viên | 5 | | | |
| Hợp đồng | 4 | | | |
| Phụ cấp (tự bổ sung) | 2 | | | |
| **TỔNG** | **20** | | | |

**Ngày thực hiện:** ___  **Người thực hiện:** ___

---

**END OF DOCUMENT**
