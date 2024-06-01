CREATE DATABASE QuanLyRauSach
go

USE QuanLyRauSach
GO

CREATE TABLE PhanQuyen (
    MaPhanQuyen INTEGER IDENTITY NOT NULL,
    TenPhanQuyen NVARCHAR(50) NOT NULL,
    PRIMARY KEY (MaPhanQuyen)
);

CREATE TABLE TaiKhoan (
    MaTK INTEGER IDENTITY NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL,
    Matkhau VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    SDT VARCHAR(10) NOT NULL,
    AnhDaiDien VARCHAR(max),
    IsActive INTEGER NOT NULL,
    MaPhanQuyen INTEGER NOT NULL,
    PRIMARY KEY (MaTK),
    FOREIGN KEY (MaPhanQuyen) REFERENCES PhanQuyen(MaPhanQuyen)
);

CREATE TABLE TKNganHang (
    MaSoThe VARCHAR(50) NOT NULL,
    MaNH VARCHAR(10) NOT NULL,
    TenNH NVARCHAR(50) NOT NULL,
    TenChuThe NVARCHAR(50) NOT NULL,
    NgayCap DATE NOT NULL,
    NgayHetHan DATE NOT NULL,
    MaTK INTEGER NOT NULL,
    PRIMARY KEY (MaSoThe),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE SieuThi (
    MaST VARCHAR(10) NOT NULL,
    TenST NVARCHAR(50) NOT NULL,
    PRIMARY KEY (MaST)
);

CREATE TABLE NhaCungCap (
    MaNCC VARCHAR(10) NOT NULL,
    TenNCC NVARCHAR(50) NOT NULL,
    MoTa NTEXT NOT NULL,
    MaSoThue VARCHAR(50) NOT NULL,
    MaGPKD VARCHAR(50) NOT NULL,
    AnhGPKD VARCHAR(max),
    NgayDangKy DATE NOT NULL,
    PRIMARY KEY (MaNCC)
);

CREATE TABLE NhanVienNCC (
    MaNV VARCHAR(10) NOT NULL,
    TenNV NVARCHAR(50) NOT NULL,
    ChucVu NVARCHAR(50) NOT NULL,
    MaNCC VARCHAR(10) NOT NULL,
    MaTK INTEGER UNIQUE NOT NULL,
    PRIMARY KEY (MaNV),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE NhanVienST (
    MaNV VARCHAR(10) NOT NULL,
    TenNV NVARCHAR(50) NOT NULL,
    ChucVu NVARCHAR(50) NOT NULL,
    MaST VARCHAR(10) NOT NULL,
    MaTK INTEGER NOT NULL,
    PRIMARY KEY (MaNV),
    FOREIGN KEY (MaST) REFERENCES SieuThi(MaST),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE DiaCHiST (
    MaDC INTEGER IDENTITY NOT NULL,
    TenDuong NTEXT NOT NULL,
    PhuongXa NTEXT NOT NULL,
    QuanHuyen NTEXT NOT NULL,
    ThanhPho NTEXT NOT NULL,
    MaST VARCHAR(10) NOT NULL,
    PRIMARY KEY (MaDC),
    FOREIGN KEY (MaST) REFERENCES SieuThi(MaST)
);

CREATE TABLE DiaChiNCC (
    MaDC INTEGER IDENTITY NOT NULL,
    TenDuong NTEXT NOT NULL,
    PhuongXa NTEXT NOT NULL,
    QuanHuyen NTEXT NOT NULL,
    ThanhPho NTEXT NOT NULL,
    MaNCC VARCHAR(10) NOT NULL,
    PRIMARY KEY (MaDC),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

CREATE TABLE DanhMuc (
    MaDanhMuc INTEGER IDENTITY NOT NULL,
    TenDanhMuc NVARCHAR(50) NOT NULL,
    MoTa NTEXT NOT NULL,
    PRIMARY KEY (MaDanhMuc)
);

CREATE TABLE MatHang (
    MaMH VARCHAR(10) NOT NULL,
    MaDM INTEGER NOT NULL,
    TenMH NVARCHAR(50) NOT NULL,
    MoTa NTEXT,
    DonViTinh NVARCHAR(50) NOT NULL,
    Dongia DECIMAL(18, 0) NOT NULL,
    SoLuong INTEGER NOT NULL,
    TGBaoQuan NVARCHAR(50) NOT NULL,
    TinhTrang INTEGER NOT NULL,
    PRIMARY KEY (MaMH),
    FOREIGN KEY (MaDM) REFERENCES DanhMuc(MaDanhMuc)
);

CREATE TABLE HinhAnhMatHang (
    MaHAMH INTEGER IDENTITY NOT NULL,
    DuongDanHinhAnh TEXT,
    MaMH VARCHAR(10) NOT NULL,
    PRIMARY KEY (MaHAMH),
    FOREIGN KEY (MaMH) REFERENCES MatHang(MaMH)
);

CREATE TABLE ChiTietCungUng (
    MaNVNCC VARCHAR(10) NOT NULL,
    MaMH VARCHAR(10) NOT NULL,
    SoLuongCungUng INTEGER NOT NULL,
    PRIMARY KEY (MaNVNCC, MaMH),
    FOREIGN KEY (MaNVNCC) REFERENCES NhanVienNCC(MaNV),
    FOREIGN KEY (MaMH) REFERENCES MatHang(MaMH)
);

CREATE TABLE DonHang (
    MaDH INTEGER IDENTITY NOT NULL,
    MaNVST VARCHAR(10) NOT NULL,
    MaNVNCC VARCHAR(10),
    NgayDat DATE NOT NULL,
    NgayGiaoHang DATE,
    TGTaoDon DATETIME NOT NULL,
    TGThanhToan DATETIME,
    DiaChiNhan NTEXT NOT NULL,
    GhiChu NTEXT,
    HinhThucThanhToan NVARCHAR(50) NOT NULL,
    TrangThaiDH NVARCHAR(50) NOT NULL,
    TTDoiTra INTEGER NOT NULL,
    PRIMARY KEY (MaDH),
    FOREIGN KEY (MaNVST) REFERENCES NhanVienST(MaNV),
    FOREIGN KEY (MaNVNCC) REFERENCES NhanVienNCC(MaNV)
);

CREATE TABLE ChiTietDonHang (
    MaDH INTEGER NOT NULL,
    MaMH VARCHAR(10) NOT NULL,
    SoLuongDat INTEGER NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    PRIMARY KEY (MaDH, MaMH),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
    FOREIGN KEY (MaMH) REFERENCES MatHang(MaMH)
);

CREATE TABLE HoaDon (
    MaHD INTEGER IDENTITY NOT NULL,
    MaDH INTEGER NOT NULL,
    MaNVGH VARCHAR(10),
    NgayTaoHD DATE NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL,
    PhuongThucThanhToan NVARCHAR(50) NOT NULL,
    GhiChu NTEXT,
    PRIMARY KEY (MaHD),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
	FOREIGN KEY (MaNVGH) REFERENCES NhanVienNCC(MaNV)
);


-- Insert data
insert into PhanQuyen
values
	(N'Admin'), (N'Tài khoản siêu thị'), (N'Tài khoản NCC')

insert into TaiKhoan
values
	('admin', 'admin123', 'admin@ex.com', '0901234567', null, 1, 1),
	('nvst1', '12345', 'nvst1@ex.com', '0123456789', null, 1, 2),
	('nvst2', '12345', 'nvst2@ex.com', '0123456781', null, 1, 2),
	('nvst3', '12345', 'nvst3@ex.com', '0123456782', null, 1, 2),
	('nvst4', '12345', 'nvst4@ex.com', '0123456783', null, 1, 2),
	('nvst5', '12345', 'nvst5@ex.com', '0123456784', null, 1, 2),
	('nvncc1', '12345', 'nvncc1@ex.com', '0987654321', null, 1, 3),
	('nvncc2', '12345', 'nvncc2@ex.com', '0987654323', null, 1, 3),
	('nvncc3', '12345', 'nvncc3@ex.com', '0987654324', null, 1, 3),
	('nvncc4', '12345', 'nvncc4@ex.com', '0987654325', null, 1, 3),
	('nvncc5', '12345', 'nvncc5@ex.com', '0987654326', null, 1, 3)

set dateformat dmy;
insert into TKNganHang(MaSoThe, MaNH, TenNH, TenChuThe, NgayCap, NgayHetHan, MaTK)
values
	('1234567890123456', 'BIDV', N'Ngân hàng Đầu tư và Phát triển Việt Nam', N'Nguyễn Văn A', '10/05/2024', '10/05/2029', 2),
	('1234567890123451', 'Vietcom', N'Ngân hàng Ngoại thương Việt Nam', N'Lê Thị B', '15/05/2024', '15/05/2027', 3),
	('1234567890123462', 'Techcom', N'Ngân hàng Kỹ thương Việt Nam', N'Trần Minh C', '10/05/2024', '10/05/2029', 4),
	('1234567890123401', 'VPBank', N'Ngân hàng Việt Nam Phát triển', N'Phạm Thị D', '10/05/2024', '10/05/2029', 5),
	('1234567890123414', 'Vietin', N'Ngân hàng TMCP Công Thương Việt Nam', N'Nguyễn Văn E', '10/05/2024', '10/05/2029', 6),
	('1234567890123406', 'MBBank', N'Ngân hàng Quân Đội', N'Lê Thị F', '10/05/2024', '10/05/2029', 7),
	('1234567890123469', 'ACB', N'Ngân hàng Á Châu', N'Trần Minh G', '10/05/2024', '10/05/2029', 8),
	('1234567890123444', 'Sacombank', N'Ngân hàng Sài Gòn Thương Mại', N'Phạm Thị H', '08/05/2024', '08/05/2029', 9),
	('1234567890123954', 'SHB', N'Ngân hàng Sài Gòn - Hà Nội', N'Nguyễn Văn I', '10/05/2024', '10/05/2029', 10),
	('1234567890123494', 'Techcom', N'Ngân hàng Kỹ thương Việt Nam', N'Hồ Thanh K', '10/05/2024', '10/05/2029', 11)

insert into SieuThi
values
	('ST001', N'Win Mart'),
	('ST002', N'Vita Mart'),
	('ST003', N'BB Mini Mart'),
	('ST004', N'DMart'),
	('ST005', N'The 1971 Mini Mart')

set dateformat dmy;
insert into NhaCungCap
values
	('NCC01', N'Công Ty TNHH MTV Thực Phẩm & Đầu Tư Fococev', N'Công Ty TNHH MTV Thực Phẩm & Đầu Tư Fococev', '1012345678', '0001234567890', null, '28/04/2024'),
	('NCC02', N'Công Ty TNHH DASUN', N'Công Ty TNHH DASUN', '0278901234', '2001234567', null, '28/04/2024'),
	('NCC03', N'Công Ty Cổ Phần Ngon Sạch Bổ', N'Công Ty Cổ Phần Ngon Sạch Bổ', '0345678910', '0123456789012', null, '28/04/2024'),
	('NCC04', N'Công Ty TNHH Mê Kông', N'Công Ty TNHH Mê Kông', '1412345678', '2101234567', null, '28/04/2024'),
	('NCC05', N'Công Ty XNK Nông Sản & Thực Phẩm Chế Biến ĐN', N'Công Ty XNK Nông Sản & Thực Phẩm Chế Biến', '0567890123', '0987654321098', null, '28/04/2024')

insert into NhanVienNCC
values
	('NVNC001', N'Hoàng Văn A', N'Quản lý', 'NCC01', 7),
	('NVNC002', N'Trần Công B', N'Nhân viên', 'NCC01', 8),
	('NVNC003', N'Nguyễn Thanh C', N'Nhân viên', 'NCC01', 9),
	('NVNC004', N'Cao Thanh D', N'Quản lý', 'NCC02', 10),
	('NVNC005', N'Trương Văn E', N'Nhân viên', 'NCC02', 11)

insert into NhanVienST
values
	('NVST001', N'Lê Thị F', N'Quản lý', 'ST001', 2),
	('NVST002', N'Trần Minh G', N'Nhân viên', 'ST001', 3),
	('NVST003', N'Phạm Thị H', N'Nhân viên', 'ST001', 4),
	('NVST004', N'Nguyễn Văn K', N'Quản lý', 'ST002', 5),
	('NVST005', N'Trần Minh H', N'Nhân viên', 'ST002', 6)

insert into DiaCHiST
values
	(N'28 Phan Châu Trinh', N'Bình Hiên', N'Hải Châu', N'Đà Nẵng', 'ST001'),
	(N'74 Hàm Nghi', N'Thạc Gián', N'Thanh Khê', N'Đà Nẵng', 'ST002'),
	(N'254 Hải Phòng', N'Tân Chính', N'Thanh Khê', N'Đà Nẵng', 'ST003'),
	(N'910 Ngô Quyền', N'An Hải', N'Sơn Trà', N'Đà Nẵng', 'ST004'),
	(N'71 Lê Hồng Phong', N'Phước Ninh', N'Hải Châu', N'Đà Nẵng', 'ST005')

insert into DiaChiNCC
values
	(N'62 Trần Quốc Toản', N'Bình Hiên', N'Hải Châu', N'Đà Nẵng', 'NCC01'),
	(N'75 Lê Duẫn', N'Khuê Trung', N'Cẩm Lệ', N'Đà Nẵng', 'NCC02'),
	(N'10 Bình An', N'Hòa Cường', N'Thanh Khê', N'Đà Nẵng', 'NCC03'),
	(N'11 Nguyễn Thiện Thuật', N'Bình Thuận', N'Hải Châu', N'Đà Nẵng', 'NCC04'),
	(N'64 Trần Phú', N'Phước Ninh', N'Hải Châu', N'Đà Nẵng', 'NCC05')

insert into DanhMuc
values
	(N'Rau lá', N'Nhóm lá xanh đại đa số có hợp chất chlorophyll khiến lá có màu xanh'),
	(N'Củ/Rể', N'Nhóm này đại đa số giàu “đường carbohydrate” ở dạng amylopectin và amylose nói cách khác nhóm này đa số có hàm lượng calories'),
	(N'Mầm', N'Nhóm nà mọc từ trong đất và nẩy mầm khỏi mặt đất'),
	(N'Trái cây', N'Sở dĩ nhóm này gọi là trái cây bởi vì mỗi đơn vị mọc ra từ MỘT BÔNG HOA'),
	(N'Nhóm hoa', N'Nhóm này gọi là hoa vì hình dạng và cách sinh trưởng của chúng gần giống với các cây cho ra hoa')

insert into MatHang
values
	('MH001', 1, N'Cải ngồng hữu cơ', null, N'Bịch', 54000, 400, '5 ngày', 1),
	('MH002', 1, N'Rau muống', null, N'Bó', 10000, 400, '2 ngày', 1),
	('MH003', 2, N'Củ cải trắng', null, N'Kg', 70000, 500, '5 ngày', 1),
	('MH004', 2, N'Khoai tây hữu cơ', null, N'Kg', 30000, 500, '5 ngày', 1),
	('MH005', 4, N'Súp lơ xanh hữu cơ', null, N'Bịch', 85000, 400, '4 ngày', 1)

	SET IDENTITY_INSERT [dbo].[HinhAnhMatHang] ON 

INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (1, N'~/assets/img/myproducts/cai-ngong-1.jpg', N'MH001')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (2, N'~/assets/img/myproducts/rau-muong-5.jpg', N'MH002')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (3, N'~/assets/img/myproducts/cua-cai-trang-9.jpg', N'MH003')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (4, N'~/assets/img/myproducts/khoai-tay-huu-co-7.jpg', N'MH004')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (5, N'~/assets/img/myproducts/sup-lo-xanh.jpg', N'MH005')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (6, N'~/assets/img/myproducts/cai-kale-8.jpg', N'MH006')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (7, N'~/assets/img/myproducts/can-tay-5.jpg', N'MH007')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (8, N'~/assets/img/myproducts/khoai-lang-mat-6.jpg', N'MH008')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (9, N'~/assets/img/myproducts/bi-do-3.jpg', N'MH009')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (10, N'~/assets/img/myproducts/bap-su-tim-8.jpg', N'MH010')
INSERT [dbo].[HinhAnhMatHang] ([MaHAMH], [DuongDanHinhAnh], [MaMH]) VALUES (11, N'~/assets/img/myproducts/thanh-long-ruot-trang-5.jpg', N'MH011')
	select * from HinhAnhMatHang

insert into HinhAnhMatHang
values
	(null, 'MH001'),
	(null, 'MH002'),
	(null, 'MH003'),
	(null, 'MH004'),
	(null, 'MH005')

insert into ChiTietCungUng
values
	('NVNC001', 'MH001', 400),
	('NVNC001', 'MH002', 400),
	('NVNC001', 'MH003', 500),
	('NVNC004', 'MH004', 500),
	('NVNC004', 'MH005', 400)

set dateformat dmy;
insert into DonHang (MaNVST, MaNVNCC, NgayDat, NgayGiaoHang, TGTaoDon, TGThanhToan, DiaChiNhan, GhiChu, HinhThucThanhToan, TrangThaiDH, TTDoiTra)
values
	('NVST001', null, '20/05/2024', null, '09:30:00 20/05/2024', null, N'28 Phan Châu Trinh, Hải Châu, Đà Nẵng', null, N'Thanh toán khi nhận hàng', N'Chờ xác nhận', 1),
	('NVST001', null, '21/05/2024', null, '13:25:00 21/05/2024', null, N'28 Phan Châu Trinh, Hải Châu, Đà Nẵng', null, N'Thanh toán khi nhận hàng', N'Chờ xác nhận', 1),
	('NVST001', null, '22/05/2024', null, '18:05:00 22/05/2024', null, N'28 Phan Châu Trinh, Hải Châu, Đà Nẵng', null, N'Thanh toán khi nhận hàng', N'Chờ xác nhận', 1),
	('NVST004', null, '21/05/2024', null, '14:46:00 21/05/2024', null, N'74 Hàm Nghi, Thạc Gián, Thanh Khê, Đà Nẵng', null, N'Thanh toán khi nhận hàng', N'Chờ xác nhận', 1),
	('NVST004', null, '22/05/2024', null, '08:25:00 22/05/2024', null, N'74 Hàm Nghi, Thạc Gián, Thanh Khê, Đà Nẵng', null, N'Thanh toán khi nhận hàng', N'Chờ xác nhận', 1)

insert into ChiTietDonHang
values
	(1, 'MH001', 20, 54000),
	(1, 'MH004', 15, 30000),
	(1, 'MH002', 60, 10000),
	(2, 'MH005', 10, 85000),
	(2, 'MH003', 50, 70000),
	(3, 'MH001', 80, 54000),
	(3, 'MH002', 75, 10000),
	(4, 'MH003', 45, 70000),
	(4, 'MH004', 10, 30000),
	(4, 'MH002', 50, 10000),
	(5, 'MH005', 20, 85000),
	(5, 'MH001', 90, 54000)

set dateformat dmy;
insert into HoaDon (MaDH, MaNVGH, NgayTaoHD, TongTien, PhuongThucThanhToan, GhiChu)
values
	(1, null, '20/05/2024', 2130000, N'Thanh toán khi nhận hàng', null),
	(2, null, '21/05/2024', 4350000, N'Thanh toán khi nhận hàng', null),
	(3, null, '22/05/2024', 5070000, N'Thanh toán khi nhận hàng', null),
	(4, null, '21/05/2024', 3950000, N'Thanh toán khi nhận hàng', null),
	(5, null, '22/05/2024', 6560000, N'Thanh toán khi nhận hàng', null)

CREATE TABLE GioHang (
    MaNVST VARCHAR(10) NOT NULL,
    MaMH VARCHAR(10) NOT NULL,
    SoLuong INTEGER NOT NULL,
    PRIMARY KEY (MaNVST, MaMH),
    FOREIGN KEY (MaNVST) REFERENCES NhanVienST(MaNV),
    FOREIGN KEY (MaMH) REFERENCES MatHang(MaMH)
);