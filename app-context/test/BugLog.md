# Bug Log — HRM System

| Thuộc tính | Giá trị |
|------------|---------|
| **Phiên bản** | 1.0 |
| **Dự án** | UTC29K1 HRM System |

**Mức độ:** 🔴 Critical | 🟠 High | 🟡 Medium | 🟢 Low

---

## Danh sách bug

| Bug # | Mức | Module | TC liên quan | Mô tả | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi | Trạng thái | Fix commit |
|-------|-----|--------|-------------|-------|--------------|----------------|-----------------|-----------|-----------|
| BUG-001 | 🔴 | Auth | TC-001 | Hash BCrypt trong V2 sai → không đăng nhập được | 1. Vào login / 2. admin / Admin@123 / 3. Submit | Không có lỗi, không redirect | Redirect về Dashboard | ✅ Fixed | — |
| BUG-002 | 🟠 | Tất cả | TC-006 | `updated_at NOT NULL` lỗi khi tạo mới bất kỳ entity | 1. Login / 2. Tạo phòng ban mới / 3. Submit | Exception 23502 | Lưu thành công | ✅ Fixed | — |

---

## Template thêm bug mới

```
| BUG-XXX | 🔴/🟠/🟡/🟢 | [Module] | TC-0XX | [Mô tả ngắn] | [Bước tái hiện] | [Thực tế] | [Mong đợi] | 🔍 Open | — |
```

**Trạng thái:** 🔍 Open | 🔄 In Progress | ✅ Fixed | ❌ Won't Fix | ⏭ Deferred

---

**Cập nhật lần cuối:** 2026-04-28
