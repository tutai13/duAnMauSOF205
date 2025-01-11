CREATE DATABASE QLBH
GO
USE QLBH
GO

CREATE TABLE [dbo].[NhanVien]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [MaNV] VARCHAR(20) NOT NULL,
    [Email] VARCHAR(50) NOT NULL,
    [TenNV] NVARCHAR(50) NOT NULL,
    [DiaChi] NVARCHAR(100) NOT NULL,
    [VaiTro] TINYINT NOT NULL,
    [TinhTrang] TINYINT NOT NULL,
    [MatKhau] NVARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaNV] ASC)
);
GO

INSERT INTO [dbo].[NhanVien] ([MaNV], [Email], [TenNV], [DiaChi], [VaiTro], [TinhTrang], [MatKhau])
VALUES
('NV001', 'nv001@example.com', N'Nguyễn Văn A', N'Hà Nội', 1, 1, 'abc123'),
('NV002', 'nv002@example.com', N'Nguyễn Văn B', N'Hà Nội', 0, 1, 'cba321');

SELECT * FROM [dbo].[NhanVien]
GO

CREATE TABLE [dbo].[KhachHang]
(
    [DienThoai] VARCHAR(15) NOT NULL,
    [TenKhach] NVARCHAR(50) NOT NULL,
    [DiaChi] NVARCHAR(100) NOT NULL,
    [Phai] NVARCHAR(20) NOT NULL,
    [MaNV] VARCHAR(20) NOT NULL,
    PRIMARY KEY CLUSTERED ([DienThoai] ASC),
    FOREIGN KEY ([MaNV]) REFERENCES [dbo].[NhanVien] ([MaNV])
);
GO

INSERT INTO [dbo].[KhachHang] ([DienThoai], [TenKhach], [DiaChi], [Phai], [MaNV])
VALUES
('0123456789', N'Khách A', N'Hà Nội', N'Nam', 'NV001'),
('0987654321', N'Khách B', N'Hà Nội', N'Nữ', 'NV002');

SELECT * FROM [dbo].[KhachHang]
GO

CREATE TABLE [dbo].[Hang] 
(
	[MaHang] INT IDENTITY (1000, 1) NOT NULL,
	[TenHang] NVARCHAR (50) NULL,
	[SoLuong] INT NOT NULL,
	[DonGiaBan] FLOAT (53) NOT NULL,
	[DonGiaNhap] FLOAT (53) NOT NULL,
	[HinhAnh] VARCHAR (400) NOT NULL,
	[GhiChu] NVARCHAR (20) NOT NULL,
	[MaNV] VARCHAR (20) NOT NULL,
	PRIMARY KEY CLUSTERED ([MaHang] ASC),
	FOREIGN KEY ([MaNV]) REFERENCES [dbo].[NhanVien] ([MaNV]) 
);

INSERT INTO [dbo].[Hang] ([TenHang], [SoLuong], [DonGiaBan], [DonGiaNhap], [HinhAnh], [GhiChu], [MaNV])
VALUES
(N'Sản phẩm A', 100, 50000.0, 40000.0, 'sp_a.jpg', N'Hàng mới', 'NV001'),
(N'Sản phẩm B', 50, 75000.0, 60000.0, 'sp_b.jpg', N'Hàng nhập khẩu', 'NV002'),
(N'Sản phẩm C', 75, 30000.0, 25000.0, 'sp_c.jpg', N'Hàng khuyến mãi', 'NV001');

SELECT * FROM [dbo].[Hang]
go 

CREATE PROC ChangePwd @email varchar(50), @oPwd nvarchar(50), @nPwd nvarchar(50)
AS BEGIN
    declare @op varchar(50)
    select @op = MatKhau from NhanVien where Email = @email
    if @op = @oPwd
    begin
        UPDATE NhanVien set Matkhau = @nPwd where Email = @email
        return 1
    end
    else
        return 0
END

CREATE PROC DangNhap @email varchar(50), @matKhau nvarchar(20)
AS BEGIN
    Declare @status int
    if exists(select * from Nhanvien where email = @email and Matkhau = @matKhau)
        set @status = 1
    else
        set @status = 0
    select @status
END

CREATE PROC DanhSachHang
as begin 
    select MaHang, TenHang, SoLuong, DonGiaNhap, DonGiaBan, HinhAnh, GhiChu From Hang
end 

CREATE PROC DanhSachKhach
as begin 
    select DienThoai, Tenkhach, DiaChi, Phai from KhachHang
end 

CREATE PROC DanhSachNhanVien
as begin 
    select Email, TenNV, DiaChi, VaiTro, TinhTrang, MatKhau from NhanVien
end 

CREATE PROC DeleteDataFromHang @mahang int
as
BEGIN
    DECLARE @result int = 1;
    if exists(select * from Hang where MaHang = @mahang)
        DELETE Hang where MaHang = @mahang
    else
        set @result = 0
    select @result
END

