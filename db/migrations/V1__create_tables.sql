CREATE SEQUENCE IF NOT EXISTS seq_employee_code
    START WITH 1
    INCREMENT BY 1
    NO CYCLE;

CREATE SEQUENCE IF NOT EXISTS seq_contract_number
    START WITH 1
    INCREMENT BY 1
    NO CYCLE;

COMMENT ON SEQUENCE seq_employee_code   IS 'Cấp số nguyên tăng dần cho mã nhân viên. Ứng dụng tự ghép ngày + nextval để tạo format (VD: NV260428001).';
COMMENT ON SEQUENCE seq_contract_number IS 'Cấp số nguyên tăng dần cho số hợp đồng. Ứng dụng tự ghép năm + nextval để tạo format (VD: HD-2026-001).';

CREATE TABLE IF NOT EXISTS department (
    id          UUID         NOT NULL DEFAULT gen_random_uuid(),
    code        VARCHAR(20)  NOT NULL,
    name        VARCHAR(100) NOT NULL,
    description TEXT,
    parent_id   UUID,
    status      VARCHAR(20)  NOT NULL DEFAULT 'ACTIVE',
    created_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by  VARCHAR(100) NOT NULL,
    updated_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by  VARCHAR(100) NOT NULL,

    CONSTRAINT pk_department        PRIMARY KEY (id),
    CONSTRAINT uq_department_code   UNIQUE (code),
    CONSTRAINT chk_department_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

CREATE INDEX IF NOT EXISTS idx_department_code   ON department (code);
CREATE INDEX IF NOT EXISTS idx_department_parent ON department (parent_id);

COMMENT ON TABLE  department            IS 'Phòng ban tổ chức. Hỗ trợ phân cấp cha-con qua parent_id.';
COMMENT ON COLUMN department.id         IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN department.code       IS 'Mã phòng ban, duy nhất. VD: DEPT-IT, DEPT-HR';
COMMENT ON COLUMN department.name       IS 'Tên phòng ban';
COMMENT ON COLUMN department.description IS 'Mô tả chi tiết, có thể để trống';
COMMENT ON COLUMN department.parent_id  IS 'UUID phòng ban cha. NULL = cấp gốc';
COMMENT ON COLUMN department.status     IS 'Trạng thái: ACTIVE | INACTIVE';
COMMENT ON COLUMN department.created_at IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN department.created_by IS 'Username người tạo';
COMMENT ON COLUMN department.updated_at IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN department.updated_by IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS position (
    id          UUID         NOT NULL DEFAULT gen_random_uuid(),
    code        VARCHAR(20)  NOT NULL,
    name        VARCHAR(100) NOT NULL,
    description TEXT,
    level       VARCHAR(20)  NOT NULL DEFAULT 'JUNIOR',
    status      VARCHAR(20)  NOT NULL DEFAULT 'ACTIVE',
    created_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by  VARCHAR(100) NOT NULL,
    updated_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by  VARCHAR(100) NOT NULL,

    CONSTRAINT pk_position         PRIMARY KEY (id),
    CONSTRAINT uq_position_code    UNIQUE (code),
    CONSTRAINT chk_position_level  CHECK (level  IN ('JUNIOR', 'MIDDLE', 'SENIOR')),
    CONSTRAINT chk_position_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

CREATE INDEX IF NOT EXISTS idx_position_code ON position (code);

COMMENT ON TABLE  position            IS 'Danh mục chức vụ trong tổ chức.';
COMMENT ON COLUMN position.id         IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN position.code       IS 'Mã chức vụ, duy nhất. VD: POS-DEV, POS-PM';
COMMENT ON COLUMN position.name       IS 'Tên chức vụ';
COMMENT ON COLUMN position.description IS 'Mô tả chi tiết, có thể để trống';
COMMENT ON COLUMN position.level      IS 'Cấp độ kinh nghiệm: JUNIOR | MIDDLE | SENIOR';
COMMENT ON COLUMN position.status     IS 'Trạng thái: ACTIVE | INACTIVE';
COMMENT ON COLUMN position.created_at IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN position.created_by IS 'Username người tạo';
COMMENT ON COLUMN position.updated_at IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN position.updated_by IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS employee (
    id          UUID         NOT NULL DEFAULT gen_random_uuid(),
    code        VARCHAR(20)  NOT NULL,
    name        VARCHAR(100) NOT NULL,
    dob         DATE         NOT NULL,
    gender      VARCHAR(10)  NOT NULL,
    id_card     VARCHAR(12)  NOT NULL,
    email       VARCHAR(100) NOT NULL,
    phone       VARCHAR(15),
    address     TEXT,
    dept_id     UUID         NOT NULL,
    position_id UUID         NOT NULL,
    hire_date   DATE         NOT NULL,
    status      VARCHAR(20)  NOT NULL DEFAULT 'ACTIVE',
    created_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by  VARCHAR(100) NOT NULL,
    updated_at  TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by  VARCHAR(100) NOT NULL,

    CONSTRAINT pk_employee         PRIMARY KEY (id),
    CONSTRAINT uq_employee_code    UNIQUE (code),
    CONSTRAINT uq_employee_id_card UNIQUE (id_card),
    CONSTRAINT uq_employee_email   UNIQUE (email),
    CONSTRAINT chk_employee_gender CHECK (gender IN ('MALE', 'FEMALE', 'OTHER')),
    CONSTRAINT chk_employee_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

CREATE INDEX IF NOT EXISTS idx_employee_code   ON employee (code);
CREATE INDEX IF NOT EXISTS idx_employee_email  ON employee (email);
CREATE INDEX IF NOT EXISTS idx_employee_dept   ON employee (dept_id);
CREATE INDEX IF NOT EXISTS idx_employee_status ON employee (status);

COMMENT ON TABLE  employee              IS 'Nhân viên — bảng trung tâm của module HRM. Các module khác tham chiếu vào đây.';
COMMENT ON COLUMN employee.id           IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN employee.code         IS 'Mã nhân viên tự sinh, format NVYYMMDD###. VD: NV260428001';
COMMENT ON COLUMN employee.name         IS 'Họ và tên đầy đủ';
COMMENT ON COLUMN employee.dob          IS 'Ngày sinh';
COMMENT ON COLUMN employee.gender       IS 'Giới tính: MALE | FEMALE | OTHER';
COMMENT ON COLUMN employee.id_card      IS 'Số CMND/CCCD 12 chữ số, duy nhất toàn hệ thống';
COMMENT ON COLUMN employee.email        IS 'Email, duy nhất toàn hệ thống';
COMMENT ON COLUMN employee.phone        IS 'Số điện thoại, có thể để trống';
COMMENT ON COLUMN employee.address      IS 'Địa chỉ thường trú, có thể để trống';
COMMENT ON COLUMN employee.dept_id      IS 'UUID phòng ban hiện tại (tham chiếu department.id)';
COMMENT ON COLUMN employee.position_id  IS 'UUID chức vụ hiện tại (tham chiếu position.id)';
COMMENT ON COLUMN employee.hire_date    IS 'Ngày vào làm chính thức';
COMMENT ON COLUMN employee.status       IS 'Trạng thái: ACTIVE | INACTIVE. Không xóa cứng (soft delete)';
COMMENT ON COLUMN employee.created_at   IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN employee.created_by   IS 'Username người tạo';
COMMENT ON COLUMN employee.updated_at   IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN employee.updated_by   IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS users (
    id              UUID         NOT NULL DEFAULT gen_random_uuid(),
    username        VARCHAR(50)  NOT NULL,
    password_hash   VARCHAR(255) NOT NULL,
    role            VARCHAR(30)  NOT NULL,
    employee_id     UUID,
    status          VARCHAR(20)  NOT NULL DEFAULT 'ACTIVE',
    last_login      TIMESTAMPTZ,
    failed_attempts INT          NOT NULL DEFAULT 0,
    locked_until    TIMESTAMPTZ,
    created_at      TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at      TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT pk_users          PRIMARY KEY (id),
    CONSTRAINT uq_users_username UNIQUE (username),
    CONSTRAINT chk_users_role    CHECK (role   IN ('HR_MANAGER', 'EMPLOYEE')),
    CONSTRAINT chk_users_status  CHECK (status IN ('ACTIVE', 'INACTIVE', 'LOCKED'))
);

CREATE INDEX IF NOT EXISTS idx_users_username ON users (username);

COMMENT ON TABLE  users                  IS 'Tài khoản đăng nhập hệ thống, liên kết 1-1 với nhân viên.';
COMMENT ON COLUMN users.id               IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN users.username         IS 'Tên đăng nhập, duy nhất';
COMMENT ON COLUMN users.password_hash    IS 'Mật khẩu đã hash bằng BCrypt cost=12, không lưu plaintext';
COMMENT ON COLUMN users.role             IS 'Vai trò phân quyền: HR_MANAGER | ACCOUNTANT';
COMMENT ON COLUMN users.employee_id      IS 'UUID nhân viên liên kết (tham chiếu employee.id). NULL = tài khoản hệ thống';
COMMENT ON COLUMN users.status           IS 'Trạng thái: ACTIVE | INACTIVE | LOCKED';
COMMENT ON COLUMN users.last_login       IS 'Thời điểm đăng nhập thành công gần nhất';
COMMENT ON COLUMN users.failed_attempts  IS 'Số lần đăng nhập sai liên tiếp. Reset về 0 sau khi đăng nhập thành công';
COMMENT ON COLUMN users.locked_until     IS 'Thời điểm hết hạn khóa tài khoản. NULL = không bị khóa';
COMMENT ON COLUMN users.created_at       IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN users.updated_at       IS 'Thời điểm cập nhật gần nhất';

CREATE TABLE IF NOT EXISTS contract (
    id              UUID          NOT NULL DEFAULT gen_random_uuid(),
    emp_id          UUID          NOT NULL,
    contract_number VARCHAR(50)   NOT NULL,
    contract_type   VARCHAR(30)   NOT NULL,
    start_date      DATE          NOT NULL,
    end_date        DATE,
    base_salary     DECIMAL(15,2) NOT NULL,
    offer_salary    DECIMAL(15,2) NOT NULL,
    salary_type     VARCHAR(10)   NOT NULL DEFAULT 'GROSS',
    terms           TEXT,
    file_url        VARCHAR(500),
    status          VARCHAR(20)   NOT NULL DEFAULT 'ACTIVE',
    created_at      TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by      VARCHAR(100)  NOT NULL,
    updated_at      TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by      VARCHAR(100)  NOT NULL,

    CONSTRAINT pk_contract              PRIMARY KEY (id),
    CONSTRAINT uq_contract_emp_start    UNIQUE (emp_id, start_date),
    CONSTRAINT chk_contract_type        CHECK (contract_type IN ('PROBATION', 'OFFICIAL', 'SEASONAL')),
    CONSTRAINT chk_contract_salary_type CHECK (salary_type   IN ('GROSS', 'NET')),
    CONSTRAINT chk_contract_status      CHECK (status        IN ('ACTIVE', 'EXPIRED', 'TERMINATED')),
    CONSTRAINT chk_contract_end_date    CHECK (end_date IS NULL OR end_date >= start_date),
    CONSTRAINT chk_contract_salary      CHECK (base_salary > 0 AND offer_salary > 0)
);

CREATE INDEX IF NOT EXISTS idx_contract_emp    ON contract (emp_id);
CREATE INDEX IF NOT EXISTS idx_contract_status ON contract (status);

COMMENT ON TABLE  contract                 IS 'Hợp đồng lao động. Một nhân viên có thể có nhiều hợp đồng theo thời gian.';
COMMENT ON COLUMN contract.id              IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN contract.emp_id          IS 'UUID nhân viên ký hợp đồng (tham chiếu employee.id)';
COMMENT ON COLUMN contract.contract_number IS 'Số hợp đồng. VD: HĐ-2026-001';
COMMENT ON COLUMN contract.contract_type   IS 'Loại hợp đồng: PROBATION (thử việc) | OFFICIAL (chính thức) | SEASONAL (thời vụ)';
COMMENT ON COLUMN contract.start_date      IS 'Ngày bắt đầu hiệu lực';
COMMENT ON COLUMN contract.end_date        IS 'Ngày kết thúc. NULL = không xác định thời hạn';
COMMENT ON COLUMN contract.base_salary     IS 'Lương cơ bản (VNĐ), dùng tính BHXH. Phải > 0';
COMMENT ON COLUMN contract.offer_salary    IS 'Lương thỏa thuận (VNĐ), dùng tính lương thực tế. Phải > 0';
COMMENT ON COLUMN contract.salary_type     IS 'Loại lương: GROSS (trước thuế) | NET (sau thuế)';
COMMENT ON COLUMN contract.terms           IS 'Điều khoản đặc biệt của hợp đồng, có thể để trống';
COMMENT ON COLUMN contract.file_url        IS 'Đường dẫn file PDF hợp đồng scan, có thể để trống';
COMMENT ON COLUMN contract.status          IS 'Trạng thái: ACTIVE | EXPIRED (hết hạn) | TERMINATED (chấm dứt)';
COMMENT ON COLUMN contract.created_at      IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN contract.created_by      IS 'Username người tạo';
COMMENT ON COLUMN contract.updated_at      IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN contract.updated_by      IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS employee_dependent (
    id           UUID         NOT NULL DEFAULT gen_random_uuid(),
    emp_id       UUID         NOT NULL,
    name         VARCHAR(100) NOT NULL,
    dob          DATE,
    relationship VARCHAR(50),
    id_card      VARCHAR(12),
    status       VARCHAR(20)  NOT NULL DEFAULT 'ACTIVE',
    created_at   TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by   VARCHAR(100) NOT NULL,
    updated_at   TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by   VARCHAR(100) NOT NULL,

    CONSTRAINT pk_employee_dependent         PRIMARY KEY (id),
    CONSTRAINT chk_dependent_relationship    CHECK (relationship IS NULL OR relationship IN ('SPOUSE', 'CHILD', 'PARENT')),
    CONSTRAINT chk_dependent_status          CHECK (status       IN ('ACTIVE', 'INACTIVE'))
);

CREATE INDEX IF NOT EXISTS idx_dependent_emp ON employee_dependent (emp_id);

COMMENT ON TABLE  employee_dependent              IS 'Người phụ thuộc của nhân viên. Dùng để tính giảm trừ gia cảnh thuế TNCN.';
COMMENT ON COLUMN employee_dependent.id           IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN employee_dependent.emp_id       IS 'UUID nhân viên chủ (tham chiếu employee.id)';
COMMENT ON COLUMN employee_dependent.name         IS 'Họ tên người phụ thuộc';
COMMENT ON COLUMN employee_dependent.dob          IS 'Ngày sinh người phụ thuộc';
COMMENT ON COLUMN employee_dependent.relationship IS 'Quan hệ với nhân viên: SPOUSE (vợ/chồng) | CHILD (con) | PARENT (cha/mẹ)';
COMMENT ON COLUMN employee_dependent.id_card      IS 'Số CMND/CCCD người phụ thuộc, có thể để trống';
COMMENT ON COLUMN employee_dependent.status       IS 'Trạng thái: ACTIVE | INACTIVE';
COMMENT ON COLUMN employee_dependent.created_at   IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN employee_dependent.created_by   IS 'Username người tạo';
COMMENT ON COLUMN employee_dependent.updated_at   IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN employee_dependent.updated_by   IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS allowance_config (
    id             UUID          NOT NULL DEFAULT gen_random_uuid(),
    code           VARCHAR(50)   NOT NULL,
    name           VARCHAR(100)  NOT NULL,
    description    TEXT,
    default_amount DECIMAL(15,2),
    status         VARCHAR(20)   NOT NULL DEFAULT 'ACTIVE',
    created_at     TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by     VARCHAR(100)  NOT NULL,
    updated_at     TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by     VARCHAR(100)  NOT NULL,

    CONSTRAINT pk_allowance_config      PRIMARY KEY (id),
    CONSTRAINT uq_allowance_config_code UNIQUE (code),
    CONSTRAINT chk_allowance_status     CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

COMMENT ON TABLE  allowance_config                IS 'Danh mục loại phụ cấp. HR định nghĩa template tại đây trước khi gán cho nhân viên.';
COMMENT ON COLUMN allowance_config.id             IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN allowance_config.code           IS 'Mã loại phụ cấp, duy nhất. VD: ALW-MEAL, ALW-PHONE, ALW-TRANSPORT';
COMMENT ON COLUMN allowance_config.name           IS 'Tên loại phụ cấp. VD: Phụ cấp ăn trưa';
COMMENT ON COLUMN allowance_config.description    IS 'Mô tả chi tiết, có thể để trống';
COMMENT ON COLUMN allowance_config.default_amount IS 'Số tiền đề xuất mặc định khi gán cho nhân viên. NULL = không có giá trị mặc định';
COMMENT ON COLUMN allowance_config.status         IS 'Trạng thái: ACTIVE | INACTIVE';
COMMENT ON COLUMN allowance_config.created_at     IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN allowance_config.created_by     IS 'Username người tạo';
COMMENT ON COLUMN allowance_config.updated_at     IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN allowance_config.updated_by     IS 'Username người cập nhật';

CREATE TABLE IF NOT EXISTS employee_allowance (
    id             UUID          NOT NULL DEFAULT gen_random_uuid(),
    emp_id         UUID          NOT NULL,
    contract_id    UUID          NOT NULL,
    allowance_id   UUID          NOT NULL,
    amount         DECIMAL(15,2) NOT NULL,
    effective_date DATE          NOT NULL,
    end_date       DATE,
    status         VARCHAR(20)   NOT NULL DEFAULT 'ACTIVE',
    created_at     TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by     VARCHAR(100)  NOT NULL,
    updated_at     TIMESTAMPTZ   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by     VARCHAR(100)  NOT NULL,

    CONSTRAINT pk_employee_allowance     PRIMARY KEY (id),
    CONSTRAINT chk_emp_allowance_amount  CHECK (amount > 0),
    CONSTRAINT chk_emp_allowance_status  CHECK (status IN ('ACTIVE', 'INACTIVE'))
);

CREATE INDEX IF NOT EXISTS idx_emp_allowance_emp      ON employee_allowance (emp_id);
CREATE INDEX IF NOT EXISTS idx_emp_allowance_contract ON employee_allowance (contract_id);

COMMENT ON TABLE  employee_allowance                IS 'Phụ cấp thực tế gán cho nhân viên theo hợp đồng cụ thể.';
COMMENT ON COLUMN employee_allowance.id             IS 'Khóa chính, UUID tự sinh';
COMMENT ON COLUMN employee_allowance.emp_id         IS 'UUID nhân viên được hưởng phụ cấp (tham chiếu employee.id)';
COMMENT ON COLUMN employee_allowance.contract_id    IS 'UUID hợp đồng gắn phụ cấp (tham chiếu contract.id)';
COMMENT ON COLUMN employee_allowance.allowance_id   IS 'UUID loại phụ cấp (tham chiếu allowance_config.id)';
COMMENT ON COLUMN employee_allowance.amount         IS 'Số tiền phụ cấp thực tế (VNĐ). Phải > 0';
COMMENT ON COLUMN employee_allowance.effective_date IS 'Ngày bắt đầu hưởng phụ cấp';
COMMENT ON COLUMN employee_allowance.end_date       IS 'Ngày kết thúc hưởng. NULL = vô thời hạn';
COMMENT ON COLUMN employee_allowance.status         IS 'Trạng thái: ACTIVE | INACTIVE';
COMMENT ON COLUMN employee_allowance.created_at     IS 'Thời điểm tạo bản ghi';
COMMENT ON COLUMN employee_allowance.created_by     IS 'Username người tạo';
COMMENT ON COLUMN employee_allowance.updated_at     IS 'Thời điểm cập nhật gần nhất';
COMMENT ON COLUMN employee_allowance.updated_by     IS 'Username người cập nhật';
