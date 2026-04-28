-- Tài khoản admin mặc định — password: Admin@123
-- Hash BCrypt cost=12, sinh bằng BCrypt.Net.BCrypt.HashPassword("Admin@123", 12)
INSERT INTO users (id, username, password_hash, role, employee_id, status, created_at, updated_at)
VALUES (
    gen_random_uuid(),
    'admin',
    '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm',
    'HR_MANAGER',
    NULL,
    'ACTIVE',
    CURRENT_TIMESTAMP,
    CURRENT_TIMESTAMP
);
