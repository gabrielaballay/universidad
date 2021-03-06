USE [universidad]
GO
/****** Object:  Table [dbo].[Registereds]    Script Date: 25/3/2021 18:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registereds](
	[Id_registered] [int] IDENTITY(1,1) NOT NULL,
	[Id_user] [int] NULL,
	[Id_subject] [int] NULL,
	[Active] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_registered] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 25/3/2021 18:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[Id_subject] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[Start_time] [time](7) NULL,
	[End_time] [time](7) NULL,
	[Max_flake] [int] NULL,
	[Id_teacher] [int] NULL,
	[Active] [tinyint] NULL,
	[Id_user] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_subject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 25/3/2021 18:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id_teacher] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Last_name] [varchar](50) NULL,
	[Dni] [int] NULL,
	[Active] [tinyint] NULL,
	[Id_user] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_teacher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_types]    Script Date: 25/3/2021 18:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_types](
	[Id_type] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25/3/2021 18:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id_user] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Last_name] [varchar](50) NULL,
	[Dni] [int] NULL,
	[Docket] [int] NULL,
	[Password] [varchar](150) NULL,
	[Active] [tinyint] NULL,
	[Id_type] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Registereds]  WITH CHECK ADD  CONSTRAINT [fk_id_subject] FOREIGN KEY([Id_subject])
REFERENCES [dbo].[Subjects] ([Id_subject])
GO
ALTER TABLE [dbo].[Registereds] CHECK CONSTRAINT [fk_id_subject]
GO
ALTER TABLE [dbo].[Registereds]  WITH CHECK ADD  CONSTRAINT [fk_user_id] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([Id_user])
GO
ALTER TABLE [dbo].[Registereds] CHECK CONSTRAINT [fk_user_id]
GO
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [fk_id_user] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([Id_user])
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [fk_id_user]
GO
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [fk_teacher] FOREIGN KEY([Id_teacher])
REFERENCES [dbo].[Teachers] ([Id_teacher])
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [fk_teacher]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [fk_User] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([Id_user])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [fk_User]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [fk_User_type] FOREIGN KEY([Id_type])
REFERENCES [dbo].[User_types] ([Id_type])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [fk_User_type]
GO
