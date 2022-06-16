﻿CREATE DATABASE THPT
USE THPT
Drop database THPT
DROP TABLE NIENKHOA
DROP TABLE GIANGDAY
DROP TABLE CHITIETDIEM
DROP TABLE LOAIKIEMTRA
DROP TABLE DIEMMON
DROP TABLE HANHKIEM
DROP TABLE LOPDAHOC
DROP TABLE HOCSINH
DROP TABLE LOP
DROP TABLE GIAOVIEN
DROP TABLE MONHOC
DROP TABLE KHOI
DROP TABLE TAIKHOAN

CREATE TABLE NIENKHOA(
	MANK CHAR(11) PRIMARY KEY NOT NULL,
	NAMBD CHAR(5),
	NAMKT CHAR(5)
)

CREATE TABLE TAIKHOAN(
	MATK		CHAR(10) PRIMARY KEY NOT NULL, --MÃ TÀI KHOẢN
	USERNAME	NVARCHAR(20), --TÀI KHOẢN
	PASS		NVARCHAR(20), --MẬT KHẨU
)

CREATE TABLE KHOI(
	MAKHOI	CHAR(4) PRIMARY KEY, --MÃ KHỐI
	TENKHOI NVARCHAR(20), --TÊN KHỐI
)

CREATE TABLE MONHOC(
	MAMH	CHAR(10) PRIMARY KEY, --MÃ MÔN HỌC
	TENMH	NVARCHAR(10), -- TÊN MÔN HỌC
)

CREATE TABLE GIAOVIEN(
	MAGV	CHAR(10) PRIMARY KEY, --MÃ GIÁO VIÊN
	MAMH	CHAR(10), --MÃ MÔN HỌC
	MATK	CHAR(10), --MÃ TÀI KHOẢN
	TENGV	NVARCHAR(25), -- TÊN GIÁO VIÊN
	DIACHI	NVARCHAR(50), --ĐỊA CHỈ
	NAMSINH CHAR(4),		-- NĂM SINH
	GIOITINH NVARCHAR(3),--GIỚI TÍNH
	SDT		VARCHAR(10), -- SỐ ĐIỆN THOẠI
	EMAIL	VARCHAR(MAX),
	CONSTRAINT FK_GV_MAMH FOREIGN KEY(MAMH) REFERENCES MONHOC(MAMH),
	CONSTRAINT FK_GV_MATK FOREIGN KEY(MATK) REFERENCES TAIKHOAN(MATK)
)

CREATE TABLE LOP(
	MALOP	CHAR(10) PRIMARY KEY,	--MÃ LỚP
	MAKHOI	CHAR(4),	--MÃ KHỐI
	MAGVCN	CHAR(10),	--MÃ GIÁO VIÊN CHỦ NHIỆM
	TENLOP	NVARCHAR(10),--TÊN LỚP
	SISO	int,	--SĨ SỐ
	-- Sửa trong lưu đồ ERD
	NAMHOC CHAR(10)
	CONSTRAINT FK_LOP_MAKHOI FOREIGN KEY(MAKHOI) REFERENCES KHOI(MAKHOI),
	CONSTRAINT FK_LOP_MAGV FOREIGN KEY(MAGVCN) REFERENCES GIAOVIEN(MAGV)
)

CREATE TABLE HOCSINH(
	MAHS		CHAR(10) PRIMARY KEY, --MÃ HỌC SINH
	MALOP		CHAR(10),
	--MAHK		CHAR(3), --MÃ HẠNH KIỂM
	MATK		CHAR(10), --MÃ TÀI KHOẢN
	HotenHS		NVARCHAR(25), --(họ tên học sinh) 
	ngaysinh	smalldatetime, --(Ngày sinh) 
	diachi		NVARCHAR(50), --(địa chỉ) 
	noisinh		NVARCHAR(max),
	sodt		varchar(10),
	email		varchar(max),
	gioitinh	NVARCHAR(3), --(giới tính ) 
	nienkhoa	VARCHAR(10), --(năm học ) 
	dantoc		NVARCHAR(15), --(dân tộc) 
	tongiao		NVARCHAR(15), --(tôn giáo ) 
	tencha		NVARCHAR(25), --(tên cha) 
	nghenghiepcha NVARCHAR(10), --(nghề nghiệp cha) 
	ngaysinhcha smalldatetime, --(ngày sinh cha) 
	tenme		NVARCHAR(25), --(tên mẹ) 
	nghenghiepme NVARCHAR(10), --(nghề nghiệp mẹ)
	ngaysinhme smalldatetime, --(ngày sinh mẹ) 
	Ghichu		NVARCHAR(30), --(ghi chú)
	ANHHS		VARBINARY(max),
	CONSTRAINT FK_HS_MALOP FOREIGN KEY(MALOP) REFERENCES LOP(MALOP),
	CONSTRAINT FK_HS_MATK FOREIGN KEY(MATK) REFERENCES TAIKHOAN(MATK)
)

