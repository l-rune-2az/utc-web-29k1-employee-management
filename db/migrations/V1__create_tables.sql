IF OBJECT_ID('department', 'U') IS NULL
CREATE TABLE department (
    id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    code        NVARCHAR(20)     NOT NULL,
    name        NVARCHAR(100)    NOT NULL,
    description NVARCHAR(MAX),
    parent_id   UNIQUEIDENTIFIER,
    status      NVARCHAR(20)     NOT NULL DEFAULT 'ACTIVE',
    created_at  DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by  NVARCHAR(100)    NOT NULL,
    updated_at  DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    updated_by  NVARCHAR(100)    NOT NULL,

    CONSTRAINT pk_department        PRIMARY KEY (id),
    CONSTRAINT uq_department_code   UNIQUE (code),
    CONSTRAINT chk_department_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

IF OBJECT_ID('position', 'U') IS NULL
CREATE TABLE position (
    id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    code        NVARCHAR(20)     NOT NULL,
    name        NVARCHAR(100)    NOT NULL,
    description NVARCHAR(MAX),
    level       NVARCHAR(20)     NOT NULL DEFAULT 'JUNIOR',
    status      NVARCHAR(20)     NOT NULL DEFAULT 'ACTIVE',
    created_at  DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by  NVARCHAR(100)    NOT NULL,
    updated_at  DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    updated_by  NVARCHAR(100)    NOT NULL,

    CONSTRAINT pk_position         PRIMARY KEY (id),
    CONSTRAINT uq_position_code    UNIQUE (code),
    CONSTRAINT chk_position_level  CHECK (level  IN ('JUNIOR', 'MIDDLE', 'SENIOR')),
    CONSTRAINT chk_position_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

IF OBJECT_ID('employee', 'U') IS NULL
CREATE TABLE employee (
    id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    code        NVARCHAR(20)     NOT NULL,
    name        NVARCHAR(100)    NOT NULL,
    dob         DATE,
    gender      NVARCHAR(10),
    id_card     NVARCHAR(20),
    email       NVARCHAR(150)    NOT NULL,
    phone       NVARCHAR(20),
    address     NVARCHAR(MAX),
    dept_id     UNIQUEIDENTIFIER,
    position_id UNIQUEIDENTIFIER,
    hire_date   DATE,
    status      NVARCHAR(10)     NOT NULL DEFAULT 'ACTIVE',
    created_at  DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by  NVARCHAR(100),
    updated_at  DATETIME2,
    updated_by  NVARCHAR(100),

    CONSTRAINT pk_employee         PRIMARY KEY (id),
    CONSTRAINT uq_employee_code    UNIQUE (code),
    CONSTRAINT uq_employee_email   UNIQUE (email),
    CONSTRAINT uq_employee_id_card UNIQUE (id_card),
    CONSTRAINT chk_employee_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

IF OBJECT_ID('users', 'U') IS NULL
CREATE TABLE users (
    id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    employee_id     UNIQUEIDENTIFIER,
    username        NVARCHAR(50)     NOT NULL,
    password_hash   NVARCHAR(255)    NOT NULL,
    role            NVARCHAR(20)     NOT NULL DEFAULT 'EMPLOYEE',
    status          NVARCHAR(10)     NOT NULL DEFAULT 'ACTIVE',
    failed_attempts INT              NOT NULL DEFAULT 0,
    locked_until    DATETIME2,
    last_login      DATETIME2,
    created_at      DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    updated_at      DATETIME2,

    CONSTRAINT pk_users          PRIMARY KEY (id),
    CONSTRAINT uq_users_username UNIQUE (username),
    CONSTRAINT chk_users_role    CHECK (role   IN ('HR_MANAGER', 'EMPLOYEE')),
    CONSTRAINT chk_users_status  CHECK (status IN ('ACTIVE', 'INACTIVE', 'LOCKED'))
);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'uix_users_employee' AND object_id = OBJECT_ID('users'))
    CREATE UNIQUE INDEX uix_users_employee ON users(employee_id) WHERE employee_id IS NOT NULL;

IF OBJECT_ID('contract', 'U') IS NULL
CREATE TABLE contract (
    id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    emp_id          UNIQUEIDENTIFIER NOT NULL,
    contract_number NVARCHAR(50)     NOT NULL,
    contract_type   NVARCHAR(20)     NOT NULL DEFAULT 'OFFICIAL',
    start_date      DATE             NOT NULL,
    end_date        DATE,
    base_salary     DECIMAL(15,2)    NOT NULL,
    offer_salary    DECIMAL(15,2)    NOT NULL,
    salary_type     NVARCHAR(10)     NOT NULL DEFAULT 'GROSS',
    terms           NVARCHAR(MAX),
    file_url        NVARCHAR(500),
    status          NVARCHAR(20)     NOT NULL DEFAULT 'ACTIVE',
    created_at      DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by      NVARCHAR(100),
    updated_at      DATETIME2,
    updated_by      NVARCHAR(100),

    CONSTRAINT pk_contract              PRIMARY KEY (id),
    CONSTRAINT uq_contract_number       UNIQUE (contract_number),
    CONSTRAINT uq_contract_emp_start    UNIQUE (emp_id, start_date),
    CONSTRAINT chk_contract_type        CHECK (contract_type IN ('PROBATION', 'OFFICIAL', 'SEASONAL')),
    CONSTRAINT chk_contract_salary_type CHECK (salary_type   IN ('GROSS', 'NET')),
    CONSTRAINT chk_contract_status      CHECK (status        IN ('ACTIVE', 'EXPIRED', 'TERMINATED')),
    CONSTRAINT chk_contract_salary      CHECK (base_salary > 0 AND offer_salary > 0)
);

IF OBJECT_ID('employee_dependent', 'U') IS NULL
CREATE TABLE employee_dependent (
    id           UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    emp_id       UNIQUEIDENTIFIER NOT NULL,
    name         NVARCHAR(100)    NOT NULL,
    dob          DATE,
    relationship NVARCHAR(20),
    id_card      NVARCHAR(20),
    status       NVARCHAR(10)     NOT NULL DEFAULT 'ACTIVE',
    created_at   DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by   NVARCHAR(100),
    updated_at   DATETIME2,
    updated_by   NVARCHAR(100),

    CONSTRAINT pk_employee_dependent      PRIMARY KEY (id),
    CONSTRAINT chk_dependent_relationship CHECK (relationship IS NULL OR relationship IN ('SPOUSE', 'CHILD', 'PARENT')),
    CONSTRAINT chk_dependent_status       CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

IF OBJECT_ID('allowance_config', 'U') IS NULL
CREATE TABLE allowance_config (
    id             UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    code           NVARCHAR(20)     NOT NULL,
    name           NVARCHAR(100)    NOT NULL,
    description    NVARCHAR(MAX),
    default_amount DECIMAL(15,2),
    status         NVARCHAR(10)     NOT NULL DEFAULT 'ACTIVE',
    created_at     DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by     NVARCHAR(100),
    updated_at     DATETIME2,
    updated_by     NVARCHAR(100),

    CONSTRAINT pk_allowance_config      PRIMARY KEY (id),
    CONSTRAINT uq_allowance_config_code UNIQUE (code),
    CONSTRAINT chk_allowance_status     CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

IF OBJECT_ID('employee_allowance', 'U') IS NULL
CREATE TABLE employee_allowance (
    id             UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    emp_id         UNIQUEIDENTIFIER NOT NULL,
    contract_id    UNIQUEIDENTIFIER NOT NULL,
    allowance_id   UNIQUEIDENTIFIER NOT NULL,
    amount         DECIMAL(15,2)    NOT NULL,
    effective_date DATE,
    end_date       DATE,
    status         NVARCHAR(10)     NOT NULL DEFAULT 'ACTIVE',
    created_at     DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    created_by     NVARCHAR(100),
    updated_at     DATETIME2,
    updated_by     NVARCHAR(100),

    CONSTRAINT pk_employee_allowance    PRIMARY KEY (id),
    CONSTRAINT chk_emp_allowance_amount CHECK (amount > 0),
    CONSTRAINT chk_emp_allowance_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);
