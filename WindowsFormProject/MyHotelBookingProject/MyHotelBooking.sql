USE [master]
GO

Create DATABASE MyTestDB1
GO



use MyTestDB1
Create TABLE CheckIn
(
	CheckInID int primary key identity(100, 1),
	[CheckInDate] [datetime] default GETDATE(),
	[Name] [varchar](30) NOT NULL,
	[FatherName] [varchar](30) NULL,
	[MotherName] [varchar](30) NULL,
	[NID] [varchar](30) NULL,
	[CellPhone] [varchar](15) NOT NULL,
	[RoomType] [varchar](20) NOT NULL,
	[RoomNo] [varchar](10) NOT NULL,
	[Address] [varchar](50) NULL,
	[NoOfPerson] [varchar](20) NULL,
	[Phone01] [varchar](15) NULL,
	[Phone02] [varchar](15) NULL,
	[Operator] [varchar](30) NULL,
	IsActive bit default 1
) 
GO

CREATE TABLE CoupleRoom
(
	[RoomNo] [int] NOT NULL,
	[S] [int] NOT NULL
)
GO

use MyTestDB1
CREATE TABLE Reservation
(
	ReservationID int primary key identity,
	[Name] [varchar](30) NOT NULL,
	[FatherName] [varchar](30) NULL,
	[MotherName] [varchar](30) NULL,
	[NID] [varchar](30) NULL,
	[CellPhone] [varchar](15) NOT NULL,
	[RoomType] [varchar](20) NOT NULL,
	[RoomNo] [varchar](10) NOT NULL,
	[Address] [varchar](50) NULL,
	[NoOfPerson] [varchar](20) NULL,
	[CheckInDate] [varchar](15) NULL,
	[CheckOutDate] [varchar](15) NULL,
	IsActive bit default 1
) 
GO


INSERT INTO CoupleRoom VALUES
('101', 1),
('102', 1),
('103', 1),
('104', 1),
('105', 1),
('106', 1),
('107', 1),
('108', 1),
('109', 1),
('110', 1)
GO



CREATE TABLE DeluxRoom
(
	[RoomNo] [int] NOT NULL,
	[S] [int] NOT NULL
)
GO

INSERT INTO DeluxRoom VALUES
('201', 1),
('202', 1),
('203', 1),
('204', 1),
('205', 1),
('206', 1),
('207', 1),
('208', 1),
('209', 1),
('210', 1)
GO

CREATE TABLE RoomType
(
	[RoomTypeId] [int] NOT NULL,
	[RoomName] [varchar](20) NOT NULL,
	Price money null
)
GO

INSERT INTO RoomType VALUES
(1, 'Couple Room', 1000),
(2, 'Delux Suit', 2000),
(3, 'Executive Suit', 3000),
(4, 'Super Delux', 4000),
(5, 'Family Suit', 5000)
GO



CREATE PROC sp_PriceUpdate
(
@roomname varchar (30),
@newprice money
)
AS
BEGIN
UPDATE RoomType SET Price = @newprice WHERE RoomName = @roomname  
END
GO

--EXEC sp_PriceUpdate 'Super Delux', 4100
--GO


Select * from RoomType
GO




CREATE TABLE Users 
(
Username varchar (30) not null,
Password varchar (30) not null,

)

INSERT INTO Users VALUES
('Belal', 'Belal123'),
('Akib', 'Akib123'),
('Rasel', 'Rasel123')
GO


--Cupole Room
CREATE PROCEDURE sp_InsertCheckIn3
(
@name varchar(30),
@fathername varchar (30),
@mothername varchar (30),
@nid varchar (30),
@cellphone varchar(15),
@roomtype varchar (20),
@roomno INT,
@address varchar (50),
@noofperson VARCHAR(10),
@phone01 varchar(15),
@phone02 varchar (15),
@operator varchar (30)
)
AS
BEGIN
INSERT INTO CheckIn(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, NoOfPerson, Phone01, Phone02, Operator)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @noofperson, @phone01, @phone02, @operator)
INSERT INTO InOut(Name, CellPhone, Operator)
VALUES (@name, @cellphone, @operator)
UPDATE DeluxRoom SET S = 3 WHERE RoomNo = @roomno
END
GO


CREATE PROCEDURE sp_ReservationCouple
(
@name varchar(30),
@fathername varchar (30),
@mothername varchar (30),
@nid varchar (30),
@cellphone varchar(15),
@roomtype varchar (20),
@roomno INT,
@address varchar (50),
@noofperson VARCHAR(10),
@checkindate varchar(15),
@checkoutdate varchar (15)
)
AS
BEGIN
INSERT INTO Reservation(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, NoOfPerson, CheckInDate, CheckOutDate)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @noofperson, @checkindate, @checkoutdate)

UPDATE CoupleRoom SET S = 2 WHERE RoomNo = @roomno

