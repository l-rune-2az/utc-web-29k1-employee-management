-- Tài khoản admin mặc định — password: Admin@123
-- Hash BCrypt cost=12, sinh bằng BCrypt.Net.BCrypt.HashPassword("Admin@123", 12)
IF NOT EXISTS (SELECT 1 FROM users WHERE username = 'admin')
BEGIN
    INSERT INTO users (id, username, password_hash, role, employee_id, status, created_at, updated_at)
    VALUES (
        NEWID(),
        'admin',
        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm',
        'HR_MANAGER',
        NULL,
        'ACTIVE',
        GETDATE(),
        GETDATE()
    );
END
