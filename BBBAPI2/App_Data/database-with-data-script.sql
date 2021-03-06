USE [master]
GO
/****** Object:  Database [irambidb]    Script Date: 5/3/2015 2:31:49 PM ******/
CREATE DATABASE [irambidb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'irambidb', FILENAME = N'C:\MSSQL\DATA\irambidb.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'irambidb_log', FILENAME = N'C:\MSSQL\DATA\irambidb_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [irambidb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [irambidb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [irambidb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [irambidb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [irambidb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [irambidb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [irambidb] SET ARITHABORT OFF 
GO
ALTER DATABASE [irambidb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [irambidb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [irambidb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [irambidb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [irambidb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [irambidb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [irambidb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [irambidb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [irambidb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [irambidb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [irambidb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [irambidb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [irambidb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [irambidb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [irambidb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [irambidb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [irambidb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [irambidb] SET RECOVERY FULL 
GO
ALTER DATABASE [irambidb] SET  MULTI_USER 
GO
ALTER DATABASE [irambidb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [irambidb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [irambidb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [irambidb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [irambidb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [irambidb]
GO
/****** Object:  User [BSOER2015\dbuser]    Script Date: 5/3/2015 2:31:49 PM ******/
CREATE USER [BSOER2015\dbuser] FOR LOGIN [BSOER2015\dbuser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ben]    Script Date: 5/3/2015 2:31:49 PM ******/
CREATE USER [ben] FOR LOGIN [ben] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [alan]    Script Date: 5/3/2015 2:31:50 PM ******/
CREATE USER [alan] FOR LOGIN [alan] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [BSOER2015\dbuser]
GO
ALTER ROLE [db_owner] ADD MEMBER [ben]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [ben]
GO
ALTER ROLE [db_datareader] ADD MEMBER [alan]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[commentid] [int] IDENTITY(1,1) NOT NULL,
	[newsid] [int] NOT NULL,
	[userid] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NOT NULL DEFAULT (getdate()),
	[content] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[commentid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Course]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[courseid] [int] IDENTITY(1,1) NOT NULL,
	[facultyid] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[courseid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseSection]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSection](
	[coursesectionid] [int] IDENTITY(1,1) NOT NULL,
	[courseid] [int] NOT NULL,
	[datetimestart] [datetime] NULL,
	[datetimeend] [datetime] NULL,
	[term] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[coursesectionid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[facultyid] [int] IDENTITY(1,1) NOT NULL,
	[facultyname] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[facultyid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[newsid] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](50) NOT NULL,
	[programid] [int] NOT NULL,
	[coursesectionid] [int] NOT NULL,
	[datetime] [datetime] NOT NULL DEFAULT (getdate()),
	[title] [nvarchar](50) NOT NULL,
	[content] [nvarchar](max) NULL,
	[priority] [nvarchar](50) NULL,
	[expirydate] [datetime] NOT NULL DEFAULT ('12/31/9999'),
	[active] [bit] NOT NULL DEFAULT ((1)),
PRIMARY KEY CLUSTERED 
(
	[newsid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Program]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[programid] [int] IDENTITY(1,1) NOT NULL,
	[facultyid] [int] NOT NULL,
	[programname] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[programid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Set]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Set](
	[setid] [int] IDENTITY(1,1) NOT NULL,
	[programid] [int] NOT NULL,
	[setname] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[setid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[facultyid] [int] NOT NULL,
	[userid] [nvarchar](50) NOT NULL,
	[position] [nvarchar](50) NOT NULL,
	[alternateemail] [nvarchar](50) NULL,
	[officelocation] [nvarchar](50) NULL,
	[ohmonday] [nvarchar](50) NULL DEFAULT ('None'),
	[ohtuesday] [nvarchar](50) NULL DEFAULT ('None'),
	[ohwednesday] [nvarchar](50) NULL DEFAULT ('None'),
	[ohthursday] [nvarchar](50) NULL DEFAULT ('None'),
	[ohfriday] [nvarchar](50) NULL DEFAULT ('None'),
PRIMARY KEY CLUSTERED 
(
	[facultyid] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userid] [nvarchar](50) NOT NULL,
	[programid] [int] NULL,
	[setid] [int] NULL,
	[password] [nvarchar](50) NOT NULL,
	[firstname] [nvarchar](50) NOT NULL,
	[lastname] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[active] [bit] NOT NULL DEFAULT ((0)),
	[phonenumber] [nvarchar](50) NULL,
	[token] [nvarchar](50) NULL,
	[appletoken] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserCourseSection]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCourseSection](
	[coursesectionid] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](50) NOT NULL,
	[role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[coursesectionid] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (13, 8, N'A00843110', CAST(N'2015-03-12 19:47:50.777' AS DateTime), N'But think of the children!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (14, 8, N'A00000002', CAST(N'2015-04-07 01:44:31.447' AS DateTime), N'They should be fine. Thanks')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (15, 8, N'A00000002', CAST(N'2015-04-07 02:04:37.857' AS DateTime), N'Let\''s meet up later today. I need to clarify some requirements. See you!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (16, 8, N'A00111111', CAST(N'2015-04-07 02:07:25.077' AS DateTime), N'Andrew, where are we meeting and what time? I might have to leave early today.')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (17, 59, N'A00111111', CAST(N'2015-04-07 02:22:24.253' AS DateTime), N'Andrew, can you bring the iPhone connector please! My team will give a demo. Thanks!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (18, 58, N'A00111111', CAST(N'2015-04-07 04:04:19.947' AS DateTime), N'Can we relocate our class for this week? We have some urgent tasks that needs priority this week. Please advise!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (19, 58, N'A00111111', CAST(N'2015-04-07 04:05:02.433' AS DateTime), N'In addition, we will need a room with projector, please! Thanks.')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (20, 58, N'A00000002', CAST(N'2015-04-07 04:11:41.530' AS DateTime), N'Sorry, Ryan. It seems all other rooms are booked for this week. Would you be alright with going to our Downtown campus?')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (25, 61, N'A00843110', CAST(N'2015-04-07 06:16:57.803' AS DateTime), N'I am in!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (26, 8, N'A00222222', CAST(N'2015-04-07 10:22:41.220' AS DateTime), N'Yay more time!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (28, 61, N'A00222222', CAST(N'2015-04-07 10:26:42.777' AS DateTime), N'Food!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (29, 58, N'A00222222', CAST(N'2015-04-07 10:32:20.157' AS DateTime), N'Why?!?! Why is it always NE1?!?!')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (30, 67, N'A00000001', CAST(N'2015-04-07 16:54:16.380' AS DateTime), N'Oh. And your write ups due in Friday')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (31, 61, N'A00843110', CAST(N'2015-04-07 17:00:56.780' AS DateTime), N'G ')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (32, 61, N'A00843110', CAST(N'2015-04-07 17:01:12.927' AS DateTime), N'No comment')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (35, 67, N'A00000002', CAST(N'2015-04-07 17:57:56.890' AS DateTime), N'Herro')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (36, 67, N'A00000002', CAST(N'2015-04-07 18:10:06.007' AS DateTime), N'Thanks Keith')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (37, 67, N'A00000002', CAST(N'2015-04-07 18:10:09.000' AS DateTime), N'Thanks Keith')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (38, 67, N'A00000002', CAST(N'2015-04-07 12:50:49.570' AS DateTime), N'Hola')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (40, 93, N'A00000002', CAST(N'2015-04-09 09:22:47.420' AS DateTime), N'bring pens instead')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (41, 8, N'A00000002', CAST(N'2015-04-09 09:28:19.467' AS DateTime), N'here is another comment')
INSERT [dbo].[Comment] ([commentid], [newsid], [userid], [datetime], [content]) VALUES (42, 97, N'A00843110', CAST(N'2015-04-09 20:32:09.050' AS DateTime), N'Oh no! That\''s terrible.')
SET IDENTITY_INSERT [dbo].[Comment] OFF
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([courseid], [facultyid], [name], [description]) VALUES (2, 1, N'Database', N'DB Intro  ')
INSERT [dbo].[Course] ([courseid], [facultyid], [name], [description]) VALUES (4, 1, N'C++', N'C++       ')
INSERT [dbo].[Course] ([courseid], [facultyid], [name], [description]) VALUES (6, 1, N'CodeIgniter', N'CI        ')
INSERT [dbo].[Course] ([courseid], [facultyid], [name], [description]) VALUES (7, 1, N'Algorithms', N'Algo      ')
INSERT [dbo].[Course] ([courseid], [facultyid], [name], [description]) VALUES (10, 1, N'ALL', N'Global/All')
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[CourseSection] ON 

INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (3, 2, CAST(N'2013-09-05 09:00:00.000' AS DateTime), CAST(N'2013-12-15 17:30:00.000' AS DateTime), N'4')
INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (7, 2, CAST(N'2015-01-02 09:00:00.000' AS DateTime), CAST(N'2015-04-15 09:00:00.000' AS DateTime), N'2')
INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (8, 4, CAST(N'2015-04-15 17:30:00.000' AS DateTime), CAST(N'2015-08-15 17:30:00.000' AS DateTime), N'3')
INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (9, 6, CAST(N'2015-01-15 17:30:00.000' AS DateTime), CAST(N'2015-04-15 17:30:00.000' AS DateTime), N'4')
INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (10, 7, CAST(N'2015-01-15 17:30:00.000' AS DateTime), CAST(N'2015-04-15 17:30:00.000' AS DateTime), N'3')
INSERT [dbo].[CourseSection] ([coursesectionid], [courseid], [datetimestart], [datetimeend], [term]) VALUES (11, 10, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CourseSection] OFF
SET IDENTITY_INSERT [dbo].[Faculty] ON 

INSERT [dbo].[Faculty] ([facultyid], [facultyname]) VALUES (1, N'Technology Academics')
SET IDENTITY_INSERT [dbo].[Faculty] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (8, N'A00000002', 1, 8, CAST(N'2015-03-07 17:34:17.643' AS DateTime), N'Essay Project Extension', N'Due to popular demand, the project due date has been extended. No more syntactical sugar', N'standard', CAST(N'2015-03-07 17:34:17.643' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (58, N'A00843110', 1, 10, CAST(N'2015-03-30 15:01:25.440' AS DateTime), N'Water Boiler Broken at BCIT', N'Serious issues, NE1 is closed', N'Critical', CAST(N'2015-04-10 09:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (59, N'A00000002', 1, 8, CAST(N'2015-04-07 02:16:00.563' AS DateTime), N'iOS Presentation Tommorrow', N'Please do not forget that you have an scheduled presentation for your iOS projects tomorrow at 11:00AM in SW12-319. I expect great things from you guys! -AK', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (60, N'A00000002', 1, 11, CAST(N'2015-04-07 04:14:46.070' AS DateTime), N'Pleasant holiday?', N'Hello everyone! I hope you enjoyed your weekend. This is the final stretch, please work hard! We have high expectations. We\''ve been receiving a lot of requests from big time companies for visiting here. I\''ll give more news later this week. -ak', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (61, N'A00000001', 1, 3, CAST(N'2015-04-07 04:43:01.420' AS DateTime), N'After Finals party event', N'Hello my esteemed Database students. I would like to congratulate you for almost finishing this course. As a reward, I\''ll be treating all graduating students to one of the most expensive restaurant in BC! Look forward the update! -k', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (62, N'A00000001', 1, 7, CAST(N'2015-04-07 04:44:30.957' AS DateTime), N'End of Finals event', N'I would like to invite all graduating Database students to attend one of the most prestigious event you will ever attend. It will happen in Downtown Whister. Please standby for more updates. -k', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (67, N'A00000001', 1, 7, CAST(N'2015-04-07 16:53:40.870' AS DateTime), N'Load your apps in phone', N'Please don\''t forget to load your app and make sure it runs for the presentation today', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (91, N'A00000001', 1, 7, CAST(N'2015-04-09 01:44:02.007' AS DateTime), N'Weekly meeting is scheduled', N'Please check your google calendars! I have asked Dr. Taurus to prepare a demo of the Hydron Accelerator for this month.', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (93, N'A00000001', 1, 3, CAST(N'2015-04-09 03:05:36.380' AS DateTime), N'Bring your pencils on Tuesday!', N'It is imperative that you bring your trusty pencils and erasers next week! Might as well include your rulers. -kt', N'Standard', CAST(N'9999-12-31 23:59:59.997' AS DateTime), 1)
INSERT [dbo].[News] ([newsid], [userid], [programid], [coursesectionid], [datetime], [title], [content], [priority], [expirydate], [active]) VALUES (97, N'A00000002', 1, 11, CAST(N'2015-04-09 20:30:41.663' AS DateTime), N'Flood in SE12', N'Danger - Flood in SE12. Please stay away.', N'Critical', CAST(N'2015-04-10 20:29:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[Program] ON 

INSERT [dbo].[Program] ([programid], [facultyid], [programname]) VALUES (1, 1, N'Computer Systems Technology')
SET IDENTITY_INSERT [dbo].[Program] OFF
SET IDENTITY_INSERT [dbo].[Set] ON 

INSERT [dbo].[Set] ([setid], [programid], [setname]) VALUES (1, 1, N'4R')
SET IDENTITY_INSERT [dbo].[Set] OFF
INSERT [dbo].[Teacher] ([facultyid], [userid], [position], [alternateemail], [officelocation], [ohmonday], [ohtuesday], [ohwednesday], [ohthursday], [ohfriday]) VALUES (1, N'A00000001', N'Database Instructor', NULL, N'SW2-444', N'9:00am-11:00am', NULL, N'1:00pm-5:00pm', N'11:30am-2:00pm', NULL)
INSERT [dbo].[Teacher] ([facultyid], [userid], [position], [alternateemail], [officelocation], [ohmonday], [ohtuesday], [ohwednesday], [ohthursday], [ohfriday]) VALUES (1, N'A00000002', N'C++ and Algo Instructor', NULL, N'SW2-104', N'9:00am-11:00am', N'2:00pm-3:00pm', NULL, NULL, N'4:00pm-5:30pm')
INSERT [dbo].[Teacher] ([facultyid], [userid], [position], [alternateemail], [officelocation], [ohmonday], [ohtuesday], [ohwednesday], [ohthursday], [ohfriday]) VALUES (1, N'A00000003', N'CI Instructor', N'joptionalparryparry@myspace.org', N'SW2-130', N'6:00am-6:30am', NULL, NULL, N'9:00pm-11:30pm', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00000001', 1, NULL, N'password', N'Keith', N'Tang', N'ktang@my.bcit.ca', N'admin', 1, N'1-800-987-8877', N'x7ftbr7Wjidpzvxk7juT', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00000002', 1, NULL, N'password', N'Andrew', N'Kult', N'akult@my.bcit.ca', N'admin', 1, N'123-456-7890', N'3lZ3dyg5DMTDtS6fyFj986cc9m5i', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00000003', 1, NULL, N'password', N'Jim', N'Parry', N'jlparry@my.bcit.ca', N'admin', 1, N'456-887-0987', NULL, NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00111111', 1, 1, N'password', N'Ryan', N'Sadio', N'studen1@my.bcit.ca', N'student', 1, N'111-222-3333', N'1YDLqzqb4HaDDAsvVraNkdlE54V', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00123456', 1, 1, N'password', N'Bert', N'Townshend', N'student2@my.bcit.ca', N'student', 1, N'123-456-7890', NULL, NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00222222', 1, 1, N'password', N'Alan', N'Lai', N'student3@my.bcit.ca', N'student', 1, N'999-888-7777', N'bAlTGe3jGyZbOAn', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00333333', 1, 1, N'password', N'Matthew', N'Banman', N'student4@my.bcit.ca', N'student', 1, N'897-574-3344', N'QKjCxi4n0dHsK3iFXi65pnf6x', NULL)
INSERT [dbo].[User] ([userid], [programid], [setid], [password], [firstname], [lastname], [email], [type], [active], [phonenumber], [token], [appletoken]) VALUES (N'A00843110', 1, 1, N'password', N'Benjamin', N'Soer', N'student5@my.bcit.ca', N'Student', 1, N'604-842-2274', N'GJHIuy19DFLDbdYrv2rDat', N'myappletoken')
SET IDENTITY_INSERT [dbo].[UserCourseSection] ON 

INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (3, N'A00000001', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (3, N'A00222222', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (3, N'A00333333', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (3, N'A00843110', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (7, N'A00000001', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (7, N'A00111111', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (8, N'A00000002', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (10, N'A00000002', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00000001', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00000002', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00000003', N'instructor')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00111111', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00123456', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00222222', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00333333', N'student')
INSERT [dbo].[UserCourseSection] ([coursesectionid], [userid], [role]) VALUES (11, N'A00843110', N'student')
SET IDENTITY_INSERT [dbo].[UserCourseSection] OFF
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_ToNews] FOREIGN KEY([newsid])
REFERENCES [dbo].[News] ([newsid])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_ToNews]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_ToUser] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([userid])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_ToUser]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_ToFaculty] FOREIGN KEY([facultyid])
REFERENCES [dbo].[Faculty] ([facultyid])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_ToFaculty]
GO
ALTER TABLE [dbo].[CourseSection]  WITH CHECK ADD  CONSTRAINT [FK_CourseSection_ToCourse] FOREIGN KEY([courseid])
REFERENCES [dbo].[Course] ([courseid])
GO
ALTER TABLE [dbo].[CourseSection] CHECK CONSTRAINT [FK_CourseSection_ToCourse]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_ToCourseSection] FOREIGN KEY([coursesectionid])
REFERENCES [dbo].[CourseSection] ([coursesectionid])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_ToCourseSection]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_ToProgram] FOREIGN KEY([programid])
REFERENCES [dbo].[Program] ([programid])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_ToProgram]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_ToUser] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([userid])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_ToUser]
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_ToFaculty] FOREIGN KEY([facultyid])
REFERENCES [dbo].[Faculty] ([facultyid])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_ToFaculty]
GO
ALTER TABLE [dbo].[Set]  WITH CHECK ADD  CONSTRAINT [FK_Set_ToProgram] FOREIGN KEY([programid])
REFERENCES [dbo].[Program] ([programid])
GO
ALTER TABLE [dbo].[Set] CHECK CONSTRAINT [FK_Set_ToProgram]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_ToFaculty] FOREIGN KEY([facultyid])
REFERENCES [dbo].[Faculty] ([facultyid])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_ToFaculty]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_ToUser] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([userid])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_ToUser]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ToProgram] FOREIGN KEY([programid])
REFERENCES [dbo].[Program] ([programid])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_ToProgram]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ToSet] FOREIGN KEY([setid])
REFERENCES [dbo].[Set] ([setid])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_ToSet]
GO
ALTER TABLE [dbo].[UserCourseSection]  WITH CHECK ADD  CONSTRAINT [FK_UserCourseSection_ToCourseSection] FOREIGN KEY([coursesectionid])
REFERENCES [dbo].[CourseSection] ([coursesectionid])
GO
ALTER TABLE [dbo].[UserCourseSection] CHECK CONSTRAINT [FK_UserCourseSection_ToCourseSection]
GO
ALTER TABLE [dbo].[UserCourseSection]  WITH CHECK ADD  CONSTRAINT [FK_UserCourseSection_ToUser] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([userid])
GO
ALTER TABLE [dbo].[UserCourseSection] CHECK CONSTRAINT [FK_UserCourseSection_ToUser]
GO
/****** Object:  StoredProcedure [dbo].[getCriticalNews]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ben Soer
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[getCriticalNews] 
	-- Add the parameters for the stored procedure here
	  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM [News] n
	WHERE n.priority = 'critical' OR n.priority = 'Critical';
END


GO
/****** Object:  StoredProcedure [dbo].[validateToken]    Script Date: 5/3/2015 2:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ben Soer
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[validateToken] 
	-- Add the parameters for the stored procedure here
	@userid nvarchar(50) = NULL, 
	@token nvarchar(50) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM [User] u
	WHERE u.userid = @userid AND u.token = @token;

END


GO
USE [master]
GO
ALTER DATABASE [irambidb] SET  READ_WRITE 
GO
