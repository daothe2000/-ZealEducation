CREATE DATABASE ZealEducationMgtDb

GO
USE ZealEducationMgtDb

GO
CREATE TABLE Parent
(
ParentID int identity(1,1) primary key,
ParentName nvarchar(50) not null,
Phone varchar(11) not null,
Address nvarchar(200),
Gender bit not null,
Password nvarchar(200) not null,
Email nvarchar(50),
)
GO

CREATE TABLE RelParentStudent
(
Id int identity(1,1) primary key,
ParentID int not null,
StudentID nvarchar(15) not null,
RelationWithStudent nvarchar(200)
)
go
CREATE TABLE Student
(
StudentID nvarchar(15) primary key not null,
StudentName nvarchar(50) not null,
Phone varchar(11) not null,
Address nvarchar(200),
Gender bit  not null,
Password nvarchar(200) not null,
Email nvarchar(60),
Image nvarchar(200),
Birthday date not null,
Status bit,
Educationlevel nvarchar(300),

)
GO


CREATE TABLE Faculty
(
FacultyID nvarchar(15) primary key not null,
FacultyName nvarchar(50) not null,
description ntext
)
GO

CREATE TABLE Teacher
(
TeacherID nvarchar(15) primary key not null,
TeacherName nvarchar(50) not null,
Phone varchar(11) not null,
Address nvarchar(200) not null,
Gender bit not null,
Password nvarchar(200) not null,
Email nvarchar(50) not null,
Image nvarchar(200) not null,
Birthday date not null,
Status bit not null,
Nation nvarchar(300),
Degree nvarchar(300),
FacultyID nvarchar(15) not null,

)
GO

CREATE TABLE GradeType
(
GradeTypeID int identity(1,1) primary key,
GradeTypeName nvarchar(50) not null,
GradeMaximum DECIMAL(5,2)  not null,
GradeFail DECIMAL(5,2)  not null,
note ntext
)
GO

CREATE TABLE Grade
(
GradeID int Identity(1,1) primary key,
StudentID nvarchar(15) not null,
SubjectID nvarchar(15) not null,
ExamNumber int,
GradeOfNumber DECIMAL(5,2) not null,
note ntext,
GradeTypeID int not null,
Status bit
)
GO
CREATE TABLE Subject
(
SubjectID nvarchar(15) primary key not null,
SubjectName nvarchar(50) not null,
Abbreviation nvarchar(30),
lesson int ,
price float,
Status bit,
FacultyID nvarchar(15) not null
)
GO


CREATE TABLE MultiSubjectTeacher
(
ID int Identity(1,1) primary key,
TeacherID nvarchar(15) not null,
SubjectID nvarchar(15) not null
)
GO

CREATE TABLE Class
(
ClassID nvarchar(15) primary key not null,
ClassName nvarchar(50) not null,
Status bit,
FacultyID nvarchar(15) not null,
StuQuantityMax int
)
GO

CREATE TABLE ClassMember
(
ID int Identity(1,1) primary key,
ClassID nvarchar(15) not null,
StudentID nvarchar(15) not null,
Status bit
)
GO

CREATE TABLE ClassSubject
(
ID int Identity(1,1) primary key,
ClassID nvarchar(15) not null,
SubjectID nvarchar(15) not null,
Status bit
)
GO

CREATE TABLE TimeTable
(
TimeID int identity(1,1) primary key,
ClassID nvarchar(15) not null,
TeacherID nvarchar(15) not null,
SubjectID nvarchar(15) not null,
TimeIn time not null,
TimeOut time not null,
Day date not null,
RoomId int not null,
IsAcvite bit,
Isfinished bit
)
GO

CREATE TABLE ExamSchedule
(
ExamScheduleID int identity(1,1) primary key,
ClassId nvarchar(15) not null,
SubjectID nvarchar(15) not null,
Examday date not null,
TimeIn time not null,
TimeOut time not null,
Note nvarchar(100),
RoomID int not null,
status bit not null
)
GO
CREATE TABLE Attendance
(
AttendanceID int identity(1,1) primary key,
StudentID nvarchar(15) not null,
TimeID int not null,
AttendanceTypeID int not null,
Note nvarchar(100),
timeIn time not null
)
GO

CREATE TABLE AttendanceType
(
AttendanceTypeID int identity(1,1) primary key,
AttendanceTypeName nvarchar(50),
Description ntext
)
GO


CREATE TABLE Room
(
RoomID int identity(1,1) primary key,
RoomName nvarchar(50) not null unique,
Status bit not null
)
GO

CREATE TABLE Fees
(
FeesID int identity(1,1) primary key,
FeesDate date,
TotalPrice float,
Status bit
)
GO




CREATE TABLE FeesStudent
(
Id int primary key identity,
FeesID int not null FOREIGN KEY (FeesID) REFERENCES Fees(FeesID),
StudentID nvarchar(15) FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
)
GO

