--Dependency
--Scaffold-DbContext "Server=STILUX;Database=TOUS;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
--SELECT @@SERVERNAME
--Microsoft.EntityFrameworkCore
--Microsoft.EntityFrameworkCore.SqlServer


--tbl_user: bảng lưu người dùng bao gồm các thuộc tính như ID, Name,.... 
--Bảng không có khóa ngoại.
--tbl_permision: bảng chứa nhóm quyền hạn. bao gồm các thuộc tính, 
--ID nhóm quyền hạn, tên nhóm quyền hạn.
--tbl_permision_detail: là bảng sẽ chứa những quyền hạn cụ thể dành cho nhóm quyền hạn. 
--Trường action_name không cần thiết bạn có thể bỏ. 
--Trường action_code là để khi lập trình mình định nghĩa một thao tác nhất định 
--trong bằng code này ví dụ quyền sửa thì code nó là EDIT chẳng hạn.

--tbl_per_relationship: là bảng lưu mối liên hệ giữa người dùng và nhóm quyền hạn. 
--Mục đích của bảng này không phải là để một người dùng có nhiều nhóm quyền 
--mà để không phải truy vấn lại bảng user chứa thông tin nhạy cảm như username và password. 
--Bạn cũng có thể bỏ qua bảng này và liên hệ trực tiếp giữa bảng user và permision luôn, 
--nhưng mình khuyên bạn nên sử dụng thêm bảng này vì có nhiều trường hợp user có nhiều quyền hạn



CREATE DATABASE TOUS
Go
USE TOUS
Go
--Create User Table
CREATE TABLE [User] (
	Id INT NOT NULL IdENTITY(1,1),
	IsExist BIT NOT NULL DEFAULT 1,
	Username VARCHAR(200),
	[Password] VARCHAR(max), --encode with base 64 and mp5
	CONSTRAINT Pk_UserId PRIMARY KEY(Id),
	CONSTRAINT Uq_Username UNIQUE(Username)
)
GO
ALTER TABLE dbo.[User] ADD CONSTRAINT Ck_User_Password CHECK(DATALENGTH(Password)> 6) 
GO

-- Create UserDetail Table
CREATE TABLE UserDetail(
	Id INT NOT NULL IdENTITY(1,1),
	UserId INT NOT NULL,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	AvatarLink NVARCHAR(max), -- Format: ToUs_email_avatar (email just take the part before @)
	CONSTRAINT Pk_UserDetail PRIMARY KEY(Id)
)
Go
ALTER TABLE dbo.UserDetail ADD CONSTRAINT Fk_UserDetail_User 
FOREIGN KEY(UserId) REFERENCES dbo.[User](Id)
Go

-- Create Permission Table
CREATE TABLE Permission (
	Id INT NOT NULL IdENTITY(1,1),
	[Name] VARCHAR(10) NOT NULL
	CONSTRAINT Pk_Permission PRIMARY KEY(Id)
)
Go

--Create PermissionDetail Table
CREATE TABLE PermissionDetail(
	Id INT NOT NULL IdENTITY(1,1),
	PermissionId INT,
	ActionName VARCHAR(30),
	ActionCode VARCHAR(20),
	CheckAction BIT,
	CONSTRAINT Pk_PermissionDetail PRIMARY KEY(Id)
)
GO

ALTER TABLE dbo.PermissionDetail ADD CONSTRAINT Fk_PermissionDetail_Permission 
FOREIGN KEY(PermissionId) REFERENCES dbo.Permission(Id)
GO

--Create UserPermission Table
CREATE TABLE UserPermission(
	UserId INT NOT NULL,
	PermissionId INT NOT NULL,
	CONSTRAINT PK_UserPermission PRIMARY KEY(UserId,PermissionId)
)
GO

--Add foreign key to UserPermission
ALTER TABLE dbo.UserPermission ADD CONSTRAINT Fk_UserPermission_User 
FOREIGN KEY(UserId) REFERENCES dbo.[User](Id)
GO

