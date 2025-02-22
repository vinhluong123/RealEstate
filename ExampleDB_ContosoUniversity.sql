USE [ContosoUniversity]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2024-02-26 8:48:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Credits] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 2024-02-26 8:48:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[EnrollmentID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
	[Grade] [int] NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2024-02-26 8:48:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[FirstMidName] [nvarchar](max) NULL,
	[EnrollmentDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (1045, N'Calculus', 4)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (1050, N'Chemistry', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (2021, N'Composition', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (2042, N'Literature', 4)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (3141, N'Trigonometry', 4)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (4022, N'Microeconomics', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (4041, N'Macroeconomics', 3)
GO
SET IDENTITY_INSERT [dbo].[Enrollment] ON 

INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (1, 1050, 4, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (2, 1050, 3, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (3, 2021, 2, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (4, 3141, 2, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (5, 1045, 2, 1)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (6, 4041, 1, 1)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (7, 4022, 1, 2)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (8, 1050, 1, 0)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (9, 4022, 4, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (10, 4041, 5, 2)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (11, 1045, 6, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (12, 3141, 7, 0)
SET IDENTITY_INSERT [dbo].[Enrollment] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (1, N'Alexander', N'Carson', CAST(N'2005-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (2, N'Alonso', N'Meredith', CAST(N'2002-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (3, N'Anand', N'Arturo', CAST(N'2003-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (4, N'Barzdukas', N'Gytis', CAST(N'2002-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (5, N'Li', N'Yan', CAST(N'2002-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (6, N'Justice', N'Peggy', CAST(N'2001-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (7, N'Norman', N'Laura', CAST(N'2003-09-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate]) VALUES (8, N'Olivetto', N'Nino', CAST(N'2005-09-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course_CourseID]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student_StudentID] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Student_StudentID]
GO