--Thêm vào lưu đồ ERD
CREATE TABLE LOPDAHOC(
	MALOPDAHOC CHAR(11) PRIMARY KEY,
	MALOP CHAR(10),
	MAHS CHAR(10),
	CONSTRAINT FK_LOPDAHOC_LOP FOREIGN KEY(MALOP) REFERENCES LOP(MALOP),
	CONSTRAINT FK_LOPDAHOC_HOCSINH FOREIGN KEY(MAHS) REFERENCES HOCSINH(MAHS)
)
SELECT HS.MAHS, HS.HotenHS FROM HOCSINH AS HS

SELECT HS.MAHS, HS.HotenHS FROM HOCSINH AS HS, LOP AS L 
--WHERE (HS.MALOP = L.MALOP AND L.TENLOP = '{CB_Lop_page3.SelectedItem.ToString()}' AND L.NAMHOC = '2019-2020' AND L.MAKHOI = 'K11') OR EXISTS (SELECT * FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HS.MAHS AND LOPDAHOC.MALOP = L.MALOP)";
--LTER TABLE HOCSINH DROP CONSTRAINT FK_HS_MALOP
--ALTER TABLE HOCSINH DROP CONSTRAINT FK_HS_MATK
--DROP TABLE HOCSINH

CREATE TABLE HANHKIEM(--Tốt, khá, trung bình, yếu.
	MAHK		CHAR(10) PRIMARY KEY, --MÃ HẠNH KIỂM
	MAHS		CHAR(10), --MAHOCSINH
	NAMHOC CHAR(10),
	XEPLOAIHKI	NVARCHAR(10), --XẾP LOẠI HỌC KÌ 1
	XEPLOAIHKII	NVARCHAR(10), --XẾP LOẠI HK 2
	XEPLOAICN	NVARCHAR(10), --XẾP LOẠI CUỐI NĂM

	CONSTRAINT FK_HK_HS FOREIGN KEY(MAHS) REFERENCES HOCSINH(MAHS),
)


CREATE TABLE DIEMMON(
	MADIEMMON CHAR(10) PRIMARY KEY NOT NULL,
	MAMONHOC CHAR(10),
	NAMHOC CHAR(10),
	MAHK CHAR(10),
	MAHOCSINH CHAR(10),
	TRUNGBINH FLOAT,
	CONSTRAINT FK_DIEMMON_MAMH FOREIGN KEY(MAMONHOC) REFERENCES MONHOC(MAMH),
	CONSTRAINT FK_DIEMMON_MAHS FOREIGN KEY(MAHOCSINH) REFERENCES HOCSINH(MAHS)
)

CREATE TABLE LOAIKIEMTRA(
	MALOAIKT CHAR(10) PRIMARY KEY NOT NULL,
	TENLOAIKT VARCHAR(20),
)

CREATE TABLE CHITIETDIEM(
	MADIEMMON CHAR(10),
	MALOAIKT CHAR(10),
	DIEM FLOAT,
	CONSTRAINT PK_CHITIETDIEM PRIMARY KEY(MADIEMMON, MALOAIKT),
	CONSTRAINT FK_CTD_MADIEMMON FOREIGN KEY(MADIEMMON) REFERENCES DIEMMON(MADIEMMON),
	CONSTRAINT FK_CTD_MALOATKT FOREIGN KEY(MALOAIKT) REFERENCES LOAIKIEMTRA(MALOAIKT)
)

CREATE TABLE GIANGDAY(
	MAGV	CHAR(10), 
	MALOP	CHAR(10),
	CONSTRAINT PK_GIANGDAY PRIMARY KEY(MAGV, MALOP),
	CONSTRAINT FK_GD_MAGV FOREIGN KEY(MAGV) REFERENCES GIAOVIEN(MAGV),
	CONSTRAINT FK_GD_MALOP FOREIGN KEY(MALOP) REFERENCES LOP(MALOP)
)