ALTER TABLE dbo.UserPermission ADD CONSTRAINT Fk_UserPermission_Permission 
FOREIGN KEY(PermissionId) REFERENCES dbo.Permission(Id)
GO

--inser value to user table
INSERT INTO dbo.[User]
VALUES
(   1, -- IsExist - bit
    'user',  -- Email - varchar(50)
    null    -- Password - varchar(20)
    )
Go

INSERT INTO dbo.[User]
VALUES
(   0, -- IsExist - bit
    'tri@gmail.com',  -- Email - varchar(50)
    123456789   -- Password - varchar(20)
    )
GO

INSERT INTO dbo.[User]
VALUES
(   1, -- IsExist - bit
    'tung@gmail.com',   -- Email - varchar(50)
    '123456789'    -- Password - varchar(20)
    )
GO

INSERT INTO dbo.[User]
VALUES
(   1, -- IsExist - bit
    'tous@gmail.com',   -- Email - varchar(50)
    '0a6b9ed1057d5fb6eaab208988a66631'    -- Password - varchar(20)
    )
Go

--Add value to Permission table
-- if sign in
INSERT INTO dbo.Permission
VALUES
('Client' -- Name - varchar(10) 
    )
GO

-- if sign in
INSERT INTO dbo.Permission
VALUES
('User' -- Name - varchar(10)
    )
GO

--Add value to UserDetail Table
INSERT INTO dbo.UserDetail
VALUES
(   1,  -- UserId - int
    NULL, -- FirstName - varchar(20)
    NULL, -- LastName - varchar(20)
    '/ToUs/Resources/Avatar/ToUsClientAvatar'  -- AvatarLink - varchar(150)
    )
GO

INSERT INTO dbo.UserDetail
VALUES
(   1,  -- UserId - int 
    N'Trí', -- FirstName - varchar(20)
    N'Lê', -- LastName - varchar(20)
    NULL  -- AvatarLink - varchar(150)
    )
GO

INSERT INTO dbo.UserDetail
VALUES
(   1,  -- UserId - int
    N'Tùng', -- FirstName - varchar(20)
    N'Trần', -- LastName - varchar(20)
    '/ToUs/Resources/Avatar/ToUs_tung_avatar'  -- AvatarLink - varchar(150)
    )
GO


--Add value to PermissionDetail
INSERT INTO dbo.PermissionDetail
VALUES
(   1,   -- PermissionId - int
    'Create Multible Boards',  -- ActionName - varchar(30)
    'Create',  -- ActionCode - varchar(20)
    0 -- CheckAction - bit
    )
GO

INSERT INTO dbo.PermissionDetail
VALUES
(   2,   -- PermissionId - int
    'Create Multible Boards',  -- ActionName - varchar(30)
    'Create',  -- ActionCode - varchar(20)
    1 -- CheckAction - bit
    )
GO

--Add value to UserPermission 
INSERT INTO dbo.UserPermission
VALUES
(   1, -- UserId - int
    1  -- PermissionId - int
    )
GO

INSERT INTO dbo.UserPermission
VALUES
(   2, -- UserId - int
    2  -- PermissionId - int
    )
GO

INSERT INTO dbo.UserPermission
VALUES
(   3, -- UserId - int
    2 -- PermissionId - int
    )
GO

CREATE TABLE [Subject](
	Id VARCHAR(10) NOT NULL,
	[Name] NVARCHAR(MAX),
	NumberOfDigits INT,
	HTGD VARCHAR(10),
	Faculity VARCHAR(10),
	IsLab BIT,
	CONSTRAINT PK_Subject PRIMARY KEY(Id)
)
GO

CREATE TABLE [Teacher](
	Id VARCHAR(20) NOT NULL,
	[Name] NVARCHAR(MAX),
	CONSTRAINT Pk_Teacher PRIMARY KEY(Id)
)
Go

