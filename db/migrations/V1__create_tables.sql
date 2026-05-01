-- 1. Create Sequences
IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'seq_employee_code')
BEGIN
    CREATE SEQUENCE seq_employee_code
        START WITH 1
        INCREMENT BY 1
        NO CYCLE;
END

IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'seq_contract_number')
BEGIN
    CREATE SEQUENCE seq_contract_number
        START WITH 1
        INCREMENT BY 1
        NO CYCLE;
END

-- 2. Department Table
IF OBJECT_ID('department', 'U') IS NULL
BEGIN
    CREATE TABLE department (
        id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        code        VARCHAR(20)      NOT NULL,
        name        NVARCHAR(100)    NOT NULL,
        description NVARCHAR(MAX),
        parent_id   UNIQUEIDENTIFIER,
        status      VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by  NVARCHAR(100)    NOT NULL,
        updated_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by  NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_department        PRIMARY KEY (id),
        CONSTRAINT uq_department_code   UNIQUE (code),
        CONSTRAINT chk_department_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
    );
    CREATE INDEX idx_department_code   ON department (code);
    CREATE INDEX idx_department_parent ON department (parent_id);
END

-- 3. Position Table
IF OBJECT_ID('position', 'U') IS NULL
BEGIN
    CREATE TABLE position (
        id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        code        VARCHAR(20)      NOT NULL,
        name        NVARCHAR(100)    NOT NULL,
        description NVARCHAR(MAX),
        level       VARCHAR(20)      NOT NULL DEFAULT 'JUNIOR',
        status      VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by  NVARCHAR(100)    NOT NULL,
        updated_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by  NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_position         PRIMARY KEY (id),
        CONSTRAINT uq_position_code    UNIQUE (code),
        CONSTRAINT chk_position_level  CHECK (level  IN ('JUNIOR', 'MIDDLE', 'SENIOR')),
        CONSTRAINT chk_position_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
    );
    CREATE INDEX idx_position_code ON position (code);
END

-- 4. Employee Table
IF OBJECT_ID('employee', 'U') IS NULL
BEGIN
    CREATE TABLE employee (
        id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        code        VARCHAR(20)      NOT NULL,
        name        NVARCHAR(100)    NOT NULL,
        dob         DATE             NOT NULL,
        gender      VARCHAR(10)      NOT NULL,
        id_card     VARCHAR(12)      NOT NULL,
        email       VARCHAR(100)     NOT NULL,
        phone       VARCHAR(15),
        address     NVARCHAR(MAX),
        dept_id     UNIQUEIDENTIFIER NOT NULL,
        position_id UNIQUEIDENTIFIER NOT NULL,
        hire_date   DATE             NOT NULL,
        status      VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by  NVARCHAR(100)    NOT NULL,
        updated_at  DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by  NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_employee         PRIMARY KEY (id),
        CONSTRAINT uq_employee_code    UNIQUE (code),
        CONSTRAINT uq_employee_id_card UNIQUE (id_card),
        CONSTRAINT uq_employee_email   UNIQUE (email),
        CONSTRAINT chk_employee_gender CHECK (gender IN ('MALE', 'FEMALE', 'OTHER')),
        CONSTRAINT chk_employee_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
    );
    CREATE INDEX idx_employee_code   ON employee (code);
    CREATE INDEX idx_employee_email  ON employee (email);
    CREATE INDEX idx_employee_dept   ON employee (dept_id);
    CREATE INDEX idx_employee_status ON employee (status);
END

-- 5. Users Table
IF OBJECT_ID('users', 'U') IS NULL
BEGIN
    CREATE TABLE users (
        id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        username        VARCHAR(50)      NOT NULL,
        password_hash   VARCHAR(255)     NOT NULL,
        role            VARCHAR(30)      NOT NULL,
        employee_id     UNIQUEIDENTIFIER,
        status          VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        last_login      DATETIME2,
        failed_attempts INT              NOT NULL DEFAULT 0,
        locked_until    DATETIME2,
        created_at      DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_at      DATETIME2        NOT NULL DEFAULT GETDATE(),

        CONSTRAINT pk_users          PRIMARY KEY (id),
        CONSTRAINT uq_users_username UNIQUE (username),
        CONSTRAINT chk_users_role    CHECK (role   IN ('HR_MANAGER', 'EMPLOYEE')),
        CONSTRAINT chk_users_status  CHECK (status IN ('ACTIVE', 'INACTIVE', 'LOCKED'))
    );
    CREATE INDEX idx_users_username ON users (username);
END