CREATE TRIGGER TRG_UPDATE_SISO ON HOCSINH FOR UPDATE AS
BEGIN 
	UPDATE LOP
	SET SISO = (SELECT COUNT(*) FROM HOCSINH AS HS WHERE HS.MALOP = LOP.MALOP OR EXISTS(SELECT* FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HS.MAHS AND LOPDAHOC.MALOP = LOP.MALOP))
END

CREATE TRIGGER TRG_UPDATE_SISO_DELETEHS ON HOCSINH FOR DELETE AS
BEGIN 
	UPDATE LOP
	SET SISO = (SELECT COUNT(*) FROM HOCSINH AS HS WHERE HS.MALOP = LOP.MALOP OR EXISTS(SELECT* FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HS.MAHS AND LOPDAHOC.MALOP = LOP.MALOP))
END

CREATE TRIGGER TRG_UPDATE_SISO_INSERTHS ON HOCSINH FOR INSERT AS
BEGIN 
	UPDATE LOP
	SET SISO = (SELECT COUNT(*) FROM HOCSINH AS HS WHERE HS.MALOP = LOP.MALOP OR EXISTS(SELECT* FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HS.MAHS AND LOPDAHOC.MALOP = LOP.MALOP))
END
--DROP TRIGGER TRG_UPDATE_SISO_INSERTHS
--DROP TRIGGER TRG_UPDATE_SISO_DELETEHS
--DROP TRIGGER TRG_UPDATE_SISO

INSERT INTO NIENKHOA(MANK, NAMBD, NAMKT)
	VALUES	('2019-2022', '2019', '2022'),
			('2020-2023', '2020', '2023'),
			('2021-2024', '2021', '2024'),
			('2022-2025', '2022', '2025')



INSERT INTO TAIKHOAN(MATK, USERNAME, PASS)
	VALUES
	('TK0001', 'GV86104262', '3beofhaxqk'),
		  ('TK0002', 'GV41317811', '8wx9hkbkgq'),
		  ('TK0003', 'GV27348923', 'xscs7sdf8d'),
		  ('TK0004', 'HS80901207', 'a'),
		  ('TK0005', 'HS62872219', 'a'),
		  ('TK0006', 'HS23948733', 'a'),
		  ('TK0000', 'admin',	   'admin')
		  select * from TAIKHOAN

INSERT INTO KHOI(MAKHOI, TENKHOI)
	VALUES('K10', N'Khối 10'),
		  ('K11', N'Khối 11'),
		  ('K12', N'Khôi 12')

INSERT INTO MONHOC(MAMH, TENMH)
	VALUES('MHT', N'Toán học'),
		  ('MHV', N'Ngữ văn'),
		  ('MHVL', N'Vật lí'),
		  ('MHHH', N'Hóa học'),
		  ('MHSH', N'Sinh học'),
		  ('MHTH', N'Tin học'),
		  ('MHLS', N'Lịch sử'),
		  ('MHDL', N'Địa lí'),
		  ('MHNN', N'Ngoại ngữ'),
		  ('MHCD', N'GDCD'),
		  ('MHCN', N'Công nghệ'),
		  ('MHTD', N'Thể dục'),
		  ('MHQP', N'GDQP')

INSERT INTO GIAOVIEN(MAGV, MAMH, MATK, TENGV, DIACHI, NAMSINH, GIOITINH, SDT)
	VALUES('GV001',		'MHT',	 'TK0001',	 N'Nguyễn Văn A',	N'Thủ Đức',	'1995',		N'Nam',	'0909001122'),
		  ('GV002',		'MHV',	 'TK0002',	 N'Nguyễn Thị B',	N'Quận 9',	'1982',		N'Nữ',	'0989787113'),
		  ('GV003',		'MHNN',	 'TK0003',	 N'Nguyễn Văn C',	N'Quận 10',	'1997',		N'Nam',	'0923454352')

SELECT GV.TENGV, MH.TENMH FROM  GIAOVIEN AS GV, MONHOC AS MH WHERE GV.MAMH = MH.MAMH
--DELETE FROM MONHOC
--DELETE FROM GIAOVIEN
--select * from MONHOC

INSERT INTO LOP(MALOP, MAKHOI, MAGVCN, TENLOP,  NAMHOC)
	VALUES('L101',	'K10', 'GV001', '10A1',  '2021-2022'),
		  ('L111',	'K11', 'GV002', '11C1',  '2021-2022'),
		  ('L121',	'K12', 'GV003', '12A2',  '2021-2022')