CREATE PROC DeleteDataFromKhach @DienThoai varchar(20)
as
BEGIN
    DECLARE @result int = 1;
    if exists(select * from KhachHang where DienThoai = @DienThoai) 
        DELETE KhachHang where DienThoai = @DienThoai
    else
        set @result = 0
    select @result
END

CREATE PROC DeleteDataFromNhanVien @Email varchar(50)
as
BEGIN
    DECLARE @result int = 1;
    if exists(select * from NhanVien where Email = @Email) 
        DELETE Nhanvien where Email = @Email
    else
        set @result = 0
    select @result
END

CREATE PROC InsertDataKhach @DienThoai varchar(20), @Tenkhach nvarchar(50), @DiaChi nvarchar(100), @Phai nvarchar(20), @Email varchar(50)
AS
BEGIN
    DECLARE @MaNV varchar(20)
    Select @MaNV = MaNV from NhanVien where Email = @Email
    INSERT INTO KhachHang values (@DienThoai, @Tenkhach, @DiaChi, @Phai, @MaNV)
END

CREATE PROC [InsertDataHang] @TenHang nvarchar(50), @Soluong int, @DonGiaBan float, @DonGiaNhap float, @HinhAnh varchar(400) , @GhiChu nvarchar(20), @Email varchar(50)
AS
BEGIN
    DECLARE @MaNV varchar(20)
    Select @MaNV = MaNV from NhanVien where Email = @Email
    INSERT INTO Hang values (@TenHang, @Soluong, @DonGiaBan, @DonGiaNhap, @HinhAnh, @GhiChu, @MaNV) 
END

alter PROC InsertDataNhanVien @email varchar(50), @tennv nvarchar(50), @diachi nvarchar(100), @vaitro tinyint, @tinhtrang tinyint, @matKhau varchar(50)
AS
BEGIN
    DECLARE @Manv varchar(20);
    DECLARE @Id int
    Select @Id = ISNULL(MAX(Id), 1000) + 1 FROM NhanVien
    SELECT @Manv = 'NV' + CONVERT (varchar(4), @Id)
    INSERT INTO NhanVien (MaNV,Email,TenNV,DiaChi, VaiTro, TinhTrang, MatKhau) 
    VALUES (@Manv, @email, @tennv, @diachi, @vaitro, @tinhtrang,@matKhau)
END

create proc SearchHang @tenHang nvarchar(50)
as 
begin 
    select * from Hang where TenHang like '%' + @tenHang + '%'
end

create proc SearchKhach @tenKhach nvarchar(50)
as 
begin 
    select * from KhachHang where Tenkhach like '%' + @tenKhach + '%'
end

create proc SearchNhanVien @tenNV nvarchar(50)
as 
begin 
    select Email, TenNV, DiaChi, VaiTro, TinhTrang from NhanVien where TenNV like '%' + @tenNV + '%'
end

create proc UpdateHang @maHang int, @tenHang nvarchar(50), @soLuong int, @donGiaNhap float, @donGiaBan float, @hinhAnh nvarchar(400), @ghiChu nvarchar(50)
as begin 
    update Hang set TenHang = @tenHang, SoLuong= @soLuong,
    DonGiaBan = @donGiaBan , DonGiaNhap = @donGiaNhap,
    HinhAnh = @hinhAnh , GhiChu= @ghiChu
    where MaHang= @maHang;
end

create proc UpdateKhach @dienThoai varchar(20), @tenKhach nvarchar(30), @diaChi nvarchar(100), @phai nvarchar(20)
as
begin 
    update KhachHang set Tenkhach=@tenKhach , DiaChi= @diaChi, Phai= @phai
    where DienThoai = @dienThoai;
end 

create proc UpdateNhanVien @email varchar(50), @tenNv nvarchar(50), @diaChi nvarchar(50), @vaiTro tinyint, @tinhTrang tinyint
as 
begin
    update NhanVien set TenNV = @tenNv , DiaChi= @diaChi , VaiTro= @vaiTro, TinhTrang = @tinhTrang
    where Email = @email
end 

create proc LayVaiTroNV @email varchar(50) 
as
begin 
    select VaiTro from NhanVien where Email = @email
end

alter proc QuenMatKhau @email varchar(50)
as 
begin 
    declare @result int 
    if exists (select * from NhanVien where Email= @email)
        set @result =1
    else 
	begin 
        set @result =  0
		end
		select @result
end

create proc ThongKeSP
as begin 
    select a.MaNV, b.TenNV, sum(a.MaHang) from Hang a 
    inner join NhanVien b on a.MaNV = b.MaNV
    group by a.MaNV, b.TenNV
end

create proc ThongKeTonKho
as begin 
    select TenHang, sum(SoLuong) from Hang group by (TenHang)
end

create proc UpdateMatKhau @email varchar(50), @matKhau varchar(50)
as 
begin
    update NhanVien set MatKhau = @matKhau
    where Email = @email
end

select * from NhanVien

exec UpdateMatKhau 'taidtpd10687@gmail.com', '123123'