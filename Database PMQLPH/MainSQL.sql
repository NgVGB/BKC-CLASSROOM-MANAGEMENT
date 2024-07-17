/* Login/Register */
create table TaiKhoanUD
(
    ID nchar(40) not null primary key,
    TenDangNhap nvarchar(40),
	TenNhanVien nvarchar(100),
    MatKhau nvarchar(40),
    Quyen nvarchar(100)
)

insert TaiKhoanUD values
('001', N'nguyenbaoadmin',N'Nguyen Bao ADMIN', N'123', N'Admin'),
('002', N'nguyenbaouser',N'Nguyen Bao USER', N'123', N'User')

select*from TaiKhoanUD

/* Main */
create table LoaiPhong
(
    IDPhong int not null primary key,
    TenLP nvarchar(100)
)

create table GiangVien
(
    IDGV nvarchar(40) not null primary key,
    TenGV nvarchar(300),
	Mon nvarchar(300)
)

create table Phong
(
    UUIDP nvarchar(20) not null primary key,
    TenPhong nvarchar(100),
    IDPhong int not null foreign key references LoaiPhong(IDPhong),
	Khu nvarchar(40),
	Lau nvarchar(40),
	TrangThai nvarchar(100)
)

create table PhanCong
(
    IDPC int not null primary key,
    IDGV nvarchar(40) not null foreign key references GiangVien(IDGV),
    UUIDP nvarchar(20) not null foreign key references Phong(UUIDP),
    NgayBatDau datetime,
    NgayKetThuc datetime
)

create table ThietBi
(
    ID int not null primary key,
    TenThietBi nvarchar(100),
    LoaiThietBi nvarchar(100),
    TrangThai nvarchar(100),
    UUIDP nvarchar(20) not null foreign key references Phong(UUIDP)
)

insert ThietBi values
(1, N'Máy tính', N'Điện tử', N'Hoạt động', '01'),
(2, N'Máy in', N'Điện tử', N'Tạm dừng', '01'),
(3, N'Bảng whiteboard', N'Điện tử', N'Hoạt động', '02'),
(4, N'TV', N'Điện tử', N'Bảo trì', '03')
select*from ThietBi

insert LoaiPhong values
(1, N'LÝ THUYẾT'),
(2, N'THỰC HÀNH'),
(3, N'HỘI TRƯỜNG')
select*from LoaiPhong

insert GiangVien values
('GV01', N'Nguyen Van A', N'Quản trị kinh doanh'),
('GV02', N'Nguyen Van B', N'CNTT'),
('GV03', N'Nguyen Van C', N'Mạng máy tính'),
('GV04', N'Nguyen Van D', N'UI/UX Design'),
('GV05', N'Nguyen Van E', N'Thương mại điện tử'),
('GV06', N'Nguyen Van F', N'Bếp'),
('GV07', N'Nguyen Van G', N'Khách sạn'),
('GV08', N'Nguyen Van H', N'Ôtô')
select*from GiangVien

insert Phong values
('01', N'A301', 1, N'Khu A', 2, N'Hoạt động'),
('02', N'B201', 1, N'Khu B', 1, N'Hoạt động'),
('03', N'B202', 1, N'Khu B', 1, N'Hoạt động'),
('04', N'B203', 1, N'Khu B', 1, N'Hoạt động'),
('05', N'B204', 2, N'Khu B', 2, N'Hoạt động'),
('06', N'C301', 2, N'Khu C', 2, N'Kết thúc'),
('07', N'C302', 3, N'Khu C', 2, N'Kết thúc'),
('08', N'C303', 3, N'Khu C', 2, N'Kết thúc')
select*from Phong

insert PhanCong values
(1, 'GV01', '01', '2024-01-01', '2024-06-30'),
(2, 'GV02', '02', '2024-02-01', '2024-07-31'),
(3, 'GV03', '03', '2024-03-01', '2024-08-31'), 
(4, 'GV04', '01', '2024-01-01', '2024-06-30'),
(5, 'GV05', '02', '2024-02-01', '2024-07-31'),
(6, 'GV06', '03', '2024-03-01', '2024-08-31'), 
(7, 'GV07', '01', '2024-01-01', '2024-06-30'),
(8, 'GV08', '02', '2024-02-01', '2024-07-31')
select*from PhanCong