SET DATEFORMAT dmy

INSERT INTO HOCSINH(MAHS,	 MALOP,	 MATK,		HotenHS,				ngaysinh,		diachi,			gioitinh, nienkhoa,		tencha,					tenme)				
	VALUES('HS001',			'L101', 'TK0004',	N'Trương Anh Duy',		'27/2/2006',	N'Quận 2',		N'Nam',	  '2021-2024',	N'Nguyễn Văn F',		N'Nguyễn Thị S'),
		  ('HS002',			'L111', 'TK0005',	N'Nguyễn Hồ Bảo Minh',	'1/5/2005',		N'Quận 7',		N'Nam',	  '2021-2024',	N'Nguyễn X',			N'Nguyễn Y'),
		  ('HS003',			'L121', 'TK0006',	N'Trần Minh Thức',		'26/8/2004',	N'Thủ Đức',		N'Nam',	  '2021-2024',	N'Nguyễn A',			N'Nguyễn B')

INSERT INTO HANHKIEM(MAHK, MAHS, XEPLOAIHKI, NAMHOC)
	VALUES('HK1', 'HS001', N'Tốt', '2021-2022'),
		  ('HK2', 'HS002', N'Trung Bình', '2021-2022'),
		  ('HK3', 'HS003', N'Khá', '2021-2022')

SELECT XEPLOAIHKI, XEPLOAIHKII FROM HANHKIEM WHERE MAHS = 'HS001' AND NAMHOC = '2021-2022'

INSERT INTO DIEMMON(MADIEMMON, MAMONHOC, MAHK, NAMHOC, MAHOCSINH)
	VALUES('HS001D4', 'MHNN', 'HK2', '2021-2022', 'HS001'),
		  ('HS001D1', 'MHT', 'HK1', '2021-2022', 'HS001'),
		  ('HS001D2', 'MHV', 'HK1', '2021-2022', 'HS001'),
		  ('HS001D3', 'MHNN', 'HK1', '2021-2022', 'HS001'),
		  ('HS002D1', 'MHT', 'HK1', '2021-2022', 'HS002'),
		  ('HS002D2', 'MHV', 'HK1', '2021-2022', 'HS002'),
		  ('HS002D3', 'MHNN', 'HK1', '2021-2022', 'HS002'),
		  ('HS003D1', 'MHT', 'HK1', '2021-2022', 'HS003'),
		  ('HS003D2', 'MHV', 'HK1', '2021-2022', 'HS003'),
		  ('HS003D3', 'MHNN', 'HK1', '2021-2022', 'HS003')
/*
SELECT HS.HotenHS, hs.MAHS, DM.MADIEMMON, DM.TRUNGBINH
FROM HOCSINH AS HS
LEFT JOIN DIEMMON AS DM ON DM.MAHOCSINH = HS.MAHS
WHERE HS.MAHS = 'HS001'
*/

--SELECT MAMONHOC, TRUNGBINH FROM DIEMMON WHERE MAHK = 'HK1' AND NAMHOC = '2021-2022' AND MAHOCSINH = 'HS001'

INSERT INTO LOAIKIEMTRA(MALOAIKT, TENLOAIKT)
	VALUES('DTX1', 'DDGTX1'),
		  ('DTX2', 'DDGTX2'),
		  ('DTX3', 'DDGTX3'),
		  ('DTX4', 'DDGTX4'),
		  ('DGK', 'DDGGK'),
		  ('DCK', 'DDGCK')
--SELECT * FROM LOAIKIEMTRA

