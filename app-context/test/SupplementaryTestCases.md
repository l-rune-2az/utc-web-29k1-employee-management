# Bổ sung Test Cases — HRM System

> Bổ sung cho TestCases.md gốc — các module chưa có TC chi tiết.

---

## Module 3 — Chức vụ (bổ sung TC-021 → TC-023)

### TC-021 — Thêm chức vụ với mã trùng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-09 |
| **Mức độ** | P1 |
| **Điều kiện** | Đã có chức vụ `POS-SALE` |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào Chức vụ → Thêm mới | — |
| 2 | Mã | `POS-SALE` (đã có) |
| 3 | Tên | `Bất kỳ` |
| 4 | Nhấn Lưu | — |

**Kết quả mong đợi:** Lỗi "Mã chức vụ đã tồn tại", không lưu

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-022 — Sửa chức vụ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-10 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Nhấn Sửa chức vụ `POS-SALE` | — |
| 2 | Đổi tên | `Nhân viên Kinh doanh Senior` |
| 3 | Đổi cấp bậc | `MIDDLE` |
| 4 | Nhấn Lưu | — |

**Kết quả mong đợi:** Cập nhật thành công, thông tin mới hiển thị trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-023 — Vô hiệu hóa chức vụ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-11 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Nhấn "Vô hiệu hóa" chức vụ `POS-SALE` → xác nhận |
| 2 | Mở form tạo nhân viên |

**Kết quả mong đợi:**
- Chức vụ chuyển INACTIVE
- Không còn trong dropdown khi tạo nhân viên

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 5 — Hợp đồng (bổ sung TC-024, TC-025)

### TC-024 — Thêm hợp đồng với ngày bắt đầu trùng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-19 |
| **Mức độ** | P1 |
| **Điều kiện** | NV đã có HĐ bắt đầu `2026-04-28` |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Thêm hợp đồng mới cho cùng NV | — |
| 2 | Ngày bắt đầu | `2026-04-28` (trùng) |
| 3 | Nhấn Lưu | — |

**Kết quả mong đợi:** Lỗi "Nhân viên đã có hợp đồng bắt đầu vào ngày này", không lưu

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-025 — Chấm dứt hợp đồng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-21 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Vào danh sách HĐ, chọn HĐ ACTIVE |
| 2 | Nhấn "Chấm dứt" → xác nhận |

**Kết quả mong đợi:** HĐ chuyển sang TERMINATED, không thể sửa

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 6 — Phụ cấp (TC-026 → TC-029)

### TC-026 — Thêm loại phụ cấp hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-23 |
| **Mức độ** | P1 |
| **Điều kiện** | Đăng nhập HR_MANAGER |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào Phụ cấp → Thêm loại phụ cấp | — |
| 2 | Mã | `ALW-MEAL` |
| 3 | Tên | `Phụ cấp ăn trưa` |
| 4 | Mức mặc định | `500,000` |
| 5 | Nhấn Lưu | — |

**Kết quả mong đợi:** Tạo thành công, xuất hiện trong danh sách

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-027 — Thêm loại phụ cấp mã trùng

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-23 |
| **Mức độ** | P1 |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Thêm loại phụ cấp | — |
| 2 | Mã | `ALW-MEAL` (đã có) |
| 3 | Nhấn Lưu | — |

**Kết quả mong đợi:** Lỗi "Mã phụ cấp đã tồn tại", không lưu

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-028 — Gán phụ cấp cho nhân viên

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-25 |
| **Mức độ** | P1 |
| **Điều kiện** | NV có HĐ ACTIVE, đã có loại phụ cấp `ALW-MEAL` |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào trang phụ cấp NV, nhấn Gán | — |
| 2 | Chọn loại phụ cấp | `ALW-MEAL` |
| 3 | Số tiền | `600,000` |
| 4 | Ngày hiệu lực | `2026-04-28` |
| 5 | Nhấn Lưu | — |

**Kết quả mong đợi:** Gán thành công, hiển thị trong danh sách phụ cấp NV

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-029 — EMPLOYEE xem phụ cấp của mình

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-26 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Đăng nhập `nv001` |
| 2 | Vào "Phụ cấp của tôi" |
| 3 | Thử truy cập phụ cấp NV khác qua URL |

**Kết quả mong đợi:**
- Bước 2: Chỉ thấy phụ cấp của `nv001`
- Bước 3: 403 Forbidden

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

## Module 7 — Người phụ thuộc (TC-030, TC-031)

### TC-030 — Thêm người phụ thuộc hợp lệ

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-27 |
| **Mức độ** | P1 |
| **Điều kiện** | Đăng nhập HR_MANAGER, chọn NV Trần Thị C |

| Bước | Hành động | Dữ liệu |
|------|-----------|---------|
| 1 | Vào trang người phụ thuộc NV | — |
| 2 | Họ tên | `Trần Văn A` |
| 3 | Ngày sinh | `1965-03-10` |
| 4 | Quan hệ | `PARENT` |
| 5 | Nhấn Lưu | — |

**Kết quả mong đợi:** Thêm thành công, xuất hiện trong danh sách người phụ thuộc

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

### TC-031 — Vô hiệu hóa người phụ thuộc

| Mục | Nội dung |
|-----|----------|
| **FR** | FR-28 |
| **Mức độ** | P1 |

| Bước | Hành động |
|------|-----------|
| 1 | Nhấn "Vô hiệu hóa" người phụ thuộc `Trần Văn A` → xác nhận |

**Kết quả mong đợi:** Chuyển INACTIVE, không xuất hiện trong danh sách mặc định

**Kết quả thực tế:** _______________ | **Trạng thái:** ___

---

**END OF DOCUMENT**
