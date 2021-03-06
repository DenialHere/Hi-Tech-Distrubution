USE [master]
GO
/****** Object:  Database [HiTechDistributionDB]    Script Date: 11/24/2020 7:46:10 PM ******/
CREATE DATABASE [HiTechDistributionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HiTechDistributionDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER2017\MSSQL\DATA\HiTechDistributionDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HiTechDistributionDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER2017\MSSQL\DATA\HiTechDistributionDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HiTechDistributionDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HiTechDistributionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HiTechDistributionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HiTechDistributionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HiTechDistributionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET RECOVERY FULL 
GO
ALTER DATABASE [HiTechDistributionDB] SET  MULTI_USER 
GO
ALTER DATABASE [HiTechDistributionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HiTechDistributionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HiTechDistributionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HiTechDistributionDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HiTechDistributionDB', N'ON'
GO
ALTER DATABASE [HiTechDistributionDB] SET QUERY_STORE = OFF
GO
USE [HiTechDistributionDB]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](255) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorBooks]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorBooks](
	[AuthorId] [int] NOT NULL,
	[ISBN] [bigint] NOT NULL,
	[YearPublished] [int] NOT NULL,
	[Edition] [nvarchar](100) NULL,
 CONSTRAINT [PK_AuthorBooks] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC,
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[ISBN] [bigint] NOT NULL,
	[BookTitle] [nvarchar](255) NOT NULL,
	[QOH] [int] NOT NULL,
	[CatergoryId] [int] NOT NULL,
	[PublisherId] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catergories]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catergories](
	[CatergoryId] [int] NOT NULL,
	[CatergoryName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Catergories] PRIMARY KEY CLUSTERED 
(
	[CatergoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](60) NULL,
	[LastName] [nvarchar](80) NULL,
	[City] [nvarchar](80) NULL,
	[Address] [nvarchar](255) NULL,
	[PostalCode] [nvarchar](6) NULL,
	[PhoneNumber] [nvarchar](14) NULL,
	[FaxNumber] [nvarchar](30) NULL,
	[CreditLimit] [float] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](14) NULL,
	[Email] [nvarchar](255) NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[JobId] [int] NOT NULL,
	[JobTitle] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderType] [nvarchar](200) NULL,
	[RequiredDate] [date] NOT NULL,
	[ShippingDate] [date] NOT NULL,
	[OrderStatus] [nvarchar](100) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersLines]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersLines](
	[OrderId] [int] NOT NULL,
	[ISBN] [bigint] NOT NULL,
	[QuantityOrdered] [int] NOT NULL,
 CONSTRAINT [PK_OrdersLines] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[PublisherId] [int] NOT NULL,
	[PublisherName] [nvarchar](255) NOT NULL,
	[WebAddress] [nvarchar](255) NULL,
 CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED 
(
	[PublisherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/24/2020 7:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserName] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[EmployeeId] [int] NULL,
	[AccessLevel] [tinyint] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (1111, N'William ', N'ShakeSpear', NULL)
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (2222, N'J. K.', N'Rowling', N'jkrowling@gmail.com')
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (3333, N'Jess ', N'Kidd                ', N'jkidd@hotmail.com')
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (4444, N'Helen', N'Phillips', N'helenp@yahoo.com')
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (5555, N'Agatha ', N'Christie', NULL)
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (6666, N'Dean', N'Koontz', N'koontz@gmail.com')
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [Email]) VALUES (7777, N'Stephen', N'King', N'sking@live.com')
GO
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (1111, 1368908370112, 2016, N'2nd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (1111, 1712367189371, 2012, N'2nd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (1111, 9783161484100, 2020, N'12th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (1111, 9883161454160, 2020, N'5th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (2222, 1773161464130, 2018, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (2222, 2173161464195, 2011, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (3333, 2519919132151, 2019, N'2nd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (3333, 3122168464188, 2017, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (3333, 3627182368810, 2010, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (4444, 5623467523892, 2020, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (5555, 1368908370112, 2016, N'2nd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (5555, 6712386723182, 2008, N'3rd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (5555, 6743836154181, 2020, N'6th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (5555, 7234623728615, 2016, N'2nd edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (5555, 7326826283625, 2012, N'1st edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (6666, 7683220192818, 2010, N'5th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (6666, 7861528239179, 2020, N'10th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (6666, 9010238671621, 2015, N'7th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (7777, 9723644356283, 2020, N'11th edition')
INSERT [dbo].[AuthorBooks] ([AuthorId], [ISBN], [YearPublished], [Edition]) VALUES (7777, 9783161484100, 2020, N'12th edition')
GO
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (1368908370112, N'Economics For Dummies', 50, 8, 6000, 50)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (1712367189371, N'Music Theory', 100, 10, 7000, 15.5)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (1773161464130, N'Artificial Intelligence', 112, 1, 1000, 69.95)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (2173161464195, N'Code Complete', 61, 1, 2000, 44)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (2519919132151, N'Programming Pearls', 18, 1, 3000, 18.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (2623738267272, N'danial', 2, 1, 5000, 5)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (3122168464188, N'The Art of Computer Programming', 14, 1, 1000, 129.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (3627182368810, N'Calculus I', 280, 3, 6000, 43.95)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (5623467523892, N'Music Around the world', 55, 10, 5000, 36.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (6712386723182, N'English Adapted', 642, 2, 4000, 22.65)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (6743836154181, N'GUI', 33, 7, 4000, 65)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (7234623728615, N'Social studies I', 178, 4, 2000, 50)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (7326826283625, N'Math I', 400, 3, 5000, 55.95)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (7683220192818, N'Health', 670, 6, 3000, 8.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (7861528239179, N'Around The World', 32, 9, 5000, 30.25)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (9010238671621, N'French', 145, 5, 7000, 12.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (9723644356283, N'Philosphy I', 321, 2, 1000, 78.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (9783161484100, N'Introduction to Algorithms', 180, 1, 1000, 58.99)
INSERT [dbo].[Books] ([ISBN], [BookTitle], [QOH], [CatergoryId], [PublisherId], [Price]) VALUES (9883161454160, N'Clean Code', 280, 1, 2000, 99.99)
GO
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (1, N'Computer Science')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (2, N'English Lanuage Arts')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (3, N'Mathematics')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (4, N'Social Studies ')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (5, N'Foreign Language Study ')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (6, N'Physical education')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (7, N'Graphic design')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (8, N'Economics')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (9, N'Geography')
INSERT [dbo].[Catergories] ([CatergoryId], [CatergoryName]) VALUES (10, N'Music')
GO
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (1, N'John', N'Marcus', N'Montreal', N'22 North Street', N'h3n2c5', N'(514)250-5555', N'(514)999-1111', 2500)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (2, N'Mary', N'Wilkens', N'Montreal', N'555 boul terrase', N'h1n2f9', N'(514)299-1111', N'(514)599-4445', 5000)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (3, N'Mark', N'Wiens', N'Toronto', N'1000 bloomfield', N'h6j9o8', N'(416)255-9000', N'(416)999-8190', 1500)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (4, N'Jessica', N'Dizazzo', N'Montreal', N'17 Parks Drive', N'h2c7t9', N'(514)271-7777', N'(514)980-9991', 5000)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (5, N'Mary', N'Green', N'Montreal', N'8355 Mount Ave', N'h4n7u1', N'(438)288-9192', N'(438)288-9192', 2000)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (6, N'Brad', N'Piff', N'Toronto', N'1791 Queen''s Boul', N'h2n6r6', N'(450)999-8112', N'(450)999-8112', 25000)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (7, N'Sarah', N'Sandel', N'Ontario', N'8277 Loriginer ', N'm4m2c5', N'(415)850-6154', N'(415)850-6154', 2500)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (8, N'Tim', N'Cook', N'Montreal', N'1911 Prieur', N'h3n2l8', N'(514)324-7556', N'(514)325-7556', 1000)
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (111, N'Henry', N'Brown', N'(514)-255-5555', N'hbrown@gmail.com ', 1)
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (222, N'Thomas', N'Moore', N'(438)-555-6666', N'tMoore@yahoo.com', 2)
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (333, N'Peter', N'Wang', N'(514)-212-9406', N'petewang@hotmail.com', 3)
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (444, N'Mary', N'Brown', N'(514)-255-5555', N'mBrown@gmail.com', 4)
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (555, N'Jenifer', N'Bouchard', N'(450)-911-2900', N'bouchjen@gmail.com', 4)
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [PhoneNumber], [Email], [JobId]) VALUES (666, N'Kim', N'Hoa Nguyen', N'(514)-477-6690', N'kimnguyen@hotmail.com', 5)
GO
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (1, N'MIS Manager')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (2, N'Sales Manager')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (3, N'Inventory Controller')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (4, N'Order Clerk')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (5, N'Accountant')
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (2, CAST(N'2020-10-23' AS Date), N'Domestic', CAST(N'2020-10-26' AS Date), CAST(N'2020-10-24' AS Date), N'Delivered', 1, 111)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (3, CAST(N'2020-09-20' AS Date), N'International', CAST(N'2020-10-01' AS Date), CAST(N'2020-09-29' AS Date), N'Delievered', 1, 111)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (4, CAST(N'2020-11-23' AS Date), N'Domestic', CAST(N'2020-11-30' AS Date), CAST(N'2020-11-25' AS Date), N'In Transit', 2, 222)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (5, CAST(N'2020-11-20' AS Date), N'Domestic', CAST(N'2020-11-29' AS Date), CAST(N'2020-11-25' AS Date), N'Packing', 3, 222)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (6, CAST(N'2020-09-10' AS Date), N'International', CAST(N'2020-09-20' AS Date), CAST(N'2020-09-14' AS Date), N'Delievered', 3, 333)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (7, CAST(N'2020-10-11' AS Date), N'International', CAST(N'2020-10-20' AS Date), CAST(N'2020-10-16' AS Date), N'Delievered', 3, 333)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (8, CAST(N'2020-11-23' AS Date), N'Domestic', CAST(N'2020-11-30' AS Date), CAST(N'2020-11-25' AS Date), N'In Transit', 4, 444)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (9, CAST(N'2020-11-23' AS Date), N'International', CAST(N'2020-12-03' AS Date), CAST(N'2020-11-29' AS Date), N'Packing', 5, 555)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (11, CAST(N'2020-08-01' AS Date), N'Domestic', CAST(N'2020-08-10' AS Date), CAST(N'2020-08-08' AS Date), N'Delievered', 6, 666)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [OrderType], [RequiredDate], [ShippingDate], [OrderStatus], [CustomerId], [EmployeeId]) VALUES (12, CAST(N'2020-11-23' AS Date), N'Domestic', CAST(N'2020-12-10' AS Date), CAST(N'2020-12-05' AS Date), N'Packing', 7, 444)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (1000, N'Premier Press', N'PremierPress.com')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (2000, N'Wrox', N'Wrox.com')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (3000, N'Murach', N'Murach.com')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (4000, N'Prentice Hall ', N'PrenticeHall.com ')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (5000, N'Penguin', N'PenguinPublishing.com')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (6000, N'Word Art', N'WordArt.com')
INSERT [dbo].[Publishers] ([PublisherId], [PublisherName], [WebAddress]) VALUES (7000, N'Dynamite', N'DynamitePublish.com')
GO
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Hbrown2020', N'2020', 111, 1)
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Jbouchard2020', N'2020', 555, 4)
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Knguyen2020', N'2020', 666, 5)
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Mbrown2020', N'2020', 444, 4)
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Pwang2020', N'2020', 333, 3)
INSERT [dbo].[Users] ([UserName], [Password], [EmployeeId], [AccessLevel]) VALUES (N'Tmoore2020', N'2020', 222, 2)
GO
ALTER TABLE [dbo].[AuthorBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBooks_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[AuthorBooks] CHECK CONSTRAINT [FK_AuthorBooks_Author]
GO
ALTER TABLE [dbo].[AuthorBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBooks_Books] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Books] ([ISBN])
GO
ALTER TABLE [dbo].[AuthorBooks] CHECK CONSTRAINT [FK_AuthorBooks_Books]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Catergories] FOREIGN KEY([CatergoryId])
REFERENCES [dbo].[Catergories] ([CatergoryId])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Catergories]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publishers] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publishers] ([PublisherId])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publishers]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Jobs1] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([JobId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Jobs1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Employees]
GO
ALTER TABLE [dbo].[OrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_OrdersLines_Books] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Books] ([ISBN])
GO
ALTER TABLE [dbo].[OrdersLines] CHECK CONSTRAINT [FK_OrdersLines_Books]
GO
ALTER TABLE [dbo].[OrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_OrdersLines_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrdersLines] CHECK CONSTRAINT [FK_OrdersLines_Orders]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Employees1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Employees1]
GO
USE [master]
GO
ALTER DATABASE [HiTechDistributionDB] SET  READ_WRITE 
GO