INSERT INTO CHITIETDIEM(MADIEMMON, MALOAIKT, DIEM)
	VALUES('HS002D2', 'DTX4',  '7.5'),
		  ('HS001D1', 'DTX1', '9'),
		  ('HS001D1', 'DTX2', '8'),
		  ('HS001D1', 'DTX3', '7.5'),
		  ('HS001D1', 'DGK',  '6'),
		  ('HS001D1', 'DCK',  '7'),
		  ('HS001D2', 'DTX1', '8'),
		  ('HS001D2', 'DTX2', '9'),
		  ('HS001D2', 'DGK',  '7.5'),
		  ('HS001D2', 'DCK',  '8'),
		  ('HS001D3', 'DTX1', '7'),
		  ('HS001D3', 'DTX2', '7'),
		  ('HS001D3', 'DTX3', '8'),
		  ('HS001D3', 'DGK',  '5'),
		  ('HS001D3', 'DCK',  '10'),

		  ('HS002D1', 'DTX1', '10'),
		  ('HS002D1', 'DTX2', '10'),
		  ('HS002D1', 'DTX3', '9'),
		  ('HS002D1', 'DGK',  '9'),
		  ('HS002D1', 'DCK',  '9.5'),
		  ('HS002D2', 'DTX1', '7'),
		  ('HS002D2', 'DTX2', '6'),
		  ('HS002D2', 'DTX3', '8'),
		  ('HS002D2', 'DGK',  '7.5'),
		  ('HS002D2', 'DCK',  '8'),
		  ('HS002D3', 'DTX1', '10'),
		  ('HS002D3', 'DTX2', '10'),
		  ('HS002D3', 'DGK',  '9'),
		  ('HS002D3', 'DCK',  '10'),

		  ('HS003D1', 'DTX1', '9'),
		  ('HS003D1', 'DTX2', '6'),
		  ('HS003D1', 'DGK',  '8.5'),
		  ('HS003D1', 'DCK',  '8.5'),
		  ('HS003D2', 'DTX1', '8'),
		  ('HS003D2', 'DTX2', '9'),
		  ('HS003D2', 'DTX3', '7.5'),
		  ('HS003D2', 'DGK',  '7.5'),
		  ('HS003D2', 'DCK',  '8.5'),
		  ('HS003D3', 'DTX1', '9'),
		  ('HS003D3', 'DTX2', '9'),
		  ('HS003D3', 'DTX3', '8'),
		  ('HS003D3', 'DGK',  '10'),
		  ('HS003D3', 'DCK',  '9.5')

INSERT INTO GIANGDAY(MAGV, MALOP)
	VALUES('GV001', 'L101'),
		  ('GV001', 'L111'),
		  ('GV002', 'L111'),
		  ('GV002', 'L121'),
		  ('GV003', 'L121')

SELECT L.MALOP, L.TENLOP, GV.TENGV FROM LOP AS L, GIAOVIEN AS GV WHERE GV.MAGV =  L.MAGVCN AND L.MAKHOI = 'K11' AND L.NAMHOC = '2021-2022' 

select * from LOP

		  SELECT MAMONHOC, MADIEMMON, TRUNGBINH FROM DIEMMON

SELECT HS.MAHS, HS.HotenHS FROM HOCSINH AS HS, LOP AS L WHERE HS.MALOP = L.MALOP AND L.TENLOP = '11C1' AND L.NAMHOC = '' AND L.MAKHOI = ''

UPDATE HOCSINH
SET MALOP = ''
WHERE MAHS = ''

USE THPT

SELECT HS.MAHS, HS.HotenHS, HS.gioitinh, HS.Ghichu FROM HOCSINH AS HS, LOP AS L WHERE HS.MALOP = L.MALOP AND L.TENLOP = '11C1' AND L.NAMHOC = '' AND L.MAKHOI = ''

SELECT GV.TENGV FROM GIAOVIEN AS GV, GIANGDAY AS GD WHERE GD.MALOP = 'L111' AND GD.MAGV = GV.MAGV AND GV.MAMH = 'MHV'

SELECT MANK FROM NIENKHOA

SELECT * FROM HOCSINH, LOP WHERE LOP.MALOP = HOCSINH.MALOP

SELECT MAHS, HotenHS, gioitinh, ngaysinh, LOP.TENLOP, noisinh, diachi, sodt, email, Ghichu FROM HOCSINH, LOP WHERE LOP.MALOP = HOCSINH.MALOP AND LOP.MAKHOI = 'K11' 

 

SELECT * FROM HOCSINH, LOP WHERE LOP.MALOP = HOCSINH.MALOP AND LOP.NAMHOC = '2021-2022' AND LOP.MAKHOI = 'K11' AND LOP.TENLOP = '11C1'
UPDATE CHITIETDIEM
SET DIEM = '5'
WHERE MADIEMMON = 'HS001D1' AND MALOAIKT = 'DTX1'

