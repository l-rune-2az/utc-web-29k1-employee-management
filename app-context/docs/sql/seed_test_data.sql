-- =============================================================
-- Seed Test Data — HRM System UTC29K1  |  Ngày tạo: 2026-04-28
-- Chạy: psql -U postgres -d hrms -f seed_test_data.sql
--
-- 52 nhân viên, phân bổ hire_date để charts có dữ liệu:
--   Trước T11/25 : 30 NV (base)
--   T11/2025     : +4  → tổng 34
--   T12/2025     : +3  → tổng 37
--   T01/2026     : +4  → tổng 41
--   T02/2026     : +5  → tổng 46
--   T03/2026     : +3  → tổng 49
--   T04/2026     : +3  → tổng 52
--
-- Mật khẩu tất cả: Admin@123
-- =============================================================
BEGIN;

-- ============================================================
-- 1. PHÒNG BAN — 15 bản ghi
-- ============================================================
INSERT INTO department (id, code, name, description, parent_id, status, created_at, created_by, updated_at, updated_by) VALUES
('2ae6b793-6305-41f7-b3c9-cfcad0b624b3','DEPT-CNTT','Công nghệ Thông tin',    'Phát triển và vận hành hệ thống phần mềm',         NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('7ba35778-7a4b-4913-9823-40cfea530c09','DEPT-NS',  'Nhân sự',                 'Quản lý nhân sự và phát triển tổ chức',             NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('cd0edf19-8d02-446d-862d-001a92fad5cb','DEPT-TCKT','Tài chính - Kế toán',     'Quản lý tài chính, kế toán và kiểm toán',           NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','DEPT-KD',  'Kinh doanh',              'Phát triển thị trường và quản lý doanh thu',        NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('4b0f00bf-24a4-4c45-ac93-7fcc88f98244','DEPT-MKT', 'Marketing',               'Xây dựng thương hiệu và truyền thông',              NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('b05be9d2-a355-491a-beb0-19897c0bc114','DEPT-VH',  'Vận hành',                'Quản lý vận hành và hỗ trợ kỹ thuật',              NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('69e0628d-9a1f-44c0-8aa8-64e60e005f17','DEPT-PC',  'Pháp chế',                'Tư vấn pháp lý và quản lý rủi ro',                 NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('0f3a7d4f-cde6-412f-8e24-9a5d5278c74f','DEPT-NC',  'Nghiên cứu & Phát triển', 'Nghiên cứu công nghệ và phát triển sản phẩm',      NULL,                                   'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('804bbfd1-20e3-45dc-b236-f905ae91e77b','DEPT-BE',  'Phát triển Backend',      'Xây dựng API và hệ thống server-side',              '2ae6b793-6305-41f7-b3c9-cfcad0b624b3', 'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('3f977327-552c-44ba-80e7-a5f54d7fe73f','DEPT-FE',  'Phát triển Frontend',     'Xây dựng giao diện người dùng',                     '2ae6b793-6305-41f7-b3c9-cfcad0b624b3', 'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('07929734-11a8-4004-a1d8-30e5e8286b5c','DEPT-QA',  'Kiểm thử Phần mềm',      'Đảm bảo chất lượng sản phẩm phần mềm',             '2ae6b793-6305-41f7-b3c9-cfcad0b624b3', 'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('998342ab-8108-41fb-b8b5-320a6ba67adf','DEPT-TD',  'Tuyển dụng',              'Tuyển dụng và onboarding nhân viên mới',            '7ba35778-7a4b-4913-9823-40cfea530c09', 'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('6696467f-e289-4c9b-88f1-a0b07215dd5a','DEPT-LPL', 'Lương & Phúc lợi',       'Tính lương và quản lý phúc lợi nhân viên',          '7ba35778-7a4b-4913-9823-40cfea530c09', 'INACTIVE',NOW(),'admin',NOW(),'admin'),
('5de1d5c9-f034-415f-a1fd-1ebb0cc70d01','DEPT-DIG', 'Digital Marketing',       'Tiếp thị số, SEO và mạng xã hội',                  '4b0f00bf-24a4-4c45-ac93-7fcc88f98244', 'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('707e32a0-1431-4e29-9913-41a7c2f2cf82','DEPT-B2C', 'Kinh doanh B2C',          'Kinh doanh trực tiếp đến người tiêu dùng cá nhân', '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4', 'INACTIVE',NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 2. CHỨC VỤ — 15 bản ghi
-- ============================================================
INSERT INTO position (id, code, name, description, level, status, created_at, created_by, updated_at, updated_by) VALUES
('39714b85-39ee-4149-b02a-db8e00061c3d','CV-GD-CNTT','Giám đốc Công nghệ (CTO)',       'Lãnh đạo chiến lược và định hướng công nghệ',          'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('9b72d4b2-c90e-4f1f-a954-746729069556','CV-GD-NS',  'Giám đốc Nhân sự (HRD)',         'Lãnh đạo chiến lược quản trị nhân sự toàn công ty',    'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('20c8d889-982b-4b68-b348-11e354a1d0d2','CV-GD-TC',  'Giám đốc Tài chính (CFO)',       'Lãnh đạo chiến lược tài chính và kiểm soát ngân sách', 'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('bddb979c-6f3c-4803-b43b-eedebe193bf5','CV-LTV-BE', 'Lập trình viên Backend',         'Phát triển API, cơ sở dữ liệu và dịch vụ backend',    'JUNIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('b7b277fb-acc2-4398-9d86-44450df1fd51','CV-LTV-BE2','Lập trình viên Backend Cao cấp', 'Thiết kế kiến trúc và phát triển hệ thống backend',    'MIDDLE','ACTIVE',NOW(),'admin',NOW(),'admin'),
('c7427a56-b6df-4431-8f25-2ae748d0db3e','CV-KT-BE',  'Kiến trúc sư Hệ thống',          'Định hướng kiến trúc toàn bộ hệ thống',                'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('62879a63-7152-4d12-be02-927477968125','CV-LTV-FE', 'Lập trình viên Frontend',        'Phát triển giao diện web và trải nghiệm người dùng',   'JUNIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('9b6ded8c-387d-4e84-894b-d31f4396c7db','CV-LTV-FE2','Lập trình viên Frontend Cao cấp','Thiết kế và phát triển UI/UX phức tạp',               'MIDDLE','ACTIVE',NOW(),'admin',NOW(),'admin'),
('248c0715-fb20-4a7f-8113-07ea69b514a9','CV-KSV-QA', 'Kỹ sư Kiểm thử',                'Viết test case và kiểm thử phần mềm tự động',          'JUNIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('50b5c303-67dd-4739-ae05-96e36960efbb','CV-CNV-NS', 'Chuyên viên Nhân sự',            'Xử lý hồ sơ, chính sách và phát triển nhân viên',     'JUNIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('6e6d9e67-af32-4d8b-b321-c07471b3def2','CV-KTV',    'Kế toán viên',                   'Quản lý sổ sách và lập báo cáo tài chính',             'MIDDLE','ACTIVE',NOW(),'admin',NOW(),'admin'),
('aa848406-e0f9-4457-afc7-af04f222a7d9','CV-NV-KD',  'Nhân viên Kinh doanh',           'Tìm kiếm khách hàng và thực hiện doanh số',            'JUNIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('407599cb-c819-47b5-b478-6b297de8f754','CV-TP-KD',  'Trưởng phòng Kinh doanh',        'Quản lý đội kinh doanh và theo dõi KPI doanh thu',     'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin'),
('6d6400d4-c14c-4d69-9cae-005b15619219','CV-LUT',    'Luật sư Tư vấn',                 'Tư vấn và soạn thảo hợp đồng pháp lý',                'MIDDLE','ACTIVE',NOW(),'admin',NOW(),'admin'),
('d7aa572a-721b-465f-8440-6de4a9dae1c9','CV-PM',     'Quản lý Sản phẩm',               'Quản lý roadmap và phát triển sản phẩm',               'SENIOR','ACTIVE',NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 3. NHÂN VIÊN — 52 bản ghi
--    E01-E30: base (hired ≤ 2025-10)
--    E31-E52: tuyển mới trong 6 tháng gần nhất
-- ============================================================
INSERT INTO employee (id, code, name, dob, gender, id_card, email, phone, address, dept_id, position_id, hire_date, status, created_at, created_by, updated_at, updated_by) VALUES
-- ── Base employees (E01-E15, giữ nguyên hire_date cũ) ──────────────────────
('e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','NV200115001','Nguyễn Văn An',     '1985-03-15','MALE',  '001085003101','nguyen.van.an@utchrm.vn',     '0901234001','15 Nguyễn Trãi, Q.1, TP.HCM',       '2ae6b793-6305-41f7-b3c9-cfcad0b624b3','39714b85-39ee-4149-b02a-db8e00061c3d','2020-01-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','NV210301001','Trần Thị Bình',     '1988-07-22','FEMALE','001088007102','tran.thi.binh@utchrm.vn',     '0901234002','27 Lê Lợi, Q.1, TP.HCM',            '7ba35778-7a4b-4913-9823-40cfea530c09','9b72d4b2-c90e-4f1f-a954-746729069556','2021-03-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('507e337d-1d62-4b20-a062-ae115929788e','NV190601001','Lê Minh Cường',     '1982-11-10','MALE',  '001082011103','le.minh.cuong@utchrm.vn',     '0901234003','88 Trần Hưng Đạo, Q.5, TP.HCM',     'cd0edf19-8d02-446d-862d-001a92fad5cb','20c8d889-982b-4b68-b348-11e354a1d0d2','2019-06-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('68f21e99-9da1-4f8c-bbf5-1ad3f53d5e52','NV220214001','Phạm Thị Dung',     '1995-04-18','FEMALE','001095004104','pham.thi.dung@utchrm.vn',     '0901234004','42 Điện Biên Phủ, Q.3, TP.HCM',     '804bbfd1-20e3-45dc-b236-f905ae91e77b','b7b277fb-acc2-4398-9d86-44450df1fd51','2022-02-14','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('53bbd8d8-b6d5-4539-b02e-10465eec6c7d','NV230701001','Hoàng Văn Em',      '1999-08-25','MALE',  '001099008105','hoang.van.em@utchrm.vn',      '0901234005','10 CMT8, Q.3, TP.HCM',              '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2023-07-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('844ff40e-48c1-45cf-9169-d51ed5fcfaf2','NV230915001','Ngô Thị Phương',    '1997-12-03','FEMALE','001097012106','ngo.thi.phuong@utchrm.vn',    '0901234006','55 Nguyễn Đình Chiểu, Q.3, TP.HCM', '3f977327-552c-44ba-80e7-a5f54d7fe73f','62879a63-7152-4d12-be02-927477968125','2023-09-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('cd971339-9e6c-4101-a642-085d0869f238','NV231101001','Vũ Quang Giang',    '2000-02-14','MALE',  '001000002107','vu.quang.giang@utchrm.vn',    '0901234007','33 Lý Tự Trọng, Q.1, TP.HCM',       '07929734-11a8-4004-a1d8-30e5e8286b5c','248c0715-fb20-4a7f-8113-07ea69b514a9','2023-11-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('c4126ca1-0383-412e-a48e-645c800b62e1','NV220801001','Đặng Thị Hà',       '1996-09-30','FEMALE','001096009108','dang.thi.ha@utchrm.vn',       '0901234008','78 Pasteur, Q.3, TP.HCM',            '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2022-08-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('8cca6b67-28c9-46ad-adf1-3e53d106de69','NV211015001','Bùi Thanh Ích',     '1993-05-07','MALE',  '001093005109','bui.thanh.ich@utchrm.vn',     '0901234009','66 Nam Kỳ Khởi Nghĩa, Q.3, TP.HCM', 'cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2021-10-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('3dcc3730-836b-43a4-a386-4a1338f8a491','NV240115001','Đinh Thị Kim Khánh','1998-06-20','FEMALE','001098006110','dinh.thi.kim.khanh@utchrm.vn','0901234010','12 Hai Bà Trưng, Q.1, TP.HCM',      '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2024-01-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('36002591-c63b-45f5-8258-212c9542239e','NV200901001','Lý Xuân Long',      '1987-01-08','MALE',  '001087001111','ly.xuan.long@utchrm.vn',      '0901234011','100 Nguyễn Huệ, Q.1, TP.HCM',       '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','407599cb-c819-47b5-b478-6b297de8f754','2020-09-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5a37c376-062a-4355-86c0-924e30c04d3e','NV220401001','Mai Thị Mỹ Linh',   '1991-10-15','FEMALE','001091010112','mai.thi.my.linh@utchrm.vn',   '0901234012','25 Ngô Đức Kế, Q.1, TP.HCM',        '69e0628d-9a1f-44c0-8aa8-64e60e005f17','6d6400d4-c14c-4d69-9cae-005b15619219','2022-04-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('ebe7beba-eba8-4fcf-a216-5980e1101f52','NV210515001','Nguyễn Anh Nhi',    '1989-03-22','OTHER', '001089003113','nguyen.anh.nhi@utchrm.vn',    '0901234013','77 Trần Quốc Thảo, Q.3, TP.HCM',   '0f3a7d4f-cde6-412f-8e24-9a5d5278c74f','d7aa572a-721b-465f-8440-6de4a9dae1c9','2021-05-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('ec8ab77c-1a8a-418f-94de-3ec45b97b170','NV220110001','Trần Ngọc Oanh',    '1994-08-11','FEMALE','001094008114','tran.ngoc.oanh@utchrm.vn',    '0901234014','18 Đinh Tiên Hoàng, Q.BT, TP.HCM', '804bbfd1-20e3-45dc-b236-f905ae91e77b','c7427a56-b6df-4431-8f25-2ae748d0db3e','2022-01-10','INACTIVE',NOW(),'admin',NOW(),'admin'),
('20631750-0afe-47e8-8bbb-5bf8ac5257c5','NV230301001','Phạm Văn Phúc',     '2001-07-19','MALE',  '001001007115','pham.van.phuc@utchrm.vn',     '0901234015','50 Bùi Viện, Q.1, TP.HCM',          '3f977327-552c-44ba-80e7-a5f54d7fe73f','9b6ded8c-387d-4e84-894b-d31f4396c7db','2023-03-01','INACTIVE',NOW(),'admin',NOW(),'admin'),
-- ── Base employees E16-E30 ──────────────────────────────────────────────────
('c9c9eef3-02fe-48cb-bd05-d346adee0579','NV200616001','Trần Văn Hùng',     '1990-04-12','MALE',  '001090004116','tran.van.hung@utchrm.vn',     '0901234016','22 Cách Mạng Tháng 8, Q.3, TP.HCM', '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2020-06-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('88c12f02-940f-4d6e-81a6-e48a2e5274e5','NV201117001','Nguyễn Thị Lan',    '1992-09-20','FEMALE','001092009117','nguyen.thi.lan@utchrm.vn',    '0901234017','9 Ngô Quyền, Q.10, TP.HCM',         '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2020-11-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('a9ec17e8-6c1d-441e-80bc-bfce604b7132','NV210218001','Phạm Quốc Hưng',    '1991-06-08','MALE',  '001091006118','pham.quoc.hung@utchrm.vn',    '0901234018','34 Phan Xích Long, Q.PNhuận, TP.HCM','4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2021-02-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('8b964f54-d9b9-470c-9105-dffd2dcea76e','NV210719001','Lê Thị Thu Hương',  '1993-11-25','FEMALE','001093011119','le.thi.thu.huong@utchrm.vn',  '0901234019','56 Trường Chinh, Q.Tân Bình, TP.HCM','cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2021-07-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('dda5ce6b-5966-4c25-a24e-35d4ea37d55d','NV211020001','Hoàng Văn Minh',    '1988-02-14','MALE',  '001088002120','hoang.van.minh@utchrm.vn',    '0901234020','11 Lý Chính Thắng, Q.3, TP.HCM',    '804bbfd1-20e3-45dc-b236-f905ae91e77b','b7b277fb-acc2-4398-9d86-44450df1fd51','2021-10-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('dbf187e1-7a51-49da-8521-842e63e2ee99','NV220321001','Vũ Thị Hoa',        '1994-07-30','FEMALE','001094007121','vu.thi.hoa@utchrm.vn',        '0901234021','67 Nguyễn Thị Minh Khai, Q.3',      '4b0f00bf-24a4-4c45-ac93-7fcc88f98244','aa848406-e0f9-4457-afc7-af04f222a7d9','2022-03-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5bea127a-4161-43f0-9d1f-d3fca26b3331','NV220622001','Đinh Văn Khánh',    '1997-01-18','MALE',  '001097001122','dinh.van.khanh@utchrm.vn',    '0901234022','14 Đinh Tiên Hoàng, Q.1, TP.HCM',   '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2022-06-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('d5ac5a0b-0e98-4599-b04a-59eb9d97a5e6','NV220923001','Nguyễn Thị Mai',    '1995-05-22','FEMALE','001095005123','nguyen.thi.mai@utchrm.vn',    '0901234023','48 Võ Văn Tần, Q.3, TP.HCM',        '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2022-09-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('a0f06db7-cb07-4bee-ac8f-16186080d66e','NV230124001','Trần Minh Đức',     '1992-08-10','MALE',  '001092008124','tran.minh.duc@utchrm.vn',     '0901234024','20 Bà Huyện Thanh Quan, Q.3',        '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2023-01-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('9066ebd3-7f74-48c5-8a3f-9923cbe7ff14','NV230425001','Bùi Thị Trang',     '1996-03-05','FEMALE','001096003125','bui.thi.trang@utchrm.vn',     '0901234025','37 Huỳnh Khương Ninh, Q.1',          '69e0628d-9a1f-44c0-8aa8-64e60e005f17','6d6400d4-c14c-4d69-9cae-005b15619219','2023-04-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('285c4bfa-f35b-47a0-869d-87e6541d226f','NV230526001','Lý Văn Tuấn',       '1998-10-14','MALE',  '001098010126','ly.van.tuan@utchrm.vn',       '0901234026','5 Nguyễn Cư Trinh, Q.1, TP.HCM',    '3f977327-552c-44ba-80e7-a5f54d7fe73f','62879a63-7152-4d12-be02-927477968125','2023-05-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('31df7a3d-7cef-4167-a786-eca46b6a30ab','NV230827001','Phạm Thị Nhung',    '1997-07-28','FEMALE','001097007127','pham.thi.nhung@utchrm.vn',    '0901234027','29 Cao Thắng, Q.3, TP.HCM',         '07929734-11a8-4004-a1d8-30e5e8286b5c','248c0715-fb20-4a7f-8113-07ea69b514a9','2023-08-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('f936c597-1af1-4381-a7a8-4d47b0835f4d','NV240128001','Ngô Văn Toàn',      '1993-12-03','MALE',  '001093012128','ngo.van.toan@utchrm.vn',      '0901234028','71 Trần Quốc Toản, Q.3, TP.HCM',    'cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2024-01-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('0c377cce-3d75-4c6a-8ebc-355ffd82d141','NV240529001','Đặng Thị Linh',     '1995-09-17','FEMALE','001095009129','dang.thi.linh@utchrm.vn',     '0901234029','8 Đoàn Văn Bơ, Q.4, TP.HCM',        '0f3a7d4f-cde6-412f-8e24-9a5d5278c74f','d7aa572a-721b-465f-8440-6de4a9dae1c9','2024-05-15','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('045b6ede-9430-402b-b00a-4d41f4767fc5','NV240830001','Cao Văn Thành',     '1991-04-09','MALE',  '001091004130','cao.van.thanh@utchrm.vn',     '0901234030','16 Cô Bắc, Q.1, TP.HCM',            '804bbfd1-20e3-45dc-b236-f905ae91e77b','b7b277fb-acc2-4398-9d86-44450df1fd51','2024-08-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 11/2025 — 4 nhân viên mới ────────────────────────────────────────
('28fecfa8-8576-4131-854b-31fb9cef3b35','NV251103001','Lưu Thị Hằng',      '1999-06-11','FEMALE','001099006131','luu.thi.hang@utchrm.vn',      '0901234031','3 Đinh Bộ Lĩnh, Q.BT, TP.HCM',      '3f977327-552c-44ba-80e7-a5f54d7fe73f','62879a63-7152-4d12-be02-927477968125','2025-11-03','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('90eca336-ea16-4836-8525-c546664de0af','NV251110001','Trương Văn Bảo',    '2000-01-25','MALE',  '001000001132','truong.van.bao@utchrm.vn',    '0901234032','44 Xô Viết Nghệ Tĩnh, Q.BT',        '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2025-11-10','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('9c11f3d0-7284-44c4-afe3-9485e7e705de','NV251117001','Phan Văn Dũng',     '1996-11-30','MALE',  '001096011133','phan.van.dung@utchrm.vn',     '0901234033','88 Phan Đăng Lưu, Q.PNhuận',        '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2025-11-17','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('e4b996a4-dc2a-4bb4-9355-abae581bc3db','NV251124001','Đỗ Thị Phúc',       '1998-03-08','FEMALE','001098003134','do.thi.phuc@utchrm.vn',       '0901234034','19 Bùi Đình Túy, Q.BT, TP.HCM',     '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2025-11-24','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 12/2025 — 3 nhân viên mới ────────────────────────────────────────
('6b2ad4af-43e6-4ee6-a182-4283d25fae4b','NV251201001','Lâm Văn Tú',        '2001-08-14','MALE',  '001001008135','lam.van.tu@utchrm.vn',        '0901234035','60 Trần Huy Liệu, Q.PNhuận',        '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2025-12-01','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('6be5ea95-c15c-4083-bd9a-941b0278b024','NV251210001','Võ Thị Ngọc',       '1999-12-20','FEMALE','001099012136','vo.thi.ngoc@utchrm.vn',       '0901234036','12 Nguyễn Kiệm, Q.PNhuận',          '07929734-11a8-4004-a1d8-30e5e8286b5c','248c0715-fb20-4a7f-8113-07ea69b514a9','2025-12-10','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('b3a7763f-20bc-4a25-ae7c-7dc92298019a','NV251222001','Hồ Văn Phong',      '1992-05-03','MALE',  '001092005137','ho.van.phong@utchrm.vn',      '0901234037','25 Cộng Hòa, Q.Tân Bình, TP.HCM',   'cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2025-12-22','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 01/2026 — 4 nhân viên mới ────────────────────────────────────────
('a85aceb6-e514-45ae-841d-ca548cfdb077','NV260105001','Dương Thị Hương',   '1997-02-27','FEMALE','001097002138','duong.thi.huong@utchrm.vn',   '0901234038','33 Hoàng Văn Thụ, Q.Tân Bình',      '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2026-01-05','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('57060aad-a982-4ac3-8946-bb66aa04e362','NV260112001','Trần Quốc Khoa',    '1995-07-16','MALE',  '001095007139','tran.quoc.khoa@utchrm.vn',    '0901234039','77 Lê Văn Sỹ, Q.Tân Bình, TP.HCM',  '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2026-01-12','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('b206b771-4c68-4fda-a7af-557b9a422cec','NV260119001','Nguyễn Thị Thanh',  '2000-10-05','FEMALE','001000010140','nguyen.thi.thanh@utchrm.vn',  '0901234040','5 Trường Sa, Q.Phú Nhuận, TP.HCM',  '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2026-01-19','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('0a133121-0239-4e52-9562-8b32b1ae368f','NV260126001','Lê Văn Tài',        '1993-04-21','MALE',  '001093004141','le.van.tai@utchrm.vn',        '0901234041','9 Đinh Tiên Hoàng, Q.1, TP.HCM',    'cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2026-01-26','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 02/2026 — 5 nhân viên mới ────────────────────────────────────────
('7b7d74f7-bcec-4de4-88ae-213c6ecb653d','NV260202001','Phùng Thị Long',    '1998-08-19','FEMALE','001098008142','phung.thi.long@utchrm.vn',    '0901234042','18 Phạm Ngũ Lão, Q.1, TP.HCM',      '3f977327-552c-44ba-80e7-a5f54d7fe73f','62879a63-7152-4d12-be02-927477968125','2026-02-02','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('f2487fbb-53c2-44ad-83ad-b8d22dbb959d','NV260209001','Trịnh Văn Kim',     '1994-01-07','MALE',  '001094001143','trinh.van.kim@utchrm.vn',     '0901234043','40 Bến Nghé, Q.4, TP.HCM',          '7ba35778-7a4b-4913-9823-40cfea530c09','50b5c303-67dd-4739-ae05-96e36960efbb','2026-02-09','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('7b75874d-901b-453f-a9b7-c270cc7ae2c3','NV260216001','Lê Hoàng Nam',      '1999-05-13','MALE',  '001099005144','le.hoang.nam@utchrm.vn',      '0901234044','62 Đề Thám, Q.1, TP.HCM',           '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2026-02-16','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5c1c34f5-ce7e-4f1d-9564-9add86898ef6','NV260223001','Bùi Thị Minh',      '1996-09-24','FEMALE','001096009145','bui.thi.minh@utchrm.vn',      '0901234045','7 Nguyễn Thái Học, Q.1, TP.HCM',    '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2026-02-23','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('6dfea6dd-3065-4c30-887a-f347e335a2a4','NV260227001','Nguyễn Đức Thịnh',  '2001-03-31','MALE',  '001001003146','nguyen.duc.thinh@utchrm.vn',  '0901234046','51 Nguyễn Gia Thiều, Q.3, TP.HCM',  '07929734-11a8-4004-a1d8-30e5e8286b5c','248c0715-fb20-4a7f-8113-07ea69b514a9','2026-02-27','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 03/2026 — 3 nhân viên mới ────────────────────────────────────────
('6410957f-75ac-4f34-b802-f7858ace137d','NV260303001','Vương Thị Thúy',    '1997-11-02','FEMALE','001097011147','vuong.thi.thuy@utchrm.vn',    '0901234047','28 Ngô Thị Thu Minh, Q.1',          '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2026-03-03','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('855cf7ad-80cb-4074-a231-6133a027b59d','NV260310001','Đinh Minh Hải',     '1990-06-16','MALE',  '001090006148','dinh.minh.hai@utchrm.vn',     '0901234048','14 Trần Quang Khải, Q.1, TP.HCM',   'cd0edf19-8d02-446d-862d-001a92fad5cb','6e6d9e67-af32-4d8b-b321-c07471b3def2','2026-03-10','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('3041bdbe-939a-4865-bcc5-3abde49ed874','NV260324001','Phan Thị Nga',      '1995-08-09','FEMALE','001095008149','phan.thi.nga@utchrm.vn',      '0901234049','6 Võ Thị Sáu, Q.3, TP.HCM',         '0f3a7d4f-cde6-412f-8e24-9a5d5278c74f','d7aa572a-721b-465f-8440-6de4a9dae1c9','2026-03-24','ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- ── Tháng 04/2026 — 3 nhân viên mới ────────────────────────────────────────
('a44424b8-0c44-4f99-be93-b619199e2297','NV260402001','Hoàng Văn Tuấn',    '1993-02-28','MALE',  '001093002150','hoang.van.tuan@utchrm.vn',    '0901234050','39 Lê Thánh Tôn, Q.1, TP.HCM',      '4fb7cc02-6834-4ee9-9a7c-7e5133eba6d4','aa848406-e0f9-4457-afc7-af04f222a7d9','2026-04-02','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('8b7f9063-ec83-4e22-b0e6-b8350b8ac591','NV260407001','Ngô Thị Bích',      '1998-04-15','FEMALE','001098004151','ngo.thi.bich@utchrm.vn',      '0901234051','55 Hai Bà Trưng, Q.1, TP.HCM',      '3f977327-552c-44ba-80e7-a5f54d7fe73f','62879a63-7152-4d12-be02-927477968125','2026-04-07','ACTIVE',  NOW(),'admin',NOW(),'admin'),
('acd53b6d-1766-46b6-9452-b42bfc063469','NV260414001','Đào Văn Bình',      '1996-12-22','MALE',  '001096012152','dao.van.binh@utchrm.vn',      '0901234052','13 Mạc Thị Bưởi, Q.1, TP.HCM',      '804bbfd1-20e3-45dc-b236-f905ae91e77b','bddb979c-6f3c-4803-b43b-eedebe193bf5','2026-04-14','ACTIVE',  NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 4. TÀI KHOẢN — 53 bản ghi (admin từ V2, skip ON CONFLICT)
-- ============================================================
INSERT INTO users (id, username, password_hash, role, employee_id, status, failed_attempts, locked_until, created_at, updated_at) VALUES
('dbfe7376-d800-4ca0-94e0-8e2e4db14480','hr.truong',           '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','HR_MANAGER',NULL,                                   'ACTIVE', 0,NULL,NOW(),NOW()),
('32adbb77-5815-486a-9a61-f386db3b762b','nguyen.van.an',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','ACTIVE', 0,NULL,NOW(),NOW()),
('dc8ca9aa-e47d-4d42-8271-5edd16d427a3','tran.thi.binh',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','ACTIVE', 0,NULL,NOW(),NOW()),
('2c247b54-e736-45ab-9b3a-a51e7bea163d','le.minh.cuong',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','507e337d-1d62-4b20-a062-ae115929788e','ACTIVE', 0,NULL,NOW(),NOW()),
('d1058ad5-7600-4b6a-8a61-e38eb73ddd13','pham.thi.dung',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','68f21e99-9da1-4f8c-bbf5-1ad3f53d5e52','ACTIVE', 0,NULL,NOW(),NOW()),
('edbb8ba2-0230-4231-964e-e55ed84f088d','hoang.van.em',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','53bbd8d8-b6d5-4539-b02e-10465eec6c7d','ACTIVE', 0,NULL,NOW(),NOW()),
('0ca48a32-307a-4e0e-8ce2-9c6c7fef787f','ngo.thi.phuong',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','844ff40e-48c1-45cf-9169-d51ed5fcfaf2','ACTIVE', 0,NULL,NOW(),NOW()),
('6d97c78e-23be-4c0c-87e5-e58387c1e532','vu.quang.giang',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','cd971339-9e6c-4101-a642-085d0869f238','ACTIVE', 0,NULL,NOW(),NOW()),
('ab7a25c5-586b-4208-8642-2626b0f8c806','dang.thi.ha',          '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','c4126ca1-0383-412e-a48e-645c800b62e1','ACTIVE', 0,NULL,NOW(),NOW()),
('0eae4787-2158-4acd-84fe-90569c21f433','bui.thanh.ich',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','8cca6b67-28c9-46ad-adf1-3e53d106de69','ACTIVE', 0,NULL,NOW(),NOW()),
('fce3c11c-c602-48c4-9842-73e85b58f9b4','dinh.thi.kim.khanh',   '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','3dcc3730-836b-43a4-a386-4a1338f8a491','ACTIVE', 0,NULL,NOW(),NOW()),
('9dc31da9-fa80-4952-bd4f-1701360dab96','ly.xuan.long',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','36002591-c63b-45f5-8258-212c9542239e','INACTIVE',0,NULL,NOW(),NOW()),
('4d20c0fe-b99d-4dc9-9591-34cc6ecdf0d7','mai.thi.my.linh',      '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','5a37c376-062a-4355-86c0-924e30c04d3e','ACTIVE', 0,NULL,NOW(),NOW()),
('f4e948ca-8234-4d85-8c94-6b3ce70155e3','nguyen.anh.nhi',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','ebe7beba-eba8-4fcf-a216-5980e1101f52','LOCKED', 5,NOW()+INTERVAL '15 minutes',NOW(),NOW()),
('9950fd6c-44df-41c9-a4ac-27d33f94ef7f','tran.ngoc.oanh',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','ec8ab77c-1a8a-418f-94de-3ec45b97b170','INACTIVE',0,NULL,NOW(),NOW()),
('a2bff2d1-0749-4524-a800-cf56a84f7fb6','pham.van.phuc',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','20631750-0afe-47e8-8bbb-5bf8ac5257c5','INACTIVE',0,NULL,NOW(),NOW()),
('e4cd1352-69ed-4956-b5bf-f4386402f8fe','tran.van.hung',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','c9c9eef3-02fe-48cb-bd05-d346adee0579','ACTIVE', 0,NULL,NOW(),NOW()),
('e3c1178f-72ee-44cc-9e4a-81f5153ba0ac','nguyen.thi.lan',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','88c12f02-940f-4d6e-81a6-e48a2e5274e5','ACTIVE', 0,NULL,NOW(),NOW()),
('cce0e0af-2e66-458c-94bc-79c6c040a6de','pham.quoc.hung',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','a9ec17e8-6c1d-441e-80bc-bfce604b7132','ACTIVE', 0,NULL,NOW(),NOW()),
('5b059770-b594-4f02-9116-07fc4a03c2e3','le.thi.thu.huong',     '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','8b964f54-d9b9-470c-9105-dffd2dcea76e','ACTIVE', 0,NULL,NOW(),NOW()),
('3db79288-c3e6-4ad3-9607-7d3a41b6dd45','hoang.van.minh',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','dda5ce6b-5966-4c25-a24e-35d4ea37d55d','ACTIVE', 0,NULL,NOW(),NOW()),
('ef3153af-e866-4f52-b8ab-2e4caa928f0d','vu.thi.hoa',           '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','dbf187e1-7a51-49da-8521-842e63e2ee99','ACTIVE', 0,NULL,NOW(),NOW()),
('16ac4332-6a8f-4f40-a931-f1d1d2ef86c3','dinh.van.khanh',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','5bea127a-4161-43f0-9d1f-d3fca26b3331','ACTIVE', 0,NULL,NOW(),NOW()),
('51e99479-0ec6-40d8-8fa2-390f385aa817','nguyen.thi.mai',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','d5ac5a0b-0e98-4599-b04a-59eb9d97a5e6','ACTIVE', 0,NULL,NOW(),NOW()),
('8d6e2ab4-0c90-44ff-abc8-cc76b184365c','tran.minh.duc',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','a0f06db7-cb07-4bee-ac8f-16186080d66e','ACTIVE', 0,NULL,NOW(),NOW()),
('dab0730b-0a2f-4cb3-ac0a-1897c7b72dd7','bui.thi.trang',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','9066ebd3-7f74-48c5-8a3f-9923cbe7ff14','ACTIVE', 0,NULL,NOW(),NOW()),
('9f95113d-365c-4328-b203-9008ef6bd773','ly.van.tuan',          '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','285c4bfa-f35b-47a0-869d-87e6541d226f','ACTIVE', 0,NULL,NOW(),NOW()),
('0812f283-fe64-4508-b0bb-30cbc48de513','pham.thi.nhung',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','31df7a3d-7cef-4167-a786-eca46b6a30ab','ACTIVE', 0,NULL,NOW(),NOW()),
('a21f9d0a-b4d7-4a6e-bcb2-adb8138c2393','ngo.van.toan',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','f936c597-1af1-4381-a7a8-4d47b0835f4d','ACTIVE', 0,NULL,NOW(),NOW()),
('7d28f1ff-14f6-4a83-842c-0ffe41e4b507','dang.thi.linh',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','0c377cce-3d75-4c6a-8ebc-355ffd82d141','ACTIVE', 0,NULL,NOW(),NOW()),
('3a6d707b-9007-407a-8c75-02270c280336','cao.van.thanh',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','045b6ede-9430-402b-b00a-4d41f4767fc5','ACTIVE', 0,NULL,NOW(),NOW()),
('98bbfd90-a0d5-499a-877a-43849ece7abd','luu.thi.hang',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','28fecfa8-8576-4131-854b-31fb9cef3b35','ACTIVE', 0,NULL,NOW(),NOW()),
('2372ab95-d538-4de1-9de3-c704bdd53713','truong.van.bao',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','90eca336-ea16-4836-8525-c546664de0af','ACTIVE', 0,NULL,NOW(),NOW()),
('4b6fb5e5-161c-4fcc-8cec-46e51af24165','phan.van.dung',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','9c11f3d0-7284-44c4-afe3-9485e7e705de','ACTIVE', 0,NULL,NOW(),NOW()),
('d755fa5d-ead1-43b1-9bdc-8d871fddb265','do.thi.phuc',          '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','e4b996a4-dc2a-4bb4-9355-abae581bc3db','ACTIVE', 0,NULL,NOW(),NOW()),
('a1ea9335-229f-458d-95f4-3061a9a939ee','lam.van.tu',           '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','6b2ad4af-43e6-4ee6-a182-4283d25fae4b','ACTIVE', 0,NULL,NOW(),NOW()),
('efb2aa62-a33f-4edc-8098-cb933694e36f','vo.thi.ngoc',          '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','6be5ea95-c15c-4083-bd9a-941b0278b024','ACTIVE', 0,NULL,NOW(),NOW()),
('7c022745-577a-4322-a1f6-e51b3e2f5fec','ho.van.phong',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','b3a7763f-20bc-4a25-ae7c-7dc92298019a','ACTIVE', 0,NULL,NOW(),NOW()),
('72c05567-ca59-4aca-a04a-3bb3ab499a34','duong.thi.huong',      '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','a85aceb6-e514-45ae-841d-ca548cfdb077','ACTIVE', 0,NULL,NOW(),NOW()),
('b4557218-e9d6-4efc-8349-1be8857f3596','tran.quoc.khoa',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','57060aad-a982-4ac3-8946-bb66aa04e362','ACTIVE', 0,NULL,NOW(),NOW()),
('3d6d64c6-900f-42f7-92cb-126a8b452bf5','nguyen.thi.thanh',     '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','b206b771-4c68-4fda-a7af-557b9a422cec','ACTIVE', 0,NULL,NOW(),NOW()),
('f143e621-77d0-452b-ad51-224b4ea7034f','le.van.tai',           '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','0a133121-0239-4e52-9562-8b32b1ae368f','ACTIVE', 0,NULL,NOW(),NOW()),
('169bb216-2b46-457e-a9dd-cd18296bda57','phung.thi.long',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','7b7d74f7-bcec-4de4-88ae-213c6ecb653d','ACTIVE', 0,NULL,NOW(),NOW()),
('d46cc834-7d14-4d13-ac30-0372dc6c098a','trinh.van.kim',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','f2487fbb-53c2-44ad-83ad-b8d22dbb959d','ACTIVE', 0,NULL,NOW(),NOW()),
('ac4fa3d6-d7ce-4084-a5b5-5ba144e29d15','le.hoang.nam',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','7b75874d-901b-453f-a9b7-c270cc7ae2c3','ACTIVE', 0,NULL,NOW(),NOW()),
('abe91028-3d26-45bc-9ba0-d3cfebaa89fe','bui.thi.minh',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','5c1c34f5-ce7e-4f1d-9564-9add86898ef6','ACTIVE', 0,NULL,NOW(),NOW()),
('4ba1651b-eb89-44c1-8718-b029f0a26f4a','nguyen.duc.thinh',     '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','6dfea6dd-3065-4c30-887a-f347e335a2a4','ACTIVE', 0,NULL,NOW(),NOW()),
('fa08dde2-b559-48ec-a88f-d9d00a2d6dde','vuong.thi.thuy',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','6410957f-75ac-4f34-b802-f7858ace137d','ACTIVE', 0,NULL,NOW(),NOW()),
('b89a87dc-4245-4b5b-bffc-ac4c3ddb85b6','dinh.minh.hai',        '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','855cf7ad-80cb-4074-a231-6133a027b59d','ACTIVE', 0,NULL,NOW(),NOW()),
('db5a84f7-c568-445c-acd6-ae06a70bd843','phan.thi.nga',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','3041bdbe-939a-4865-bcc5-3abde49ed874','ACTIVE', 0,NULL,NOW(),NOW()),
('7837f653-9c42-44ab-acab-2a1ea5bb04b1','hoang.van.tuan',       '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','a44424b8-0c44-4f99-be93-b619199e2297','ACTIVE', 0,NULL,NOW(),NOW()),
('387b9fe7-eb50-43ff-9e73-69e3692a8ea0','ngo.thi.bich',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','8b7f9063-ec83-4e22-b0e6-b8350b8ac591','ACTIVE', 0,NULL,NOW(),NOW()),
('c8af884b-7c00-4314-9e41-6c540469719b','dao.van.binh',         '$2a$12$nIU6GXZUjvpQZkEZoafeP.MepStYuzAKsvIWWmTra/xTu5Is7kbRm','EMPLOYEE','acd53b6d-1766-46b6-9452-b42bfc063469','ACTIVE', 0,NULL,NOW(),NOW())
ON CONFLICT (username) DO NOTHING;


-- ============================================================
-- 5. HỢP ĐỒNG — 15 hợp đồng cũ + 37 hợp đồng mới
-- ============================================================
INSERT INTO contract (id, emp_id, contract_number, contract_type, start_date, end_date, base_salary, offer_salary, salary_type, terms, file_url, status, created_at, created_by, updated_at, updated_by) VALUES
-- Existing 15 contracts (giữ nguyên)
('c4e294ca-ebd3-4001-a171-3ee1040c72f7','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','HĐ-2020-001','OFFICIAL', '2020-01-15',NULL,        60000000,85000000,'GROSS','Không xác định thời hạn — cán bộ cấp cao',NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('68c0d4c4-1e31-4bdc-b417-aa34c29524e0','4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','HĐ-2021-001','OFFICIAL', '2021-03-01',NULL,        35000000,52000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('2f911d08-7b22-4ba8-9494-3a3e91755f7a','507e337d-1d62-4b20-a062-ae115929788e','HĐ-2019-001','OFFICIAL', '2019-06-01',NULL,        55000000,75000000,'NET', 'Lương thực lĩnh sau thuế',NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('39a9047b-1f91-425f-a3ac-ce7267a9ed03','68f21e99-9da1-4f8c-bbf5-1ad3f53d5e52','HĐ-2022-001','PROBATION','2022-02-14','2022-05-13',12000000,15000000,'GROSS','Thử việc 3 tháng',NULL,'EXPIRED',   NOW(),'admin',NOW(),'admin'),
('05c19356-46a0-4b98-9911-7baf3f69fa39','68f21e99-9da1-4f8c-bbf5-1ad3f53d5e52','HĐ-2022-002','OFFICIAL', '2022-05-14',NULL,        18000000,24000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('12d4e767-f96f-406e-83a9-9d7901c65d9c','53bbd8d8-b6d5-4539-b02e-10465eec6c7d','HĐ-2023-001','OFFICIAL', '2023-10-01',NULL,        12000000,14000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('1a7b9695-d938-47e1-8a23-94cf48d2fcaf','844ff40e-48c1-45cf-9169-d51ed5fcfaf2','HĐ-2024-001','OFFICIAL', '2023-12-15',NULL,        13000000,16000000,'NET', NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('85b4bf02-6bd0-44a9-8f3b-d2e3ddd15806','cd971339-9e6c-4101-a642-085d0869f238','HĐ-2024-002','OFFICIAL', '2024-11-01',NULL,        12000000,14000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('757ac0e1-d2ec-4a5c-9857-0bd79737becd','c4126ca1-0383-412e-a48e-645c800b62e1','HĐ-2022-003','OFFICIAL', '2022-08-01',NULL,        13000000,17000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('a0c06eaa-d028-4799-aa8a-c72b3fef4b6c','8cca6b67-28c9-46ad-adf1-3e53d106de69','HĐ-2021-002','OFFICIAL', '2021-10-15',NULL,        18000000,23000000,'NET', NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('c37027c5-9a52-436d-865e-641e7fbd2bb8','20631750-0afe-47e8-8bbb-5bf8ac5257c5','HĐ-2023-002','OFFICIAL', '2023-03-01','2024-02-29',15000000,18000000,'GROSS',NULL,NULL,'TERMINATED',NOW(),'admin',NOW(),'admin'),
('69a15502-da39-4814-93fe-f8acc99c2be0','36002591-c63b-45f5-8258-212c9542239e','HĐ-2020-002','OFFICIAL', '2020-09-01',NULL,        30000000,42000000,'GROSS',NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('0884f740-70da-4c1c-8627-8c3a850e3c3c','5a37c376-062a-4355-86c0-924e30c04d3e','HĐ-2022-004','OFFICIAL', '2022-04-01',NULL,        22000000,28000000,'NET', NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('649a2c15-f500-48b6-93b1-946b4cb12200','ebe7beba-eba8-4fcf-a216-5980e1101f52','HĐ-2021-003','OFFICIAL', '2021-05-15',NULL,        28000000,38000000,'NET', NULL,NULL,'ACTIVE',    NOW(),'admin',NOW(),'admin'),
('11033c05-09bb-4f8a-b42c-9c5c508e8423','ec8ab77c-1a8a-418f-94de-3ec45b97b170','HĐ-2022-005','OFFICIAL', '2022-01-10','2024-01-09',25000000,33000000,'GROSS','Chấm dứt theo thỏa thuận',NULL,'TERMINATED',NOW(),'admin',NOW(),'admin'),
-- E16-E30: hợp đồng chính thức
('d7ecd962-1932-4c8e-997b-ee9fcafbbcd0','c9c9eef3-02fe-48cb-bd05-d346adee0579','HĐ-2020-016','OFFICIAL', '2020-06-15',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('bafbb33e-331b-4f34-81fe-20b6cf2ad4fa','88c12f02-940f-4d6e-81a6-e48a2e5274e5','HĐ-2020-017','OFFICIAL', '2020-11-01',NULL,        10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('7d2bd32e-4853-4fa6-93ac-e1cfb21214a6','a9ec17e8-6c1d-441e-80bc-bfce604b7132','HĐ-2021-018','OFFICIAL', '2021-02-01',NULL,        11500000,14000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('685e940e-dbb1-4b86-a6e7-a3ec6b2246ad','8b964f54-d9b9-470c-9105-dffd2dcea76e','HĐ-2021-019','OFFICIAL', '2021-07-01',NULL,        13000000,16000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('9c19e22f-0c60-4975-a380-68242ab665de','dda5ce6b-5966-4c25-a24e-35d4ea37d55d','HĐ-2021-020','OFFICIAL', '2021-10-01',NULL,        16000000,20000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('7f9222c1-6f68-458f-abbb-5629f829d7dd','dbf187e1-7a51-49da-8521-842e63e2ee99','HĐ-2022-021','OFFICIAL', '2022-03-01',NULL,        10500000,12500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('effde052-df5c-41b6-9bd6-88e06fb26bed','5bea127a-4161-43f0-9d1f-d3fca26b3331','HĐ-2022-022','OFFICIAL', '2022-06-15',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('f1e8b72d-7c3c-438a-bb25-5d31d055c6d3','d5ac5a0b-0e98-4599-b04a-59eb9d97a5e6','HĐ-2022-023','OFFICIAL', '2022-09-01',NULL,        10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('4b0e0551-1392-4743-849e-309398b9bd4d','a0f06db7-cb07-4bee-ac8f-16186080d66e','HĐ-2023-024','OFFICIAL', '2023-01-15',NULL,        11000000,13500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('826c460e-a941-4a89-abda-3dccc22608b3','9066ebd3-7f74-48c5-8a3f-9923cbe7ff14','HĐ-2023-025','OFFICIAL', '2023-04-01',NULL,        18000000,22000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('47921199-ccdb-47e9-af5b-d212f51d3f7b','285c4bfa-f35b-47a0-869d-87e6541d226f','HĐ-2023-026','OFFICIAL', '2023-05-15',NULL,        10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('0587f207-e935-423d-b7ef-e12f1c8b5cdc','31df7a3d-7cef-4167-a786-eca46b6a30ab','HĐ-2023-027','OFFICIAL', '2023-08-01',NULL,        10500000,12500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('501dadb1-2549-4566-8310-d2eacea25126','f936c597-1af1-4381-a7a8-4d47b0835f4d','HĐ-2024-028','OFFICIAL', '2024-01-15',NULL,        13000000,15500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('6fcea118-3317-4680-a57d-d10eeac590c9','0c377cce-3d75-4c6a-8ebc-355ffd82d141','HĐ-2024-029','OFFICIAL', '2024-05-15',NULL,        22000000,28000000,'NET', NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('6dd81d99-7dbe-4ce1-9504-a58985341239','045b6ede-9430-402b-b00a-4d41f4767fc5','HĐ-2024-030','OFFICIAL', '2024-08-01',NULL,        15000000,18000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
-- E31-E37 (Nov-Dec 2025): thử việc xong → chính thức
('05c7ce5b-f3e6-4700-ac6d-83d7d8ecc3d4','28fecfa8-8576-4131-854b-31fb9cef3b35','HĐ-2025-031','PROBATION','2025-11-03','2026-02-02', 9500000,11000000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('7960426a-25d1-4a76-bcbf-37d8412f5cb7','28fecfa8-8576-4131-854b-31fb9cef3b35','HĐ-2026-031','OFFICIAL', '2026-02-03',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('c1b9107d-d3ce-4534-bbba-edc049e99b0f','90eca336-ea16-4836-8525-c546664de0af','HĐ-2025-032','PROBATION','2025-11-10','2026-02-09', 9500000,11000000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('02c87c50-010c-456b-a221-a2fa5ebbafde','90eca336-ea16-4836-8525-c546664de0af','HĐ-2026-032','OFFICIAL', '2026-02-10',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('e2efca6b-fd94-4d04-b212-11353ab7ee62','9c11f3d0-7284-44c4-afe3-9485e7e705de','HĐ-2025-033','PROBATION','2025-11-17','2026-02-16',10000000,12000000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('426af81a-f094-42d1-af40-724e91ddefcc','9c11f3d0-7284-44c4-afe3-9485e7e705de','HĐ-2026-033','OFFICIAL', '2026-02-17',NULL,        12000000,14000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('80c2f038-a7ab-4999-894b-a49775de2bac','e4b996a4-dc2a-4bb4-9355-abae581bc3db','HĐ-2025-034','PROBATION','2025-11-24','2026-02-23', 9000000,10500000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('02f24aef-6b71-4d5b-b094-8e89e45ab639','e4b996a4-dc2a-4bb4-9355-abae581bc3db','HĐ-2026-034','OFFICIAL', '2026-02-24',NULL,        10500000,12500000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('d4d3c39f-e6cf-45a3-ae0b-a3eac9b19821','6b2ad4af-43e6-4ee6-a182-4283d25fae4b','HĐ-2025-035','PROBATION','2025-12-01','2026-02-28', 9500000,11000000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('1baf35d3-9de0-4dd2-859e-bb66d8eee185','6b2ad4af-43e6-4ee6-a182-4283d25fae4b','HĐ-2026-035','OFFICIAL', '2026-03-01',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('603a1a18-0593-40c8-962c-72287b8956ba','6be5ea95-c15c-4083-bd9a-941b0278b024','HĐ-2025-036','PROBATION','2025-12-10','2026-03-09', 9500000,11000000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('c2cbd1d7-20d5-4602-ab11-067620ba2f8a','6be5ea95-c15c-4083-bd9a-941b0278b024','HĐ-2026-036','OFFICIAL', '2026-03-10',NULL,        11000000,13000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('0c052917-3e3c-4a0c-a09c-257320bdafbb','b3a7763f-20bc-4a25-ae7c-7dc92298019a','HĐ-2025-037','PROBATION','2025-12-22','2026-03-21',12000000,14500000,'GROSS',NULL,NULL,'EXPIRED', NOW(),'admin',NOW(),'admin'),
('7e24c072-0d5e-4903-a66d-269cc08de2b1','b3a7763f-20bc-4a25-ae7c-7dc92298019a','HĐ-2026-037','OFFICIAL', '2026-03-22',NULL,        14500000,17000000,'GROSS',NULL,NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
-- E38-E52 (Jan-Apr 2026): còn đang thử việc
('3e56301d-a62b-4282-bac4-f01932a55cc2','a85aceb6-e514-45ae-841d-ca548cfdb077','HĐ-2026-038','PROBATION','2026-01-05','2026-04-04', 9000000,10500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('63b4d5db-5114-4ff5-9eb1-0bc36b9aaa25','57060aad-a982-4ac3-8946-bb66aa04e362','HĐ-2026-039','PROBATION','2026-01-12','2026-04-11',10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('8e082c1e-df97-4e40-b631-7d3236e1a52b','b206b771-4c68-4fda-a7af-557b9a422cec','HĐ-2026-040','PROBATION','2026-01-19','2026-04-18', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('b19d6bdf-9bc1-4649-9441-08028e61a7af','0a133121-0239-4e52-9562-8b32b1ae368f','HĐ-2026-041','PROBATION','2026-01-26','2026-04-25',12000000,14000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('a0036f4b-8044-408f-9e27-4b70f53e473c','7b7d74f7-bcec-4de4-88ae-213c6ecb653d','HĐ-2026-042','PROBATION','2026-02-02','2026-05-01', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('bc438e89-a244-4543-a399-ce5f99f4ea86','f2487fbb-53c2-44ad-83ad-b8d22dbb959d','HĐ-2026-043','PROBATION','2026-02-09','2026-05-08', 9000000,10500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('47ea0970-59d3-4b00-b003-fcf0e95ec323','7b75874d-901b-453f-a9b7-c270cc7ae2c3','HĐ-2026-044','PROBATION','2026-02-16','2026-05-15', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('9f6071fc-55cb-4263-a940-bbf5be4edffe','5c1c34f5-ce7e-4f1d-9564-9add86898ef6','HĐ-2026-045','PROBATION','2026-02-23','2026-05-22',10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('fadf4dcc-d2d2-47cf-bdec-21ddefdbb223','6dfea6dd-3065-4c30-887a-f347e335a2a4','HĐ-2026-046','PROBATION','2026-02-27','2026-05-26', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('bcd63e1a-b70a-4fbc-aa16-a5aaef1a12a3','6410957f-75ac-4f34-b802-f7858ace137d','HĐ-2026-047','PROBATION','2026-03-03','2026-06-02', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('420e88dd-f370-40fb-9e1a-02f8d10d9297','855cf7ad-80cb-4074-a231-6133a027b59d','HĐ-2026-048','PROBATION','2026-03-10','2026-06-09',12000000,14500000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('65f96d94-3899-4095-b14e-2647bccb4cc3','3041bdbe-939a-4865-bcc5-3abde49ed874','HĐ-2026-049','PROBATION','2026-03-24','2026-06-23',20000000,25000000,'NET', NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('10b2a896-4f68-42bd-94d1-85c94bdf3af3','a44424b8-0c44-4f99-be93-b619199e2297','HĐ-2026-050','PROBATION','2026-04-02','2026-07-01',10000000,12000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('61001663-1011-411f-bfe5-84df3b3740a6','8b7f9063-ec83-4e22-b0e6-b8350b8ac591','HĐ-2026-051','PROBATION','2026-04-07','2026-07-06', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('e064d322-c342-4ae5-bd3d-786dfbde8835','acd53b6d-1766-46b6-9452-b42bfc063469','HĐ-2026-052','PROBATION','2026-04-14','2026-07-13', 9500000,11000000,'GROSS',NULL,NULL,'ACTIVE',NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 6. DANH MỤC PHỤ CẤP — 15 bản ghi (giữ nguyên)
-- ============================================================
INSERT INTO allowance_config (id, code, name, description, default_amount, status, created_at, created_by, updated_at, updated_by) VALUES
('99714adb-be5f-4ce7-823f-8c8f54e1bf72','PC-AN', 'Phụ cấp ăn trưa',         'Hỗ trợ chi phí ăn trưa hằng ngày',                   730000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5993a6c9-52af-487e-814c-043c69eaf546','PC-DT', 'Phụ cấp điện thoại',      'Hỗ trợ cước phí điện thoại',                          300000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('d81cc1a3-a0e0-46a7-9ed2-0e16f8ae3643','PC-DL', 'Phụ cấp đi lại',          'Hỗ trợ chi phí di chuyển',                            500000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('b63cb124-348f-4a7e-a690-a4848302a919','PC-XE', 'Phụ cấp giữ xe',          'Hỗ trợ chi phí gửi xe',                               200000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5248bfa4-2134-44da-bf98-27c554715c33','PC-SK', 'Phụ cấp sức khỏe',       'Hỗ trợ khám sức khỏe định kỳ',                       1000000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('df4cde50-7eef-4ac6-ad9f-c052bebb4a93','PC-NO', 'Phụ cấp nhà ở',           'Hỗ trợ chi phí thuê nhà',                            2000000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('9a50eaa6-3890-4ce9-b558-6aaa62ce9011','PC-WFH','Phụ cấp làm việc từ xa', 'Hỗ trợ internet và điện WFH',                         500000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('5b15a29d-82c8-45ed-aacb-f25cb4149fe4','PC-DT2','Phụ cấp đào tạo',         'Hỗ trợ học phí nâng cao kỹ năng',                   1500000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('184ca3c0-72af-457f-9d78-d185bd9a23eb','PC-CC', 'Thưởng chứng chỉ',        'Thưởng đạt chứng chỉ quốc tế',                      3000000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('ad17a7dc-4ee8-420f-a155-6b0d8624d889','PC-SN', 'Phụ cấp sinh nhật',       'Quà tặng sinh nhật hằng năm',                         500000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('52011eca-e702-48a8-be33-26dc9114ca8b','PC-TN', 'Phụ cấp thâm niên',       'Tăng thêm theo số năm gắn bó',                           NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('45d460ec-045d-4bd1-ac6c-d7f97b14fdf9','PC-QL', 'Phụ cấp quản lý',         'Bổ sung cho trưởng phòng và giám đốc',               3000000,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('c3b491f6-0ced-42c4-a38a-41ccbbe87982','PC-BH', 'Phụ cấp bảo hiểm bổ sung','Bảo hiểm sức khỏe nâng cao',                            NULL,'ACTIVE',  NOW(),'admin',NOW(),'admin'),
('fdb49fd4-e560-418b-ad49-e34ef57f69fc','PC-OT', 'Phụ cấp làm thêm giờ',   'Đã chuyển sang lương OT',                                NULL,'INACTIVE',NOW(),'admin',NOW(),'admin'),
('3a5d7749-bade-4245-a59e-b98f6e4d4c38','PC-CA', 'Phụ cấp ca đêm',          'Ca làm từ 22h đến 6h',                                   NULL,'INACTIVE',NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 7. PHỤ CẤP NHÂN VIÊN — 6 bản ghi (giữ nguyên)
-- ============================================================
INSERT INTO employee_allowance (id, emp_id, contract_id, allowance_id, amount, effective_date, end_date, status, created_at, created_by, updated_at, updated_by) VALUES
('a77986c4-6664-40fa-8459-91235886ed63','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','c4e294ca-ebd3-4001-a171-3ee1040c72f7','99714adb-be5f-4ce7-823f-8c8f54e1bf72', 730000,'2020-01-15',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('9b166871-41a2-44a4-afc5-abcdbd8c03c1','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','c4e294ca-ebd3-4001-a171-3ee1040c72f7','45d460ec-045d-4bd1-ac6c-d7f97b14fdf9',5000000,'2020-01-15',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('a82e8a13-b61e-4796-aae6-aa01cd4a1f78','4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','68c0d4c4-1e31-4bdc-b417-aa34c29524e0','99714adb-be5f-4ce7-823f-8c8f54e1bf72', 730000,'2021-03-01',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('d0945f87-f592-4ab2-9258-5e5039e18dde','4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','68c0d4c4-1e31-4bdc-b417-aa34c29524e0','45d460ec-045d-4bd1-ac6c-d7f97b14fdf9',3000000,'2021-03-01',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('a7ab52e8-5c82-429f-91ed-c93064e1bf49','507e337d-1d62-4b20-a062-ae115929788e','2f911d08-7b22-4ba8-9494-3a3e91755f7a','99714adb-be5f-4ce7-823f-8c8f54e1bf72', 730000,'2019-06-01',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin'),
('fdc36db8-1dfe-4efd-830b-353d6b853974','36002591-c63b-45f5-8258-212c9542239e','69a15502-da39-4814-93fe-f8acc99c2be0','45d460ec-045d-4bd1-ac6c-d7f97b14fdf9',3000000,'2020-09-01',NULL,'ACTIVE',NOW(),'admin',NOW(),'admin');


-- ============================================================
-- 8. NGƯỜI PHỤ THUỘC — 5 bản ghi (giữ nguyên)
-- ============================================================
INSERT INTO employee_dependent (id, emp_id, name, dob, relationship, id_card, status, created_at, created_by, updated_at, updated_by) VALUES
('41efbbea-40d3-4376-a455-d885b32e6845','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','Lê Thị Ngọc Anh',   '1987-05-20','SPOUSE','001087005901','ACTIVE',NOW(),'admin',NOW(),'admin'),
('885f1e0a-83d4-4000-b88d-d8c44aaafc01','e8c6f3f5-3388-4466-a6ca-a8f6822eadbe','Nguyễn Minh Khoa',  '2015-08-12','CHILD', NULL,          'ACTIVE',NOW(),'admin',NOW(),'admin'),
('d05d86b9-f615-4507-bf1f-9fcc99b2da3d','4bec3bd9-c506-4392-b48f-ff3b75fdd6d5','Phạm Quốc Hùng',    '1985-11-30','SPOUSE','001085011902','ACTIVE',NOW(),'admin',NOW(),'admin'),
('b9cc8df4-a7dc-4430-a2bf-ded3e0e6153d','507e337d-1d62-4b20-a062-ae115929788e','Nguyễn Thị Thu Hà', '1984-09-18','SPOUSE','001084009904','ACTIVE',NOW(),'admin',NOW(),'admin'),
('1fa6cb6b-1e1e-4336-9678-0c31b704f746','68f21e99-9da1-4f8c-bbf5-1ad3f53d5e52','Hoàng Thị Lan',     '1965-02-14','PARENT','001065002905','ACTIVE',NOW(),'admin',NOW(),'admin');


COMMIT;

-- ============================================================
-- Tổng kết:
--   department        : 15  |  position          : 15
--   employee          : 52 (50 ACTIVE + 2 INACTIVE)
--   users             : 53 (1 hr.truong + 52 NV)
--   contract          : 52 (15 cũ + 22 thử việc/chính thức + 15 probation)
--   allowance_config  : 15  |  employee_allowance : 6
--   employee_dependent:  5
--
-- Headcount chart (cuối mỗi tháng):
--   T11/25=34 | T12/25=37 | T1/26=41 | T2/26=46 | T3/26=49 | T4/26=52
-- ============================================================
