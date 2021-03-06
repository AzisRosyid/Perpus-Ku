USE [master]
GO
/****** Object:  Database [PerpusKu]    Script Date: 31/05/2022 04:58:16 ******/
CREATE DATABASE [PerpusKu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PerpusKu', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PerpusKu.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PerpusKu_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PerpusKu_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PerpusKu] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PerpusKu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PerpusKu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PerpusKu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PerpusKu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PerpusKu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PerpusKu] SET ARITHABORT OFF 
GO
ALTER DATABASE [PerpusKu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PerpusKu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PerpusKu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PerpusKu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PerpusKu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PerpusKu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PerpusKu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PerpusKu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PerpusKu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PerpusKu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PerpusKu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PerpusKu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PerpusKu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PerpusKu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PerpusKu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PerpusKu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PerpusKu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PerpusKu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PerpusKu] SET  MULTI_USER 
GO
ALTER DATABASE [PerpusKu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PerpusKu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PerpusKu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PerpusKu] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PerpusKu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PerpusKu] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PerpusKu] SET QUERY_STORE = OFF
GO
USE [PerpusKu]
GO
/****** Object:  Table [dbo].[_book]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_book](
	[_isbn] [nvarchar](20) NOT NULL,
	[_title] [varchar](50) NOT NULL,
	[_author] [varchar](50) NOT NULL,
	[_year] [int] NOT NULL,
	[_publisher] [varchar](50) NOT NULL,
	[_classification_id] [int] NOT NULL,
	[_location] [varchar](50) NOT NULL,
	[_amount] [int] NOT NULL,
	[_pdf] [varchar](50) NULL,
 CONSTRAINT [PK__book] PRIMARY KEY CLUSTERED 
(
	[_isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_circulation]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_circulation](
	[_circulation_id] [int] NOT NULL,
	[_member_id] [int] NOT NULL,
	[_date_start] [date] NOT NULL,
	[_date_finish] [date] NOT NULL,
	[_date_return] [date] NULL,
	[_status] [date] NULL,
	[_user] [int] NOT NULL,
 CONSTRAINT [PK__circulation] PRIMARY KEY CLUSTERED 
(
	[_circulation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_circulation_detail]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_circulation_detail](
	[_id] [int] NOT NULL,
	[_circulation_id] [int] NOT NULL,
	[_isbn] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK__circulation_detail] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_classification]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_classification](
	[_id] [int] NOT NULL,
	[_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK__classification] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_member]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_member](
	[_member_id] [int] NOT NULL,
	[_identity_number] [nvarchar](20) NOT NULL,
	[_identity_type] [int] NOT NULL,
	[_name] [varchar](50) NOT NULL,
	[_registration_date] [date] NOT NULL,
 CONSTRAINT [PK__member] PRIMARY KEY CLUSTERED 
(
	[_member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_user]    Script Date: 31/05/2022 04:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user](
	[_user_id] [int] NOT NULL,
	[_identity_number] [varchar](50) NOT NULL,
	[_identity_type] [int] NOT NULL,
	[_name] [nvarchar](50) NOT NULL,
	[_username] [varchar](50) NOT NULL,
	[_password] [varchar](50) NOT NULL,
	[_level] [int] NOT NULL,
 CONSTRAINT [PK__user] PRIMARY KEY CLUSTERED 
(
	[_user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[_book] ([_isbn], [_title], [_author], [_year], [_publisher], [_classification_id], [_location], [_amount], [_pdf]) VALUES (N'978-602-8519-93-9', N'Membuat Aplikasi Desktop 1', N'Ahmad Fathoni, S.T.', 2015, N'Balai Pustaka', 1, N'IF 1 201', 36, N'PDF_15_04_2022_091633_978-602-8519-93-9.pdf')
GO
INSERT [dbo].[_circulation] ([_circulation_id], [_member_id], [_date_start], [_date_finish], [_date_return], [_status], [_user]) VALUES (1, 2, CAST(N'2022-04-15' AS Date), CAST(N'2022-04-16' AS Date), CAST(N'2022-04-17' AS Date), CAST(N'2022-04-15' AS Date), 1)
GO
INSERT [dbo].[_circulation_detail] ([_id], [_circulation_id], [_isbn]) VALUES (1, 1, N'978-602-8519-93-9')
GO
INSERT [dbo].[_classification] ([_id], [_name]) VALUES (1, N'Umum')
GO
INSERT [dbo].[_member] ([_member_id], [_identity_number], [_identity_type], [_name], [_registration_date]) VALUES (1, N'1234234', 1, N'halo', CAST(N'2022-04-14' AS Date))
INSERT [dbo].[_member] ([_member_id], [_identity_number], [_identity_type], [_name], [_registration_date]) VALUES (2, N'11111111111', 2, N'helo', CAST(N'2022-04-14' AS Date))
GO
INSERT [dbo].[_user] ([_user_id], [_identity_number], [_identity_type], [_name], [_username], [_password], [_level]) VALUES (1, N'32193', 1, N'Azis Rosyid', N'string', N'16a44bf9f8362466dfbb16bece6d92c09b2a7d3d', 3)
INSERT [dbo].[_user] ([_user_id], [_identity_number], [_identity_type], [_name], [_username], [_password], [_level]) VALUES (2, N'023483', 2, N'helo', N'helo', N'helo', 3)
INSERT [dbo].[_user] ([_user_id], [_identity_number], [_identity_type], [_name], [_username], [_password], [_level]) VALUES (3, N'223482', 1, N'halo', N'halo', N'halo', 1)
GO
ALTER TABLE [dbo].[_book]  WITH CHECK ADD  CONSTRAINT [FK__book__classification] FOREIGN KEY([_classification_id])
REFERENCES [dbo].[_classification] ([_id])
GO
ALTER TABLE [dbo].[_book] CHECK CONSTRAINT [FK__book__classification]
GO
ALTER TABLE [dbo].[_circulation]  WITH CHECK ADD  CONSTRAINT [FK__circulation__circulation] FOREIGN KEY([_member_id])
REFERENCES [dbo].[_member] ([_member_id])
GO
ALTER TABLE [dbo].[_circulation] CHECK CONSTRAINT [FK__circulation__circulation]
GO
ALTER TABLE [dbo].[_circulation]  WITH CHECK ADD  CONSTRAINT [FK__circulation__user] FOREIGN KEY([_user])
REFERENCES [dbo].[_user] ([_user_id])
GO
ALTER TABLE [dbo].[_circulation] CHECK CONSTRAINT [FK__circulation__user]
GO
ALTER TABLE [dbo].[_circulation_detail]  WITH CHECK ADD  CONSTRAINT [FK__circulation_detail__book] FOREIGN KEY([_isbn])
REFERENCES [dbo].[_book] ([_isbn])
GO
ALTER TABLE [dbo].[_circulation_detail] CHECK CONSTRAINT [FK__circulation_detail__book]
GO
ALTER TABLE [dbo].[_circulation_detail]  WITH CHECK ADD  CONSTRAINT [FK__circulation_detail__circulation] FOREIGN KEY([_circulation_id])
REFERENCES [dbo].[_circulation] ([_circulation_id])
GO
ALTER TABLE [dbo].[_circulation_detail] CHECK CONSTRAINT [FK__circulation_detail__circulation]
GO
USE [master]
GO
ALTER DATABASE [PerpusKu] SET  READ_WRITE 
GO
