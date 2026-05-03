# Test Execution Record — HRM System

| Thuộc tính | Giá trị |
|------------|---------|
| **Phiên bản** | 1.0 |
| **Ngày thực hiện** | 2026-04-28 |
| **Người thực hiện** | Nhóm 29K1 |
| **Môi trường** | localhost:5173 / PostgreSQL local |
| **Build** | dotnet run — branch main |

**Ký hiệu:** ✅ PASS | ❌ FAIL | ⏭ SKIP | 🔄 BLOCK

---

## Module 1 — Đăng nhập / Đăng xuất

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-001 | Đăng nhập thành công — HR_MANAGER | P0 | | | |
| TC-002 | Đăng nhập thành công — EMPLOYEE | P0 | | | |
| TC-003 | Đăng nhập sai mật khẩu | P1 | | | |
| TC-004 | Đăng xuất | P0 | | | |
| TC-005 | EMPLOYEE không vào được trang HR | P0 | | | |

---

## Module 2 — Phòng ban

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-006 | Thêm phòng ban hợp lệ | P1 | | | |
| TC-007 | Thêm phòng ban mã trùng | P1 | | | |
| TC-008 | Sửa phòng ban | P1 | | | |
| TC-009 | Vô hiệu hóa phòng ban | P1 | | | |

---

## Module 3 — Chức vụ

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-010 | Thêm chức vụ hợp lệ | P1 | | | |
| TC-021 | Thêm chức vụ mã trùng | P1 | | | |
| TC-022 | Sửa chức vụ | P1 | | | |
| TC-023 | Vô hiệu hóa chức vụ | P1 | | | |

---

## Module 4 — Nhân viên

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-011 | Xem danh sách + lọc | P1 | | | |
| TC-012 | Thêm nhân viên hợp lệ | P1 | | | |
| TC-013 | Thêm nhân viên email trùng | P1 | | | |
| TC-014 | Sửa nhân viên | P1 | | | |
| TC-015 | Vô hiệu hóa nhân viên | P1 | | | |
| TC-016 | EMPLOYEE xem hồ sơ bản thân | P1 | | | |

---

## Module 5 — Hợp đồng

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-017 | Thêm hợp đồng hợp lệ | P1 | | | |
| TC-018 | Sửa hợp đồng | P1 | | | |
| TC-019 | EMPLOYEE xem HĐ của mình | P1 | | | |
| TC-020 | EMPLOYEE không xem HĐ NV khác | P0 | | | |
| TC-024 | Thêm HĐ ngày bắt đầu trùng | P1 | | | |
| TC-025 | Chấm dứt hợp đồng | P1 | | | |

---

## Module 6 — Phụ cấp (AllowanceConfig)

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-026 | Thêm loại phụ cấp hợp lệ | P1 | | | |
| TC-027 | Thêm loại phụ cấp mã trùng | P1 | | | |
| TC-028 | Gán phụ cấp cho nhân viên | P1 | | | |
| TC-029 | EMPLOYEE xem phụ cấp của mình | P1 | | | |

---

## Module 7 — Người phụ thuộc

| Mã | Tên | Mức | Kết quả thực tế | Trạng thái | Bug # |
|----|-----|-----|----------------|-----------|-------|
| TC-030 | Thêm người phụ thuộc hợp lệ | P1 | | | |
| TC-031 | Xóa mềm người phụ thuộc | P1 | | | |

---

## Tổng kết

| Module | Tổng | ✅ PASS | ❌ FAIL | ⏭ SKIP | 🔄 BLOCK |
|--------|------|--------|--------|--------|---------|
| Đăng nhập / Đăng xuất | 5 | | | | |
| Phòng ban | 4 | | | | |
| Chức vụ | 4 | | | | |
| Nhân viên | 6 | | | | |
| Hợp đồng | 6 | | | | |
| Phụ cấp | 4 | | | | |
| Người phụ thuộc | 2 | | | | |
| **TỔNG** | **31** | | | | |

---

## Tiêu chí ra (Exit Criteria)

- [ ] 100% TC P0 PASS (TC-001, TC-002, TC-004, TC-005, TC-020)
- [ ] ≥ 90% TC P1 PASS (≥ 24/26 TC)
- [ ] Không còn bug Critical/Blocker chưa xử lý