CREATE TABLE FeesDetail
(
FeesID int not null,
SubjectID nvarchar(15) not null,
CONSTRAINT FeesDetailpk PRIMARY KEY (FeesID, SubjectID),
price float
)
GO
CREATE TABLE Staff
(
StaffID nvarchar(15) primary key not null,
Password nvarchar(100),
Phone varchar(11) not null,
Address nvarchar(200) not null,
Gender bit not null,
Email nvarchar(50) not null,
Image nvarchar(200) not null,
Birthday date not null,
Status bit

)
GO

CREATE TABLE Event
(
EventID int identity(1,1) primary key,
EventName nvarchar(100) not null,
startDate date not null,
endDate date not null,
note nvarchar(200),
status bit not null
)
Go
CREATE TABLE Feedback
(
FeedbackID int identity(1,1) primary key,
StudentID nvarchar(15) not null,
description ntext
)
GO
USE [ZealEducationMgtDb]
GO
SET IDENTITY_INSERT [dbo].[Parent] ON 

INSERT [dbo].[Parent] ([ParentID], [ParentName], [Phone], [Address], [Gender], [Password], [Email]) VALUES (1, N'Thomas Edison', N'0398632695', N'Ha Noi', 1, N'e10adc3949ba59abbe56e057f20f883e', N'anhtungagg@gmail.com')
INSERT [dbo].[Parent] ([ParentID], [ParentName], [Phone], [Address], [Gender], [Password], [Email]) VALUES (2, N'Thomas Tunad', N'0386239547', N'Bac Giang', 1, N'e10adc3949ba59abbe56e057f20f883e', N'anhtungagg11@gmail.com')
INSERT [dbo].[Parent] ([ParentID], [ParentName], [Phone], [Address], [Gender], [Password], [Email]) VALUES (3, N'Bofd Boa', N'0398655452', N'Ha Noi', 0, N'e10adc3949ba59abbe56e057f20f883e', N'anhtunga55@gmail.com')
SET IDENTITY_INSERT [dbo].[Parent] OFF
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0001', N'Brade De Lasci', N'8772044675', N'21 Londonderry Drive', 1, N'123456', N'bde0@hubpages.com', N'student2.png', CAST(N'1997-10-26' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0002', N'Beverlee Haysham', N'9919577734', N'81813 5th Way', 0, N'123456', N'bhaysham1@squarespace.com', N'student3.png', CAST(N'1997-10-02' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0003', N'Shayne Tether', N'5944110614', N'7411 Hovde Park', 0, N'123456', N'stether2@yellowpages.com', N'student4.png', CAST(N'1999-08-06' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0004', N'Alayne McMichan', N'9183645289', N'14 Forest Dale Street', 0, N'123456', N'amcmichan3@wordpress.com', N'student5.png', CAST(N'1999-02-06' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0005', N'Hynda Hymor', N'1748650366', N'74773 Buena Vista Center', 1, N'123456', N'hhymor4@cyberchimps.com', N'student6.png', CAST(N'1997-11-24' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0006', N'Josie Doull', N'8356856722', N'40115 Oak Alley', 1, N'123456', N'jdoull5@youku.com', N'student7.png', CAST(N'1999-09-14' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0007', N'Cosmo Juckes', N'6956071774', N'355 Corben Way', 1, N'123456', N'cjuckes6@dot.gov', N'student8.png', CAST(N'1996-01-27' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0008', N'Juditha Pringour', N'3512732419', N'5707 Alpine Road', 1, N'123456', N'jpringour7@blogs.com', N'student9.png', CAST(N'1996-11-24' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0009', N'Darren Shaddick', N'7415552406', N'39364 Bashford Alley', 0, N'123456', N'dshaddick8@prnewswire.com', N'student10.png', CAST(N'1998-06-28' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0010', N'Kaycee Borless', N'7943846577', N'905 Mendota Alley', 0, N'123456', N'kborless9@cbc.ca', N'student1.jpg', CAST(N'1997-08-12' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0011', N'Randie Kettlestringes', N'6128873376', N'4685 Cherokee Drive', 0, N'123456', N'rkettlestringesa@hubpages.com', N'student12.png', CAST(N'1999-07-18' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0012', N'Sena Garcia', N'1649139499', N'63780 Del Mar Road', 1, N'123456', N'sgarciab@freewebs.com', N'student13.png', CAST(N'1998-10-03' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0013', N'Thorndike Edleston', N'4912405146', N'6247 West Alley', 0, N'123456', N'tedlestonc@over-blog.com', N'student8.png', CAST(N'1999-01-05' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0014', N'Hannah Goter', N'9179400662', N'7685 Ruskin Center', 0, N'123456', N'hgoterd@scribd.com', N'student6.png', CAST(N'1998-05-12' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0015', N'Noach Joppich', N'3296905653', N'51 Blaine Junction', 1, N'123456', N'njoppiche@instagram.com', N'student4.png', CAST(N'1997-10-26' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0016', N'Rosaline Menis', N'7689963497', N'5477 Drewry Center', 0, N'123456', N'rmenisf@discuz.net', N'student2.png', CAST(N'1996-04-10' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0017', N'Tyler Acuna', N'5682132284', N'094 Lyons Way', 0, N'123456', N'tacunag@sciencedaily.com', N'student5.png', CAST(N'1999-07-22' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0018', N'Olympie Twinbourne', N'9499364963', N'73693 Farragut Avenue', 1, N'123456', N'otwinbourneh@goo.gl', N'student7.png', CAST(N'1999-03-14' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0019', N'Jamaal Size', N'8438924498', N'29529 Kingsford Point', 0, N'123456', N'jsizei@hhs.gov', N'student9.png', CAST(N'1999-07-08' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0020', N'Sheeree Lillyman', N'7961552906', N'45 Independence Road', 1, N'123456', N'slillymanj@lycos.com', N'student11.png', CAST(N'1997-12-20' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0021', N'Alis Tuke', N'8434202479', N'95595 Gina Hill', 1, N'123456', N'atuke0@soundcloud.com', N'student13.png', CAST(N'1998-09-18' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0022', N'Gradey Skelhorne', N'2161252642', N'9835 Roxbury Hill', 1, N'123456', N'gskelhorne1@ask.com', N'student5.png', CAST(N'1998-11-11' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0023', N'Ricard Lorait', N'4674896562', N'0285 Hayes Avenue', 1, N'123456', N'rlorait2@people.com.cn', N'student10.png', CAST(N'1998-08-20' AS Date), 1, N'High school')
INSERT [dbo].[Student] ([StudentID], [StudentName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Educationlevel]) VALUES (N'A0024', N'Duc le', N'0398332697', N'bac giang', 1, N'12345678', N'anhtungagg55@gmail.com', N'student1.jpg', CAST(N'2000-04-14' AS Date), 1, N'a')
SET IDENTITY_INSERT [dbo].[RelParentStudent] ON 

INSERT [dbo].[RelParentStudent] ([Id], [ParentID], [StudentID], [RelationWithStudent]) VALUES (1, 1, N'A0001', N'')
INSERT [dbo].[RelParentStudent] ([Id], [ParentID], [StudentID], [RelationWithStudent]) VALUES (2, 1, N'A0002', N'')
INSERT [dbo].[RelParentStudent] ([Id], [ParentID], [StudentID], [RelationWithStudent]) VALUES (3, 2, N'A0004', N'')
INSERT [dbo].[RelParentStudent] ([Id], [ParentID], [StudentID], [RelationWithStudent]) VALUES (4, 2, N'A0005', N'')
SET IDENTITY_INSERT [dbo].[RelParentStudent] OFF
SET IDENTITY_INSERT [dbo].[GradeType] ON 

INSERT [dbo].[GradeType] ([GradeTypeID], [GradeTypeName], [GradeMaximum], [GradeFail], [note]) VALUES (1, N'1', CAST(20.00 AS Decimal(5, 2)), CAST(8.00 AS Decimal(5, 2)), NULL)
SET IDENTITY_INSERT [dbo].[GradeType] OFF
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [description]) VALUES (N'F0001', N'Computer Science', N'Computer science is the study of computers and computing as well as their theoretical and practical applications.')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [description]) VALUES (N'F0002', N'Information Technology', N'nformation technology (IT) is the use of any computers, storage, networking and other physical devices')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub001', N'Subject1', N'10', 10, 100, 1, N'F0001')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub002', N'Subject2', N'10', 9, 100, 1, N'F0001')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub003', N'Subject3', N'10', 9, 111, 1, N'F0001')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub004', N'Subject4', N'10', 11, 118, 1, N'F0002')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub005', N'Subject5', N'10', 10, 108, 1, N'F0002')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [Abbreviation], [lesson], [price], [Status], [FacultyID]) VALUES (N'Sub006', N'Subject6', N'10', 10, 127, 1, N'F0002')
INSERT [dbo].[Class] ([ClassID], [ClassName], [Status], [FacultyID], [StuQuantityMax]) VALUES (N'Class001', N'A1801A1', 1, N'F0001', 22)
INSERT [dbo].[Class] ([ClassID], [ClassName], [Status], [FacultyID], [StuQuantityMax]) VALUES (N'Class002', N'A1801A2', 1, N'F0002', 23)
INSERT [dbo].[Class] ([ClassID], [ClassName], [Status], [FacultyID], [StuQuantityMax]) VALUES (N'Class003', N'A1801A3', NULL, N'F0001', 20)
SET IDENTITY_INSERT [dbo].[ClassMember] ON 

INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (1, N'Class001', N'A0001', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (2, N'Class001', N'A0002', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (3, N'Class001', N'A0003', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (4, N'Class001', N'A0004', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (5, N'Class001', N'A0005', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (6, N'Class001', N'A0006', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (7, N'Class001', N'A0007', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (8, N'Class001', N'A0008', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (9, N'Class001', N'A0009', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (10, N'Class001', N'A0010', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (11, N'Class001', N'A0011', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (12, N'Class001', N'A0012', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (13, N'Class001', N'A0013', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (14, N'Class001', N'A0014', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (15, N'Class001', N'A0015', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (16, N'Class001', N'A0016', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (17, N'Class001', N'A0017', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (18, N'Class001', N'A0018', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (19, N'Class001', N'A0019', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (20, N'Class001', N'A0020', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (21, N'Class001', N'A0021', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (22, N'Class001', N'A0022', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (23, N'Class002', N'A0023', NULL)
INSERT [dbo].[ClassMember] ([ID], [ClassID], [StudentID], [Status]) VALUES (24, N'Class003', N'A0024', NULL)
SET IDENTITY_INSERT [dbo].[ClassMember] OFF
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Nation], [Degree], [FacultyID]) VALUES (N'1', N'Munmro Clipson', N'1476440873', N'60 Muir Way', 1, N'e10adc3949ba59abbe56e057f20f883e', N'teacher1@gmail.com', N'/Uploads/images/parents.jpg', CAST(N'1975-06-08' AS Date), 1, NULL, NULL, N'F0001')
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Nation], [Degree], [FacultyID]) VALUES (N'2', N'Johna Peaker', N'4076902058', N'15 Alpine Park', 0, N'e10adc3949ba59abbe56e057f20f883e', N'teacher2@gmail.com', N'/Uploads/images/student1.jpg', CAST(N'1981-02-04' AS Date), 1, NULL, NULL, N'F0002')
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Nation], [Degree], [FacultyID]) VALUES (N'B005', N'Christopher', N'0981268863', N'Ha Noi', 1, N'e10adc3949ba59abbe56e057f20f883e', N'anhtu2k@gmail.com', N'/Uploads/images/student1.jpg', CAST(N'1980-02-01' AS Date), 1, NULL, NULL, N'F0002')
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Nation], [Degree], [FacultyID]) VALUES (N'T003', N'Thoma Trump', N'0398336697', N'bac giang', 1, N'e10adc3949ba59abbe56e057f20f883e', N'anhtungagg@gmail.com', N'/Uploads/images/parents.jpg', CAST(N'1985-04-14' AS Date), 1, NULL, NULL, N'F0001')
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Phone], [Address], [Gender], [Password], [Email], [Image], [Birthday], [Status], [Nation], [Degree], [FacultyID]) VALUES (N'T004', N'Tom Hiddleston', N'0981298635', N'Ha Noi', 1, N'e10adc3949ba59abbe56e057f20f883e', N'anhtungagg4478@gmail.com', N'/Uploads/images/parents.jpg', CAST(N'1986-09-14' AS Date), 1, NULL, NULL, N'F0002')
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([RoomID], [RoomName], [Status]) VALUES (1, N'P1', 1)
INSERT [dbo].[Room] ([RoomID], [RoomName], [Status]) VALUES (3, N'P2', 1)
INSERT [dbo].[Room] ([RoomID], [RoomName], [Status]) VALUES (4, N'D1', 1)
INSERT [dbo].[Room] ([RoomID], [RoomName], [Status]) VALUES (5, N'D2', 1)
SET IDENTITY_INSERT [dbo].[Room] OFF
SET IDENTITY_INSERT [dbo].[TimeTable] ON 

INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (1, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-05' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (2, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-06' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (3, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-07' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (4, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-08' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (5, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-09' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (6, N'Class001', N'1', N'Sub001', CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2021-11-10' AS Date), 1, 1, 1)
INSERT [dbo].[TimeTable] ([TimeID], [ClassID], [TeacherID], [SubjectID], [TimeIn], [TimeOut], [Day], [RoomId], [IsAcvite], [Isfinished]) VALUES (7, N'Class001', N'1', N'Sub001', CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time), CAST(N'2021-11-04' AS Date), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TimeTable] OFF
SET IDENTITY_INSERT [dbo].[AttendanceType] ON 

INSERT [dbo].[AttendanceType] ([AttendanceTypeID], [AttendanceTypeName], [Description]) VALUES (1, N'Present', N'be present in the classroom')
INSERT [dbo].[AttendanceType] ([AttendanceTypeID], [AttendanceTypeName], [Description]) VALUES (2, N'Absent', N'absent from class while studying')
INSERT [dbo].[AttendanceType] ([AttendanceTypeID], [AttendanceTypeName], [Description]) VALUES (3, N'Excused Absence', N'Bbsent from class but have a reason')
SET IDENTITY_INSERT [dbo].[AttendanceType] OFF
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (1, N'A0001', 7, 1, N'', CAST(N'11:37:36.2051790' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (2, N'A0002', 7, 2, N'', CAST(N'11:37:36.3934547' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (3, N'A0003', 7, 1, N'', CAST(N'11:37:36.3974620' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (4, N'A0004', 7, 1, N'', CAST(N'11:37:36.3994528' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (5, N'A0005', 7, 1, N'', CAST(N'11:37:36.4014337' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (6, N'A0006', 7, 1, N'', CAST(N'11:37:36.4034283' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (7, N'A0007', 7, 1, N'', CAST(N'11:37:36.4054243' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (8, N'A0008', 7, 1, N'', CAST(N'11:37:36.4074195' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (9, N'A0009', 7, 1, N'', CAST(N'11:37:36.4094239' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (10, N'A0010', 7, 1, N'', CAST(N'11:37:36.4114069' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (11, N'A0011', 7, 1, N'', CAST(N'11:37:36.4124235' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (12, N'A0012', 7, 1, N'', CAST(N'11:37:36.4144221' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (13, N'A0013', 7, 1, N'', CAST(N'11:37:36.4174124' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (14, N'A0014', 7, 1, N'', CAST(N'11:37:36.4185312' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (15, N'A0015', 7, 1, N'', CAST(N'11:37:36.4205282' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (16, N'A0016', 7, 1, N'', CAST(N'11:37:36.4223044' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (17, N'A0017', 7, 2, N'', CAST(N'11:37:36.4236667' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (18, N'A0018', 7, 1, N'', CAST(N'11:37:36.4246669' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (19, N'A0019', 7, 1, N'', CAST(N'11:37:36.4258856' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (20, N'A0020', 7, 2, N'', CAST(N'11:37:36.4278835' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (21, N'A0021', 7, 1, N'', CAST(N'11:37:36.4288803' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (22, N'A0022', 7, 1, N'', CAST(N'11:37:36.4298775' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (23, N'A0001', 1, 1, N'', CAST(N'11:38:01.1790650' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (24, N'A0002', 1, 3, N'', CAST(N'11:38:01.1830569' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (25, N'A0003', 1, 1, N'', CAST(N'11:38:01.1850810' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (26, N'A0004', 1, 1, N'', CAST(N'11:38:01.1860769' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (27, N'A0005', 1, 1, N'', CAST(N'11:38:01.1880402' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (28, N'A0006', 1, 3, N'', CAST(N'11:38:01.1900499' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (29, N'A0007', 1, 1, N'', CAST(N'11:38:01.1920877' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (30, N'A0008', 1, 1, N'', CAST(N'11:38:01.1930863' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (31, N'A0009', 1, 1, N'', CAST(N'11:38:01.1960843' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (32, N'A0010', 1, 1, N'', CAST(N'11:38:01.1995693' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (33, N'A0011', 1, 1, N'', CAST(N'11:38:01.2011002' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (34, N'A0012', 1, 1, N'', CAST(N'11:38:01.2040958' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (35, N'A0013', 1, 1, N'', CAST(N'11:38:01.2060698' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (36, N'A0014', 1, 1, N'', CAST(N'11:38:01.2080454' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (37, N'A0015', 1, 1, N'', CAST(N'11:38:01.2110403' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (38, N'A0016', 1, 1, N'', CAST(N'11:38:01.2132290' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (39, N'A0017', 1, 1, N'', CAST(N'11:38:01.2151176' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (40, N'A0018', 1, 1, N'', CAST(N'11:38:01.2170952' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (41, N'A0019', 1, 1, N'', CAST(N'11:38:01.2200878' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (42, N'A0020', 1, 1, N'', CAST(N'11:38:01.2220830' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (43, N'A0021', 1, 1, N'', CAST(N'11:38:01.2241075' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (44, N'A0022', 1, 1, N'', CAST(N'11:38:01.2269683' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (45, N'A0001', 2, 1, N'', CAST(N'11:38:18.7398924' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (46, N'A0002', 2, 3, N'', CAST(N'11:38:18.7439153' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (47, N'A0003', 2, 1, N'', CAST(N'11:38:18.7468755' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (48, N'A0004', 2, 1, N'', CAST(N'11:38:18.7498675' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (49, N'A0005', 2, 3, N'', CAST(N'11:38:18.7518592' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (50, N'A0006', 2, 1, N'', CAST(N'11:38:18.7548530' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (51, N'A0007', 2, 1, N'', CAST(N'11:38:18.7578451' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (52, N'A0008', 2, 1, N'', CAST(N'11:38:18.7598469' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (53, N'A0009', 2, 3, N'', CAST(N'11:38:18.7618328' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (54, N'A0010', 2, 1, N'', CAST(N'11:38:18.7648403' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (55, N'A0011', 2, 1, N'', CAST(N'11:38:18.7668198' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (56, N'A0012', 2, 1, N'', CAST(N'11:38:18.7688151' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (57, N'A0013', 2, 1, N'', CAST(N'11:38:18.7708226' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (58, N'A0014', 2, 1, N'', CAST(N'11:38:18.7728040' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (59, N'A0015', 2, 1, N'', CAST(N'11:38:18.7757969' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (60, N'A0016', 2, 1, N'', CAST(N'11:38:18.7777903' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (61, N'A0017', 2, 1, N'', CAST(N'11:38:18.7798096' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (62, N'A0018', 2, 1, N'', CAST(N'11:38:18.7817797' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (63, N'A0019', 2, 1, N'', CAST(N'11:38:18.7837752' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (64, N'A0020', 2, 1, N'', CAST(N'11:38:18.7857683' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (65, N'A0021', 2, 1, N'', CAST(N'11:38:18.7877638' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (66, N'A0022', 2, 1, N'', CAST(N'11:38:18.7897581' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (67, N'A0001', 3, 1, N'', CAST(N'11:38:25.4785398' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (68, N'A0002', 3, 1, N'', CAST(N'11:38:25.4825285' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (69, N'A0003', 3, 1, N'', CAST(N'11:38:25.4845232' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (70, N'A0004', 3, 1, N'', CAST(N'11:38:25.4865182' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (71, N'A0005', 3, 1, N'', CAST(N'11:38:25.4885135' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (72, N'A0006', 3, 1, N'', CAST(N'11:38:25.4895365' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (73, N'A0007', 3, 1, N'', CAST(N'11:38:25.4932810' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (74, N'A0008', 3, 1, N'', CAST(N'11:38:25.4961008' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (75, N'A0009', 3, 1, N'', CAST(N'11:38:25.4986195' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (76, N'A0010', 3, 1, N'', CAST(N'11:38:25.5003033' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (77, N'A0011', 3, 1, N'', CAST(N'11:38:25.5022969' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (78, N'A0012', 3, 1, N'', CAST(N'11:38:25.5052903' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (79, N'A0013', 3, 1, N'', CAST(N'11:38:25.5083081' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (80, N'A0014', 3, 1, N'', CAST(N'11:38:25.5102753' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (81, N'A0015', 3, 1, N'', CAST(N'11:38:25.5124493' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (82, N'A0016', 3, 1, N'', CAST(N'11:38:25.5144578' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (83, N'A0017', 3, 1, N'', CAST(N'11:38:25.5164593' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (84, N'A0018', 3, 1, N'', CAST(N'11:38:25.5184363' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (85, N'A0019', 3, 1, N'', CAST(N'11:38:25.5203909' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (86, N'A0020', 3, 1, N'', CAST(N'11:38:25.5224039' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (87, N'A0021', 3, 1, N'', CAST(N'11:38:25.5240822' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (88, N'A0022', 3, 1, N'', CAST(N'11:38:25.5261024' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (89, N'A0001', 4, 1, N'', CAST(N'11:38:34.1695117' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (90, N'A0002', 4, 1, N'', CAST(N'11:38:34.1725290' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (91, N'A0003', 4, 1, N'', CAST(N'11:38:34.1754960' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (92, N'A0004', 4, 1, N'', CAST(N'11:38:34.1764937' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (93, N'A0005', 4, 1, N'', CAST(N'11:38:34.1785135' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (94, N'A0006', 4, 1, N'', CAST(N'11:38:34.1806014' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (95, N'A0007', 4, 1, N'', CAST(N'11:38:34.1821781' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (96, N'A0008', 4, 1, N'', CAST(N'11:38:34.1851901' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (97, N'A0009', 4, 1, N'', CAST(N'11:38:34.1881745' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (98, N'A0010', 4, 1, N'', CAST(N'11:38:34.1912660' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (99, N'A0011', 4, 1, N'', CAST(N'11:38:34.1938148' AS Time))
GO
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (100, N'A0012', 4, 1, N'', CAST(N'11:38:34.1958303' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (101, N'A0013', 4, 1, N'', CAST(N'11:38:34.1978069' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (102, N'A0014', 4, 1, N'', CAST(N'11:38:34.2007990' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (103, N'A0015', 4, 1, N'', CAST(N'11:38:34.2027971' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (104, N'A0016', 4, 1, N'', CAST(N'11:38:34.2047879' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (105, N'A0017', 4, 1, N'', CAST(N'11:38:34.2077799' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (106, N'A0018', 4, 1, N'', CAST(N'11:38:34.2097743' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (107, N'A0019', 4, 1, N'', CAST(N'11:38:34.2107720' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (108, N'A0020', 4, 1, N'', CAST(N'11:38:34.2127664' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (109, N'A0021', 4, 1, N'', CAST(N'11:38:34.2147617' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (110, N'A0022', 4, 1, N'', CAST(N'11:38:34.2177550' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (111, N'A0001', 5, 1, N'', CAST(N'11:38:47.6080846' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (112, N'A0002', 5, 1, N'', CAST(N'11:38:47.6130713' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (113, N'A0003', 5, 1, N'', CAST(N'11:38:47.6150910' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (114, N'A0004', 5, 1, N'', CAST(N'11:38:47.6170780' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (115, N'A0005', 5, 1, N'', CAST(N'11:38:47.6190546' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (116, N'A0006', 5, 1, N'', CAST(N'11:38:47.6200518' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (117, N'A0007', 5, 1, N'', CAST(N'11:38:47.6220460' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (118, N'A0008', 5, 1, N'', CAST(N'11:38:47.6250384' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (119, N'A0009', 5, 1, N'', CAST(N'11:38:47.6280294' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (120, N'A0010', 5, 1, N'', CAST(N'11:38:47.6313850' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (121, N'A0011', 5, 1, N'', CAST(N'11:38:47.6330302' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (122, N'A0012', 5, 1, N'', CAST(N'11:38:47.6350249' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (123, N'A0013', 5, 1, N'', CAST(N'11:38:47.6380178' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (124, N'A0014', 5, 1, N'', CAST(N'11:38:47.6400651' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (125, N'A0015', 5, 1, N'', CAST(N'11:38:47.6420619' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (126, N'A0016', 5, 1, N'', CAST(N'11:38:47.6439945' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (127, N'A0017', 5, 1, N'', CAST(N'11:38:47.6459919' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (128, N'A0018', 5, 1, N'', CAST(N'11:38:47.6477135' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (129, N'A0019', 5, 1, N'', CAST(N'11:38:47.6497103' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (130, N'A0020', 5, 1, N'', CAST(N'11:38:47.6515006' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (131, N'A0021', 5, 1, N'', CAST(N'11:38:47.6525329' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (132, N'A0022', 5, 1, N'', CAST(N'11:38:47.6545296' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (133, N'A0001', 6, 1, N'', CAST(N'11:38:58.9357606' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (134, N'A0002', 6, 1, N'', CAST(N'11:38:58.9397493' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (135, N'A0003', 6, 1, N'', CAST(N'11:38:58.9417493' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (136, N'A0004', 6, 1, N'', CAST(N'11:38:58.9427416' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (137, N'A0005', 6, 1, N'', CAST(N'11:38:58.9447499' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (138, N'A0006', 6, 1, N'', CAST(N'11:38:58.9500533' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (139, N'A0007', 6, 1, N'', CAST(N'11:38:58.9527459' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (140, N'A0008', 6, 1, N'', CAST(N'11:38:58.9547443' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (141, N'A0009', 6, 1, N'', CAST(N'11:38:58.9579257' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (142, N'A0010', 6, 1, N'', CAST(N'11:38:58.9599230' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (143, N'A0011', 6, 1, N'', CAST(N'11:38:58.9619177' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (144, N'A0012', 6, 1, N'', CAST(N'11:38:58.9639242' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (145, N'A0013', 6, 1, N'', CAST(N'11:38:58.9659068' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (146, N'A0014', 6, 3, N'', CAST(N'11:38:58.9669046' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (147, N'A0015', 6, 1, N'', CAST(N'11:38:58.9688989' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (148, N'A0016', 6, 1, N'', CAST(N'11:38:58.9702246' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (149, N'A0017', 6, 1, N'', CAST(N'11:38:58.9714698' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (150, N'A0018', 6, 1, N'', CAST(N'11:38:58.9726885' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (151, N'A0019', 6, 3, N'', CAST(N'11:38:58.9736889' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (152, N'A0020', 6, 1, N'', CAST(N'11:38:58.9746859' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (153, N'A0021', 6, 1, N'', CAST(N'11:38:58.9756832' AS Time))
INSERT [dbo].[Attendance] ([AttendanceID], [StudentID], [TimeID], [AttendanceTypeID], [Note], [timeIn]) VALUES (154, N'A0022', 6, 1, N'', CAST(N'11:38:58.9776784' AS Time))
SET IDENTITY_INSERT [dbo].[Attendance] OFF
SET IDENTITY_INSERT [dbo].[MultiSubjectTeacher] ON 

INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (11, N'2', N'Sub004')
INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (12, N'2', N'Sub006')
INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (13, N'2', N'Sub005')
INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (14, N'1', N'Sub001')
INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (15, N'1', N'Sub002')
INSERT [dbo].[MultiSubjectTeacher] ([ID], [TeacherID], [SubjectID]) VALUES (16, N'1', N'Sub003')
SET IDENTITY_INSERT [dbo].[MultiSubjectTeacher] OFF
SET IDENTITY_INSERT [dbo].[ClassSubject] ON 

INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (1, N'Class001', N'Sub001', 1)
INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (2, N'Class001', N'Sub002', 1)
INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (3, N'Class001', N'Sub003', 0)
INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (4, N'Class002', N'Sub004', 1)
INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (5, N'Class002', N'Sub005', 1)
INSERT [dbo].[ClassSubject] ([ID], [ClassID], [SubjectID], [Status]) VALUES (6, N'Class002', N'Sub006', 1)
SET IDENTITY_INSERT [dbo].[ClassSubject] OFF
SET IDENTITY_INSERT [dbo].[ExamSchedule] ON 

INSERT [dbo].[ExamSchedule] ([ExamScheduleID], [ClassId], [SubjectID], [Examday], [TimeIn], [TimeOut], [Note], [RoomID], [status]) VALUES (1, N'Class001', N'Sub001', CAST(N'2021-11-06' AS Date), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'a', 1, 1)
INSERT [dbo].[ExamSchedule] ([ExamScheduleID], [ClassId], [SubjectID], [Examday], [TimeIn], [TimeOut], [Note], [RoomID], [status]) VALUES (2, N'Class002', N'Sub004', CAST(N'2021-11-06' AS Date), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'a', 3, 1)
SET IDENTITY_INSERT [dbo].[ExamSchedule] OFF
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([EventID], [EventName], [startDate], [endDate], [note], [status]) VALUES (1, N'Tet', CAST(N'2021-11-04' AS Date), CAST(N'2021-11-04' AS Date), N'a', 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
INSERT [dbo].[Staff] ([StaffID], [Password], [Phone], [Address], [Gender], [Email], [Image], [Birthday], [Status]) VALUES (N'admin', N'admin', N'0389862698', N'Hà Nội', 1, N'anhtuang123@gmail.com', N'a', CAST(N'2000-04-14' AS Date), 1)

ALTER TABLE Grade
  ADD CONSTRAINT FKGradeGradeTypes
   FOREIGN KEY (GradeTypeID)
   REFERENCES GradeType(GradeTypeID)
GO

ALTER TABLE Grade
   ADD CONSTRAINT FKGradeSubjects
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE Grade
   ADD CONSTRAINT FKGradeStudent
   FOREIGN KEY (StudentID)
   REFERENCES Student(StudentID)
GO

ALTER TABLE Subject
   ADD CONSTRAINT FKSubjectsFaculty
   FOREIGN KEY (FacultyID)
   REFERENCES Faculty(FacultyID)
GO

ALTER TABLE MultiSubjectTeacher
   ADD CONSTRAINT FKMultiSubjectTeacher
   FOREIGN KEY (TeacherID)
   REFERENCES Teacher(TeacherID)
GO

ALTER TABLE MultiSubjectTeacher
   ADD CONSTRAINT FKMultiTeacherTSubject
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE RelParentStudent
   ADD CONSTRAINT FKStudentParent
   FOREIGN KEY (StudentID)
   REFERENCES Student(StudentID)
GO

ALTER TABLE RelParentStudent
   ADD CONSTRAINT FKParentStudent
   FOREIGN KEY (ParentID)
   REFERENCES Parent(ParentID)
GO

ALTER TABLE Class
   ADD CONSTRAINT FKClassFaculty
   FOREIGN KEY (FacultyID)
   REFERENCES Faculty(FacultyID)
GO

ALTER TABLE ClassMember
   ADD CONSTRAINT FKStudentClass
   FOREIGN KEY (StudentID)
   REFERENCES Student(StudentID)
GO



ALTER TABLE ClassMember
   ADD CONSTRAINT FKClassStudent
   FOREIGN KEY (ClassID)
   REFERENCES Class(ClassID)
GO

ALTER TABLE ClassSubject
   ADD CONSTRAINT FKSubjectClass
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE ClassSubject
   ADD CONSTRAINT FKClassSubject
   FOREIGN KEY (ClassID)
   REFERENCES Class(ClassID)
GO

ALTER TABLE TimeTable
   ADD CONSTRAINT FKTimeClass
   FOREIGN KEY (ClassID)
   REFERENCES Class(ClassID)
GO

ALTER TABLE TimeTable
   ADD CONSTRAINT FKTimeSubject
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE TimeTable
   ADD CONSTRAINT FKTimeTeacher
   FOREIGN KEY (TeacherID)
   REFERENCES Teacher(TeacherID)
GO

ALTER TABLE TimeTable
   ADD CONSTRAINT FKTimeRoom
   FOREIGN KEY (RoomID)
   REFERENCES Room(RoomID)
GO

ALTER TABLE Teacher
   ADD CONSTRAINT FKTeacherFaculty
   FOREIGN KEY (FacultyID)
   REFERENCES Faculty(FacultyID)
GO

ALTER TABLE ExamSchedule
   ADD CONSTRAINT FKExamScheduleClass
   FOREIGN KEY (ClassID)
   REFERENCES Class(ClassID)
GO

ALTER TABLE ExamSchedule
   ADD CONSTRAINT FKExamScheduleSubjects
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE ExamSchedule
   ADD CONSTRAINT FKExamScheduleRoom
   FOREIGN KEY (RoomID)
   REFERENCES Room(RoomID)
GO

ALTER TABLE Attendance
   ADD CONSTRAINT FKattendanceType
   FOREIGN KEY (AttendanceTypeID)
   REFERENCES AttendanceType(AttendanceTypeID)
GO

ALTER TABLE Attendance
   ADD CONSTRAINT FKattendanceStudent
   FOREIGN KEY (StudentID)
   REFERENCES Student(StudentID)
GO

ALTER TABLE Attendance
   ADD CONSTRAINT FKattendanceTime
   FOREIGN KEY (TimeID)
   REFERENCES TimeTable(TimeID)
GO


ALTER TABLE FeesDetail
   ADD CONSTRAINT FKFeesDetailFeess
   FOREIGN KEY (FeesID)
   REFERENCES Fees(FeesID)
GO

ALTER TABLE FeesDetail
   ADD CONSTRAINT FKFeesDetailSubjects
   FOREIGN KEY (SubjectID)
   REFERENCES Subject(SubjectID)
GO

ALTER TABLE Feedback
   ADD CONSTRAINT FKFeedbackStudent
   FOREIGN KEY (StudentID)
   REFERENCES Student(StudentID)
GO