-- 6. Contract Table
IF OBJECT_ID('contract', 'U') IS NULL
BEGIN
    CREATE TABLE contract (
        id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        emp_id          UNIQUEIDENTIFIER NOT NULL,
        contract_number VARCHAR(50)      NOT NULL,
        contract_type   VARCHAR(30)      NOT NULL,
        start_date      DATE             NOT NULL,
        end_date        DATE,
        base_salary     DECIMAL(18,2)    NOT NULL,
        offer_salary    DECIMAL(18,2)    NOT NULL,
        salary_type     VARCHAR(10)      NOT NULL DEFAULT 'GROSS',
        terms           NVARCHAR(MAX),
        file_url        VARCHAR(500),
        status          VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at      DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by      NVARCHAR(100)    NOT NULL,
        updated_at      DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by      NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_contract              PRIMARY KEY (id),
        CONSTRAINT uq_contract_emp_start    UNIQUE (emp_id, start_date),
        CONSTRAINT chk_contract_type        CHECK (contract_type IN ('PROBATION', 'OFFICIAL', 'SEASONAL')),
        CONSTRAINT chk_contract_salary_type CHECK (salary_type   IN ('GROSS', 'NET')),
        CONSTRAINT chk_contract_status      CHECK (status        IN ('ACTIVE', 'EXPIRED', 'TERMINATED')),
        CONSTRAINT chk_contract_end_date    CHECK (end_date IS NULL OR end_date >= start_date),
        CONSTRAINT chk_contract_salary      CHECK (base_salary > 0 AND offer_salary > 0)
    );
    CREATE INDEX idx_contract_emp    ON contract (emp_id);
    CREATE INDEX idx_contract_status ON contract (status);
END

-- 7. Employee Dependent Table
IF OBJECT_ID('employee_dependent', 'U') IS NULL
BEGIN
    CREATE TABLE employee_dependent (
        id           UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        emp_id       UNIQUEIDENTIFIER NOT NULL,
        name         NVARCHAR(100)    NOT NULL,
        dob          DATE,
        relationship VARCHAR(50),
        id_card      VARCHAR(12),
        status       VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at   DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by   NVARCHAR(100)    NOT NULL,
        updated_at   DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by   NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_employee_dependent         PRIMARY KEY (id),
        CONSTRAINT chk_dependent_relationship    CHECK (relationship IS NULL OR relationship IN ('SPOUSE', 'CHILD', 'PARENT')),
        CONSTRAINT chk_dependent_status          CHECK (status       IN ('ACTIVE', 'INACTIVE'))
    );
    CREATE INDEX idx_dependent_emp ON employee_dependent (emp_id);
END

-- 8. Allowance Config Table
IF OBJECT_ID('allowance_config', 'U') IS NULL
BEGIN
    CREATE TABLE allowance_config (
        id             UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        code           VARCHAR(50)      NOT NULL,
        name           NVARCHAR(100)    NOT NULL,
        description    NVARCHAR(MAX),
        default_amount DECIMAL(18,2),
        status         VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at     DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by     NVARCHAR(100)    NOT NULL,
        updated_at     DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by     NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_allowance_config      PRIMARY KEY (id),
        CONSTRAINT uq_allowance_config_code UNIQUE (code),
        CONSTRAINT chk_allowance_status     CHECK (status IN ('ACTIVE', 'INACTIVE'))
    );
END

-- 9. Employee Allowance Table
IF OBJECT_ID('employee_allowance', 'U') IS NULL
BEGIN
    CREATE TABLE employee_allowance (
        id             UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        emp_id         UNIQUEIDENTIFIER NOT NULL,
        contract_id    UNIQUEIDENTIFIER NOT NULL,
        allowance_id   UNIQUEIDENTIFIER NOT NULL,
        amount         DECIMAL(18,2)    NOT NULL,
        effective_date DATE             NOT NULL,
        end_date       DATE,
        status         VARCHAR(20)      NOT NULL DEFAULT 'ACTIVE',
        created_at     DATETIME2        NOT NULL DEFAULT GETDATE(),
        created_by     NVARCHAR(100)    NOT NULL,
        updated_at     DATETIME2        NOT NULL DEFAULT GETDATE(),
        updated_by     NVARCHAR(100)    NOT NULL,

        CONSTRAINT pk_employee_allowance     PRIMARY KEY (id),
        CONSTRAINT chk_emp_allowance_amount  CHECK (amount > 0),
        CONSTRAINT chk_emp_allowance_status  CHECK (status IN ('ACTIVE', 'INACTIVE'))
    );
    CREATE INDEX idx_emp_allowance_emp      ON employee_allowance (emp_id);
    CREATE INDEX idx_emp_allowance_contract ON employee_allowance (contract_id);
END