END
GO


CREATE PROCEDURE sp_ReservationDelux
(
@name varchar(30),
@fathername varchar (30),
@mothername varchar (30),
@nid varchar (30),
@cellphone varchar(15),
@roomtype varchar (20),
@roomno INT,
@address varchar (50),
@noofperson VARCHAR(10),
@checkindate varchar(15),
@checkoutdate varchar (15)
)
AS
BEGIN
INSERT INTO Reservation(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, NoOfPerson, CheckInDate, CheckOutDate)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @noofperson, @checkindate, @checkoutdate)

UPDATE DeluxRoom SET S = 2 WHERE RoomNo = @roomno

END
GO


CREATE PROCEDURE sp_PendingActionConfirm
(
@roomno INT,
@operator varchar (30)
)
AS
BEGIN

declare @name varchar (30) set @name = (Select Name From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @fathername varchar(30) set @fathername = (Select FatherName From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @mothername varchar(30) set @mothername = (Select MotherName From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @nid varchar(30) set @nid = (Select NID From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @cellphone varchar(30) set @cellphone = (Select CellPhone From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @roomtype varchar(30) set @roomtype = (Select RoomType From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @address varchar(30) set @address = (Select Address From Reservation Where RoomNo = @roomno AND IsActive = 1);
INSERT INTO CheckIn(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, Operator)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @operator)
INSERT INTO InOut(Name, CellPhone, Operator)
VALUES (@name, @cellphone, @operator)
UPDATE CoupleRoom SET S = 3 WHERE RoomNo = @roomno
UPDATE Reservation SET IsActive = 0 WHERE RoomNo = @roomno AND IsActive = 1
END
GO


CREATE PROCEDURE sp_PendingActionConfirmDeluxRoom
(
@roomno INT,
@operator varchar (30)
)
AS
BEGIN

declare @name varchar (30) set @name = (Select Name From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @fathername varchar(30) set @fathername = (Select FatherName From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @mothername varchar(30) set @mothername = (Select MotherName From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @nid varchar(30) set @nid = (Select NID From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @cellphone varchar(30) set @cellphone = (Select CellPhone From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @roomtype varchar(30) set @roomtype = (Select RoomType From Reservation Where RoomNo = @roomno AND IsActive = 1);
declare @address varchar(30) set @address = (Select Address From Reservation Where RoomNo = @roomno AND IsActive = 1);
INSERT INTO CheckIn(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, Operator)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @operator)
INSERT INTO InOut(Name, CellPhone, Operator)
VALUES (@name, @cellphone, @operator)
UPDATE DeluxRoom SET S = 3 WHERE RoomNo = @roomno
UPDATE Reservation SET IsActive = 0 WHERE RoomNo = @roomno
END
GO

--Delux Room
CREATE PROCEDURE sp_InsertCheckIn4
(
@name varchar(30),
@fathername varchar (30),
@mothername varchar (30),
@nid varchar (30),
@cellphone varchar(15),
@roomtype varchar (20),
@roomno INT,
@address varchar (50),
@noofperson VARCHAR(10),
@phone01 varchar(15),
@phone02 varchar (15),
@operator varchar (30)
)
AS
BEGIN
INSERT INTO CheckIn(Name, FatherName, MotherName, NID, CellPhone, RoomType, RoomNo, Address, NoOfPerson, Phone01, Phone02, Operator)
VALUES (@name, @fathername, @mothername, @nid, @cellphone, @roomtype, @roomno, @address, @noofperson, @phone01, @phone02, @operator)
INSERT INTO InOut(Name, CellPhone, Operator)
VALUES (@name, @cellphone, @operator)
UPDATE CoupleRoom SET S = 3 WHERE RoomNo = @roomno
END
GO



USE MyTestDB1
GO
CREATE TABLE InOut
(
InOutID int primary key identity,
CheckInDate varchar (30) default GETDATE(),
Name varchar (30) null,
CellPhone varchar(15) null,
CheckOutDate varchar (30) default GETDATE(),
Operator varchar (30) null,
ToalDays  AS Datediff(day, CheckInDate, CheckOutDate)
)
GO

USE MyTestDB1
GO
CREATE TABLE Calculation
(
ID int primary key identity (100, 1),
CheckInID int  foreign key references CheckIn (CheckInID),
Name varchar (30) null,
CellPhone varchar (15) null,
RoomType varchar (20) null,
RoomNo varchar (10) null,
CheckInDate varchar (30) null,
CheckOutDate varchar (30) null,
TotalDay int null,
RoomCharge money default 0,
Tax money default 0.15,
ServiceCharge money default 0.10,
Discount money default 0,
TotalAmount AS ((RoomCharge + (RoomCharge*Tax)+(RoomCharge*ServiceCharge)-Discount)*TotalDay)
)
GO

--INSERT INTO Calculation(Name, CellPhone, RoomType, RoomNo, CheckInDate, CheckOutDate, TotalDay, RoomCharge) Values ('Faysal', '09876543', 'Couple', '101', '2/2/2019', '2/4/2019', 2, 1000)
--GO

--DELETE FROM Calculation where ID  = 100
--GO

Select * from Calculation
GO


USE MyTestDB1
GO
CREATE TABLE BillEditRecord
(
BillEditRecordID int primary key identity (100, 1),
ID int foreign key references Calculation (ID)  ON DELETE CASCADE,
CheckInID int  foreign key references CheckIn (CheckInID),
DiscountAmount money default 0,
Operation varchar(50) default 'Discount Added To This Guest Bill',
EditBy varchar (30) null,
DateOfEdit varchar(30) default GETDATE()
)
GO

--insert into BillEditRecord (ID, CheckInID,  DiscountAmount) values (100,100, 2000)
--Go

Select * from BillEditRecord
Go

Select * from CheckIn
GO

Select * from Calculation
Go


CREATE PROC sp_UpdateGuestBill
(
@checkinid int,
@discount money,
@operator varchar (20)
)
AS
BEGIN
declare @id int set @id = (Select ID from Calculation Where CheckInID = @checkinid);

UPDATE Calculation SET Discount = @discount WHERE CheckInID = @checkinid

Insert into BillEditRecord(ID, CheckInID, DiscountAmount, EditBy)
VALUES (@id, @checkinid, @discount, @operator)
END
GO

--EXEC sp_UpdateGuestBill 101, 200
--GO





SELECT * FROM Calculation
GO


USE MyTestDB1
GO
CREATE TABLE BillPrint
(
ID int default 1,
CheckInID int null,
Name varchar (30) null,
CellPhone varchar (15) null,
RoomType varchar (20) null,
RoomNo varchar (10) null,
CheckInDate varchar (30) null,
CheckOutDate varchar (30) null,
TotalDay int null,
RoomCharge money default 0,
Tax money default 0.15,
ServiceCharge money default 0.10,
Discount money default 0,
TotalAmount money default 0
)
GO

INSERT INTO BillPrint Values(1,1, 'aaaa', 'bbbb', 'cccc', '100', 'eeee', 'ffff', 0, 0, 0, 0, 0, 0)
Go

Create PROC sp_TotalAmountCouple
(
@name varchar (30),
@cellphone varchar (15),
@roomtype varchar (20),
@roomno varchar (10),
@checkinid int,
@totalday int
)
AS
BEGIN
declare @checkindate datetime SET @checkindate = (Select CheckInDate from InOut Where CellPhone = @cellphone AND Name = @name);
declare @checkoutdate datetime SET @checkoutdate = GETDATE();
declare @roomcharge money Set @roomcharge = (Select Price from RoomType where RoomName = @roomtype);
INSERT INTO Calculation (CheckInID, Name, CellPhone, RoomType, RoomNo, CheckInDate, CheckOutDate,TotalDay, RoomCharge)
VALUES (@checkinid, @name, @cellphone, @roomtype, @roomno, @checkindate, @checkoutdate, @totalday, @roomcharge)
END
GO

--EXEC sp_TotalAmountCouple 'Towhid3', '8765432', 'Couple Room', '107', 1
--GO

Select * from Calculation
Go

select * from CheckIn
Go


--Bill Print For Couple Room

Create PROC sp_BillPrintCouple
(
@checkinid int
)
AS
BEGIN
declare @name varchar (30) SET @name = (Select Name from Calculation Where CheckInID = @checkinid);
declare @cellphone varchar (15) SET @cellphone = (Select CellPhone from Calculation Where CheckInID = @checkinid);
declare @roomtype varchar (20) SET @roomtype = (Select RoomType from Calculation where CheckInID = @checkinid);
declare @roomno varchar (10) SET @roomno = (Select RoomNo from Calculation where CheckInID = @checkinid);
declare @totalday int SET @totalday = (Select TotalDay from Calculation where CheckInID = @checkinid);
declare @checkindate datetime SET @checkindate = (Select CheckInDate from Calculation Where CheckInID = @checkinid);
declare @checkoutdate datetime SET @checkoutdate = GETDATE();
declare @roomcharge money Set @roomcharge = (Select RoomCharge from Calculation Where CheckInID = @checkinid);
declare @totalamount money Set @totalamount = (Select TotalAmount from Calculation where CheckInID = @checkinid);
declare @tax money Set @tax = (Select Tax from Calculation Where CheckInID = @checkinid);
declare @servicecharge money Set @servicecharge = (Select ServiceCharge from Calculation Where CheckInID = @checkinid);
declare @discount money Set @discount = (Select Discount from Calculation Where CheckInID = @checkinid);

UPDATE BillPrint SET
CheckInID = @checkinid,
Name = @name,
CellPhone = @cellphone,
RoomType = @roomtype,
RoomNo = @roomno,
CheckInDate = @checkindate,
CheckOutDate = @checkoutdate,
TotalDay = @totalday,
RoomCharge = @roomcharge,
Tax = @tax,
ServiceCharge = @servicecharge,
Discount = @discount,
TotalAmount = @totalamount
END
GO

EXEC sp_BillPrintCouple 102
Go



--Bill Print For Delux Room


Create PROC sp_BillPrintDelux
(
@checkinid int
)
AS
BEGIN
declare @name varchar (30) SET @name = (Select Name from Calculation Where CheckInID = @checkinid);
declare @cellphone varchar (15) SET @cellphone = (Select CellPhone from Calculation Where CheckInID = @checkinid);
declare @roomtype varchar (20) SET @roomtype = (Select RoomType from Calculation where CheckInID = @checkinid);
declare @roomno varchar (10) SET @roomno = (Select RoomNo from Calculation where CheckInID = @checkinid);
declare @totalday int SET @totalday = (Select TotalDay from Calculation where CheckInID = @checkinid);
declare @checkindate datetime SET @checkindate = (Select CheckInDate from Calculation Where CheckInID = @checkinid);
declare @checkoutdate datetime SET @checkoutdate = GETDATE();
declare @roomcharge money Set @roomcharge = (Select RoomCharge from Calculation Where CheckInID = @checkinid);
declare @totalamount money Set @totalamount = (Select TotalAmount from Calculation where CheckInID = @checkinid);
declare @tax money Set @tax = (Select Tax from Calculation Where CheckInID = @checkinid);
declare @servicecharge money Set @servicecharge = (Select ServiceCharge from Calculation Where CheckInID = @checkinid);
declare @discount money Set @discount = (Select Discount from Calculation Where CheckInID = @checkinid);

UPDATE BillPrint SET
CheckInID = @checkinid,
Name = @name,
CellPhone = @cellphone,
RoomType = @roomtype,
RoomNo = @roomno,
CheckInDate = @checkindate,
CheckOutDate = @checkoutdate,
TotalDay = @totalday,
RoomCharge = @roomcharge,
Tax = @tax,
ServiceCharge = @servicecharge,
Discount = @discount,
TotalAmount = @totalamount
END
GO



Select * from BillPrint
GO

SELECT * from Calculation
Go

Select * from RoomType
Go




-- For Check-Out==================================================
USE MyTestDB1
GO
CREATE PROC sp_CheckOut
(
@roomno int,
@cellphone varchar (30)
)
AS
BEGIN
declare @date datetime SET @date = GETDATE()
UPDATE InOut SET CheckOutDate = @date WHERE CellPhone = @cellphone  
DELETE FROM CheckIn WHERE RoomNo = @roomno
END
GO


Create PROC sp_CheckOutCoupleRoom
(
@roomno int,
@cellphone varchar (15)
)
AS
BEGIN
declare @date datetime SET @date = GETDATE()
UPDATE InOut SET CheckOutDate = @date WHERE CellPhone = @cellphone
UPDATE CoupleRoom SET S = 1 WHERE RoomNo = @roomno  
Update CheckIn SET IsActive = 0 Where CellPhone = @cellphone AND RoomNo = @roomno
END
GO


Create PROC sp_CheckOutDeluxSuit
(
@roomno int,
@cellphone varchar (15)
)
AS
BEGIN
declare @date datetime SET @date = GETDATE()
UPDATE DeluxRoom SET S = 1 WHERE RoomNo = @roomno 
UPDATE InOut SET CheckOutDate = @date WHERE CellPhone = @cellphone 
Update CheckIn SET IsActive = 0 Where CellPhone = @cellphone AND RoomNo = @roomno
END
GO

-- For Reservation Cancel================================
Create PROC sp_ReservationActionCancel
(
@roomno int
)
AS
BEGIN
UPDATE CoupleRoom SET S = 1 WHERE RoomNo = @roomno
UPDATE Reservation SET IsActive = 0 WHERE RoomNo = @roomno
END
GO


Create PROC sp_ReservationActionCancelDelux
(
@roomno int
)
AS
BEGIN
UPDATE DeluxRoom SET S = 1 WHERE RoomNo = @roomno
UPDATE Reservation SET IsActive = 0 WHERE RoomNo = @roomno
END
GO


create table NoRoom
(
RoomNo varchar (20)
)
Go

Insert into NoRoom VALUES ('Not Found');
GO

Select * from NoRoom
Go

SELECT * FROM CheckIn
GO

SELECT * FROM CoupleRoom
GO

SELECT * FROM InOut
GO


Select * from Calculation
Go

Select * from Reservation
Go

Select * from InOut
GO

Select * from BillPrint
Go