CREATE TABLE Class(
	Id VARCHAR(30) NOT NULL,
	NumberOfStudents INT,
	Room VARCHAR(20),
	DayInWeek VARCHAR(20),
	Lession VARCHAR(50), 
	Frequency INT, 
	[System] VARCHAR(10),
	Semester INT, 
	[Year] INT, 
	Note NVARCHAR(MAX),
	BeginDate DATE, 
	EndDate DATE,
	[Language] CHAR(2),
	CONSTRAINT Pk_Class PRIMARY KEY(Id)
)
GO

CREATE TABLE SubjectManager(
	Id INT NOT NULL IdENTITY(1,1),
	SubjectId VARCHAR(10) NOT NULL,
	TeacherId VARCHAR(20) NULL,
	ClassId VARCHAR(30) NOT NULL,
	IsDelete BIT NOT NULL DEFAULT 0,
	ExcelPath NVARCHAR(max)
	CONSTRAINT Pk_SubjectManager PRIMARY KEY(Id)
)
GO


ALTER TABLE dbo.SubjectManager ADD CONSTRAINT Fk_SubjectManager_Subject 
FOREIGN KEY(SubjectId) REFERENCES dbo.Subject(Id)
GO

ALTER TABLE dbo.SubjectManager ADD CONSTRAINT Fk_SubjectManager_Teacher 
FOREIGN KEY(TeacherId) REFERENCES dbo.Teacher(Id)
GO

ALTER TABLE dbo.SubjectManager ADD CONSTRAINT Fk_SubjectManager_Class 
FOREIGN KEY(ClassId) REFERENCES dbo.Class(Id)
GO


CREATE TABLE TimeTable(
	Id INT NOT NULL,
	UserDetailId INT NOT NULL,
	[Name] NVARCHAR(100),
	CONSTRAINT Pk_TimeTable PRIMARY KEY(Id)
)
GO

ALTER TABLE dbo.TimeTable ADD CONSTRAINT Fk_TimeTable_UserDetail 
FOREIGN KEY(UserDetailId) REFERENCES dbo.UserDetail(Id)
GO


CREATE TABLE TableManager(
	TableId INT NOT NULL,
	SubjectManagerId INT NOT NULL,
	CONSTRAINT Pk_TableManager PRIMARY KEY(TableId,SubjectManagerId)
)
GO



ALTER TABLE dbo.TableManager ADD CONSTRAINT Fk_TableManager_TimeTable
FOREIGN KEY(TableId) REFERENCES dbo.TimeTable(Id)
GO

ALTER TABLE dbo.TableManager ADD CONSTRAINT Fk_TableManager_SubjectManager
FOREIGN KEY(SubjectManagerId) REFERENCES dbo.SubjectManager(Id)
GO



--Relation of user
SELECT * FROM dbo.[User] --done
GO

SELECT * FROM dbo.UserDetail --done
GO

SELECT * FROM dbo.UserPermission
GO

SELECT * FROM dbo.Permission --done
GO

SELECT * FROM dbo.PermissionDetail
GO



--Relation of subject
SELECT * FROM dbo.Subject
GO

SELECT * FROM dbo.Teacher
GO

SELECT * FROM dbo.Class
GO

SELECT * FROM dbo.SubjectManager
Go

DELETE FROM dbo.SubjectManager
Go
DELETE FROM dbo.Subject
Go
DELETE FROM dbo.Teacher
Go
DELETE FROM dbo.Class
Go


--BEGIN
--	SELECT dbo.Subject.*, @@ROWCOUNT TOUS FROM dbo.SubjectManager
--	JOIN dbo.Subject ON Subject.Id = SubjectManager.SubjectId
--	JOIN dbo.Class ON Class.Id = SubjectManager.ClassId
--	LEFT JOIN dbo.Teacher ON Teacher.Id = SubjectManager.TeacherId
--	WHERE dbo.Subject.Name LIKE N'Đồ án 1'
	
--END