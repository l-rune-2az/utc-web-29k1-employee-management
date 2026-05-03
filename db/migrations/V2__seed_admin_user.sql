IF NOT EXISTS (SELECT 1 FROM users WHERE username = 'admin')
INSERT INTO users (id, username, password_hash, role, employee_id, status, created_at, updated_at)
VALUES (
    NEWID(),
    'admin',
    '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm',
    'HR_MANAGER',
    NULL,
    'ACTIVE',
    GETUTCDATE(),
    GETUTCDATE()
);