SELECT L.TENLOP, L.SISO, GV.TENGV FROM LOP AS L, GIAOVIEN AS GV WHERE L.MALOP = 'L101' AND L.MAGVCN = GV.MAGV
/*Lấy điểm HS001: (đã test)
SELECT HS.MAHS, HS.HotenHS, MN.MAMH, MN.TENMH, CTD.MADIEMMON, CTD.DIEM, LKT.TENLOAIKT
FROM CHITIETDIEM AS CTD
INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON AND DM.MAHOCSINH = 'HS001'
LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC
--Viết cái này xong ngáo luôn ó@@
	
HS1:
		TX1		TX2		TX3		TX4		GK		CK		TRB
Toán	9		8		7.5				6		7
Văn		8		9						7.5		8
Anh		7		7		8				5		10

HS2:
		TX1		TX2		TX3		TX4		GK		CK		TRB
Toán	10		10		9				9		9.5
Văn		7		6		8				7.5		8
Anh		10		10						9		10

HS3:
		TX1		TX2		TX3		TX4		GK		CK		TRB
Toán	9		6						8.5		8.5
Văn		8		9		7.5				7.5		8.5
Anh		9		9		8				10		9.5
*/

			
--SELECT * FROM GIANGDAY
--THONG TIN HS
	SELECT HS.MAHS, HS.HotenHS, HS.NgaySinh, HS.diachi, HS.gioitinh, HS.nienkhoa, HS.dantoc,
	HS.tongiao, HS.tencha, HS.nghenghiepcha, HS.ngaysinhcha, HS.tenme, HS.nghenghiepme, 
	HS.ngaysinhme, HS.ghichu, L.TENLOP, HS.email, HS.sodt
	FROM HOCSINH AS HS
	LEFT JOIN LOP AS L ON L.MALOP = HS.MALOP 
	WHERE HS.MAHS = 'HS001'

USE THPT
--DIEMTUYENSINH
SELECT D.SBD, HS.HotenHS, HS.NgaySinh, D.NAMTHI, D.TOAN, D.VAN, D.ANH, D.MONCHUYEN
FROM DIEMDAUVAO AS D
RIGHT JOIN HOCSINH AS HS ON HS.MAHS = D.MAHS
WHERE HS.MAHS = 'HS001'

--Điểm tốt nghiệp:
SELECT HS.HotenHS, HS.NgaySinh, D.SBD, D.NAMTHI, D.TOAN, D.VAN, D.ANH, TN.VATLI, TN.HOAHOC, TN.SINHHOC, XH.LICHSU, XH.DIALI, XH.GDCD
FROM DIEMTHITN AS D
LEFT JOIN BANGDIEMTOHOPTUNHIEN AS TN ON TN.MABDTN = D.MABDTN
LEFT JOIN BANGDIEMTOHOPXAHOI AS XH ON XH.MABDTN = D.MABDTN
RIGHT JOIN HOCSINH AS HS ON HS.MAHS = D.MAHS
WHERE HS.MAHS = 'HS001'

--Tim hoc sinh
SELECT * FROM HOCSINH
WHERE MAHS = 'HS001'


--Lấy mã học kì, mã năm học
SELECT DISTINCT DM.MAHK, DM.NAMHOC
FROM DIEMMON AS DM
LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
WHERE HS.MAHS = 'HS002'
ORDER BY DM.NAMHOC ASC, DM.MAHK ASC


--Lấy điểm cá nhân theo học kì, năm học
SELECT HS.MAHS, HS.HotenHS, MN.MAMH, MN.TENMH, CTD.MADIEMMON, CTD.DIEM, LKT.TENLOAIKT, DM.TRUNGBINH 
                FROM CHITIETDIEM AS CTD
                INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON 
                LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
                LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC
				WHERE DM.MAHOCSINH = 'HS001' AND DM.MAHK = 'HK1' AND DM.NAMHOC = '2021-2022'

--Lấy list user + mật khẩu
SELECT TK.MATK, TK.USERNAME, TK.PASS
FROM TAIKHOAN AS TK

USE THPT

--Kiểm tra tồn tại user 
SELECT TK.USERNAME, TK.PASS
FROM TAIKHOAN AS TK
WHERE TK.USERNAME = 'HS80901207'
--Lấy thông tin user theo username của học sinh;

SELECT TK.USERNAME, TK.PASS, HS.MAHS
FROM TAIKHOAN AS TK
INNER JOIN HOCSINH AS HS ON HS.MATK = TK.MATK
WHERE TK.USERNAME = 'HS80901207'
--Lấy thông tin user theo username của giáo viên;

