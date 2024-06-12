USE [OOO_DBChemistShop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufacturerCountry]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufacturerCountry](
	[ManufacturerCountryId] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerCountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ManufacturerCountry] PRIMARY KEY CLUSTERED 
(
	[ManufacturerCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[MedicineManufacturerId] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerCountryId] [int] NOT NULL,
	[ManufacturerName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[MedicineManufacturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicine](
	[MedicineId] [int] IDENTITY(1,1) NOT NULL,
	[MedicineManufacturerId] [int] NOT NULL,
	[MedicineName] [nvarchar](100) NOT NULL,
	[MedicineCost] [float] NOT NULL,
	[MedicineDiscount] [float] NOT NULL,
	[MedicineDiscountMax] [float] NOT NULL,
	[MedicineRank] [float] NOT NULL,
	[MedicineCount] [int] NOT NULL,
	[MedicineCategory] [int] NOT NULL,
	[MedicineDateManufacturing] [date] NOT NULL,
	[MedicineExpirationDate] [int] NOT NULL,
	[MedicineDescription] [nvarchar](110) NULL,
	[MedicinePhoto] [nvarchar](100) NULL,
 CONSTRAINT [PK_Medicine] PRIMARY KEY CLUSTERED 
(
	[MedicineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineOrder]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineOrder](
	[OrderId] [int] NOT NULL,
	[MedicineId] [int] NOT NULL,
	[ProductCount] [int] NOT NULL,
 CONSTRAINT [PK_MedicineOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[MedicineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[DateOrder] [datetime] NOT NULL,
	[DateDelivery] [datetime] NOT NULL,
	[OrderPointId] [int] NOT NULL,
	[OrderClient] [nvarchar](100) NOT NULL,
	[OrderCode] [int] NOT NULL,
	[OrderStatusId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Point]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[PointId] [int] IDENTITY(1,1) NOT NULL,
	[PointAddress] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.06.2024 16:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[UserFullName] [nvarchar](100) NOT NULL,
	[UserLogin] [nvarchar](50) NOT NULL,
	[UserPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (1, N'Жаропонижающее')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (3, N'Антисептик')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (4, N'Иммуностимулирующее')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (5, N'Антибиотики')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (7, N'Противокашлевое')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (8, N'Противовосполительное')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (16, N'Антидепрессанты')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (17, N'Сосудосуживающее')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (18, N'Противовирусное')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ManufacturerCountry] ON 

INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (1, N'Германия')
INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (3, N'США')
INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (4, N'Япония')
INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (5, N'Китай')
INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (9, N'Россия')
INSERT [dbo].[ManufacturerCountry] ([ManufacturerCountryId], [ManufacturerCountryName]) VALUES (10, N'Хорватия')
SET IDENTITY_INSERT [dbo].[ManufacturerCountry] OFF
GO
SET IDENTITY_INSERT [dbo].[Manufacturers] ON 

INSERT [dbo].[Manufacturers] ([MedicineManufacturerId], [ManufacturerCountryId], [ManufacturerName]) VALUES (1, 1, N'Bionorica')
INSERT [dbo].[Manufacturers] ([MedicineManufacturerId], [ManufacturerCountryId], [ManufacturerName]) VALUES (2, 3, N'Reckitt Benckiser Healthcare International Ltd')
INSERT [dbo].[Manufacturers] ([MedicineManufacturerId], [ManufacturerCountryId], [ManufacturerName]) VALUES (13, 9, N'Органика АО')
INSERT [dbo].[Manufacturers] ([MedicineManufacturerId], [ManufacturerCountryId], [ManufacturerName]) VALUES (14, 10, N'ЯДРАН-ГАЛЕНСКИ ЛАБОРАТОРИЙ')
SET IDENTITY_INSERT [dbo].[Manufacturers] OFF
GO
SET IDENTITY_INSERT [dbo].[Medicine] ON 

INSERT [dbo].[Medicine] ([MedicineId], [MedicineManufacturerId], [MedicineName], [MedicineCost], [MedicineDiscount], [MedicineDiscountMax], [MedicineRank], [MedicineCount], [MedicineCategory], [MedicineDateManufacturing], [MedicineExpirationDate], [MedicineDescription], [MedicinePhoto]) VALUES (1, 1, N'Синупрет', 500, 5, 13, 4.8, 4, 8, CAST(N'2023-06-13' AS Date), 12, NULL, N'Синупрет.jpg')
INSERT [dbo].[Medicine] ([MedicineId], [MedicineManufacturerId], [MedicineName], [MedicineCost], [MedicineDiscount], [MedicineDiscountMax], [MedicineRank], [MedicineCount], [MedicineCategory], [MedicineDateManufacturing], [MedicineExpirationDate], [MedicineDescription], [MedicinePhoto]) VALUES (2, 2, N'Нурофен', 300, 10, 6, 4, 125, 1, CAST(N'2023-06-14' AS Date), 36, N'', N'Нурофен.jpg')
INSERT [dbo].[Medicine] ([MedicineId], [MedicineManufacturerId], [MedicineName], [MedicineCost], [MedicineDiscount], [MedicineDiscountMax], [MedicineRank], [MedicineCount], [MedicineCategory], [MedicineDateManufacturing], [MedicineExpirationDate], [MedicineDescription], [MedicinePhoto]) VALUES (3, 1, N'Анаферон', 213, 11, 3, 3, 0, 18, CAST(N'2020-12-01' AS Date), 12, N'Для профилактики.', N'Анаферон.jpg')
INSERT [dbo].[Medicine] ([MedicineId], [MedicineManufacturerId], [MedicineName], [MedicineCost], [MedicineDiscount], [MedicineDiscountMax], [MedicineRank], [MedicineCount], [MedicineCategory], [MedicineDateManufacturing], [MedicineExpirationDate], [MedicineDescription], [MedicinePhoto]) VALUES (15, 13, N'Фенибут', 303, 10, 5, 4, 88, 16, CAST(N'2015-01-13' AS Date), 36, N'', N'Фенибут.jpg')
INSERT [dbo].[Medicine] ([MedicineId], [MedicineManufacturerId], [MedicineName], [MedicineCost], [MedicineDiscount], [MedicineDiscountMax], [MedicineRank], [MedicineCount], [MedicineCategory], [MedicineDateManufacturing], [MedicineExpirationDate], [MedicineDescription], [MedicinePhoto]) VALUES (24, 14, N'Риномарис', 169.9, 15, 10, 4.5, 188, 17, CAST(N'2018-02-08' AS Date), 48, N'', N'Риномарис.jpg')
SET IDENTITY_INSERT [dbo].[Medicine] OFF
GO
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (71, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (72, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (73, 3, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (74, 3, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (75, 3, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (76, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (77, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (78, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (79, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (80, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (81, 3, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (82, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (82, 2, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (82, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (83, 2, 38)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (83, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (84, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (84, 3, 39)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (85, 2, 38)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (86, 2, 50)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (87, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (88, 2, 51)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (88, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (89, 2, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (89, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (90, 2, 60)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (90, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (91, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (92, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (93, 3, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (95, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (97, 3, 5)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (99, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (100, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (101, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (102, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (103, 1, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (103, 2, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (103, 3, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (104, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (104, 3, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (104, 15, 6)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (105, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (106, 1, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (106, 2, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (106, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (106, 15, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (107, 1, 25)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (107, 2, 3)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (107, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (107, 15, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (108, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (109, 15, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (110, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (110, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (110, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (110, 15, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (110, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (111, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (111, 3, 4)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (112, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (113, 3, 4)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (114, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (115, 1, 9)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (116, 2, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (116, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (117, 2, 10)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (118, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (119, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (119, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (120, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (121, 1, 6)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (121, 3, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (122, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (122, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (123, 24, 2)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (124, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (124, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (125, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (126, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (126, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (127, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (127, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (128, 1, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (128, 2, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (1128, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (1129, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (1130, 15, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (1131, 24, 1)
INSERT [dbo].[MedicineOrder] ([OrderId], [MedicineId], [ProductCount]) VALUES (1132, 24, 1)
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (71, CAST(N'2024-04-24T21:21:21.547' AS DateTime), CAST(N'2024-04-27T21:21:21.563' AS DateTime), 2, N'Ефремов  Сергей Пантелеймонович', 659, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (72, CAST(N'2024-05-02T17:30:55.127' AS DateTime), CAST(N'2024-05-06T00:00:00.000' AS DateTime), 3, N'Ефремов  Сергей Пантелеймонович', 456, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (73, CAST(N'2024-05-04T23:05:37.813' AS DateTime), CAST(N'2024-05-07T23:05:37.837' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 348, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (74, CAST(N'2024-05-04T23:05:46.550' AS DateTime), CAST(N'2024-05-07T23:05:46.553' AS DateTime), 4, N'Ефремов  Сергей Пантелеймонович', 224, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (75, CAST(N'2024-05-04T23:06:10.230' AS DateTime), CAST(N'2024-05-07T23:06:10.230' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 991, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (76, CAST(N'2024-05-04T23:09:06.597' AS DateTime), CAST(N'2024-05-07T23:09:06.613' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 780, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (77, CAST(N'2024-05-04T23:10:00.170' AS DateTime), CAST(N'2024-05-07T23:10:00.193' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 910, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (78, CAST(N'2024-05-04T23:16:10.227' AS DateTime), CAST(N'2024-05-07T23:16:10.387' AS DateTime), 5, N'Ефремов  Сергей Пантелеймонович', 261, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (79, CAST(N'2024-05-04T23:17:29.427' AS DateTime), CAST(N'2024-05-07T23:17:29.457' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 848, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (80, CAST(N'2024-05-04T23:19:30.867' AS DateTime), CAST(N'2024-05-07T23:19:30.887' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 205, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (81, CAST(N'2024-05-05T00:30:43.263' AS DateTime), CAST(N'2024-05-08T00:30:43.280' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 824, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (82, CAST(N'2024-05-05T00:31:04.540' AS DateTime), CAST(N'2024-05-08T00:31:04.543' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 584, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (83, CAST(N'2024-05-05T00:32:13.087' AS DateTime), CAST(N'2024-05-08T00:32:13.107' AS DateTime), 6, N'Ефремов  Сергей Пантелеймонович', 303, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (84, CAST(N'2024-05-05T00:33:27.833' AS DateTime), CAST(N'2024-05-08T00:33:27.833' AS DateTime), 2, N'Ефремов  Сергей Пантелеймонович', 566, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (85, CAST(N'2024-05-05T00:34:10.873' AS DateTime), CAST(N'2024-05-11T00:34:10.873' AS DateTime), 3, N'Ефремов  Сергей Пантелеймонович', 571, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (86, CAST(N'2024-05-05T00:37:03.203' AS DateTime), CAST(N'2024-05-08T00:37:03.210' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 130, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (87, CAST(N'2024-05-05T00:37:18.397' AS DateTime), CAST(N'2024-05-11T00:37:18.400' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 519, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (88, CAST(N'2024-05-05T00:40:22.783' AS DateTime), CAST(N'2024-05-11T00:40:22.787' AS DateTime), 4, N'Ефремов  Сергей Пантелеймонович', 522, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (89, CAST(N'2024-05-05T01:00:22.323' AS DateTime), CAST(N'2024-05-08T01:00:22.330' AS DateTime), 3, N'Ефремов  Сергей Пантелеймонович', 416, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (90, CAST(N'2024-05-05T01:00:56.030' AS DateTime), CAST(N'2024-05-11T01:00:56.030' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 686, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (91, CAST(N'2024-05-06T03:23:18.813' AS DateTime), CAST(N'2024-05-09T03:23:18.830' AS DateTime), 1, N'Ефремов  Сергей Пантелеймонович', 566, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (92, CAST(N'2024-05-06T03:24:09.637' AS DateTime), CAST(N'2024-05-09T03:24:09.637' AS DateTime), 2, N'Ефремов  Сергей Пантелеймонович', 417, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (93, CAST(N'2024-05-09T16:15:56.230' AS DateTime), CAST(N'2024-05-12T16:15:56.230' AS DateTime), 4, N'Ефремов  Сергей Пантелеймонович', 165, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (95, CAST(N'2024-05-09T16:44:33.403' AS DateTime), CAST(N'2024-05-12T16:44:33.403' AS DateTime), 5, N'Ефремов  Сергей Пантелеймонович', 275, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (97, CAST(N'2024-05-10T19:42:16.477' AS DateTime), CAST(N'2024-05-13T19:42:16.477' AS DateTime), 1, N'Даниил Васильев Сергеевич', 924, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (99, CAST(N'2024-05-19T00:59:27.233' AS DateTime), CAST(N'2024-05-22T00:00:00.000' AS DateTime), 1, N'Даниил Васильев Сергеевич', 740, 2)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (100, CAST(N'2024-05-19T01:23:09.943' AS DateTime), CAST(N'2024-05-23T00:00:00.000' AS DateTime), 1, N'Даниил Васильев Сергеевич', 649, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (101, CAST(N'2024-05-19T01:28:06.877' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 1, N'Даниил Васильев Сергеевич', 584, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (102, CAST(N'2024-05-19T01:28:41.843' AS DateTime), CAST(N'2024-07-22T01:28:41.843' AS DateTime), 4, N'Даниил Васильев Сергеевич', 883, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (103, CAST(N'2024-05-19T01:33:43.253' AS DateTime), CAST(N'2024-06-22T01:33:43.253' AS DateTime), 1, N'Даниил Васильев Сергеевич', 417, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (104, CAST(N'2024-05-19T02:56:26.277' AS DateTime), CAST(N'2024-05-22T02:56:26.277' AS DateTime), 4, N'Даниил Васильев Сергеевич', 591, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (105, CAST(N'2024-05-19T03:03:18.207' AS DateTime), CAST(N'2024-05-22T03:03:18.207' AS DateTime), 1, N'Даниил Васильев Сергеевич', 876, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (106, CAST(N'2024-05-19T03:16:44.767' AS DateTime), CAST(N'2024-05-22T03:16:44.767' AS DateTime), 2, N'Даниил Васильев Сергеевич', 489, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (107, CAST(N'2024-05-19T03:17:43.210' AS DateTime), CAST(N'2024-05-22T03:17:43.210' AS DateTime), 1, N'Даниил Васильев Сергеевич', 783, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (108, CAST(N'2024-05-19T14:33:12.260' AS DateTime), CAST(N'2024-05-22T14:33:12.260' AS DateTime), 1, N'Даниил Васильев Сергеевич', 885, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (109, CAST(N'2024-05-19T14:36:01.127' AS DateTime), CAST(N'2024-05-22T14:36:01.127' AS DateTime), 1, N'Даниил Васильев Сергеевич', 502, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (110, CAST(N'2024-05-20T17:10:54.417' AS DateTime), CAST(N'2024-05-23T17:10:54.417' AS DateTime), 1, N'Даниил Васильев Сергеевич', 521, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (111, CAST(N'2024-05-20T20:11:57.113' AS DateTime), CAST(N'2024-05-23T20:11:57.113' AS DateTime), 1, N'Даниил Васильев Сергеевич', 223, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (112, CAST(N'2024-05-20T21:24:55.500' AS DateTime), CAST(N'2024-05-23T21:24:55.500' AS DateTime), 1, N'Даниил Васильев Сергеевич', 487, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (113, CAST(N'2024-05-20T21:26:49.873' AS DateTime), CAST(N'2024-05-23T21:26:49.873' AS DateTime), 1, N'Даниил Васильев Сергеевич', 843, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (114, CAST(N'2024-05-20T21:30:48.253' AS DateTime), CAST(N'2024-05-23T00:00:00.000' AS DateTime), 1, N'Даниил Васильев Сергеевич', 964, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (115, CAST(N'2024-05-21T16:55:06.810' AS DateTime), CAST(N'2024-05-24T16:55:06.810' AS DateTime), 1, N'Даниил Васильев Сергеевич', 368, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (116, CAST(N'2024-05-21T18:00:00.080' AS DateTime), CAST(N'2024-05-24T18:00:00.080' AS DateTime), 1, N'Даниил Васильев Сергеевич', 110, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (117, CAST(N'2024-05-21T20:54:52.693' AS DateTime), CAST(N'2024-05-24T20:54:52.693' AS DateTime), 1, N'Даниил Васильев Сергеевич', 608, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (118, CAST(N'2024-05-28T16:46:21.803' AS DateTime), CAST(N'2024-05-31T16:46:21.820' AS DateTime), 1, N'Даниил Васильев Сергеевич', 968, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (119, CAST(N'2024-05-28T16:47:28.030' AS DateTime), CAST(N'2024-05-31T16:47:28.030' AS DateTime), 1, N'Даниил Васильев Сергеевич', 932, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (120, CAST(N'2024-05-28T16:53:14.950' AS DateTime), CAST(N'2024-05-31T16:53:14.970' AS DateTime), 1, N'Даниил Васильев Сергеевич', 783, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (121, CAST(N'2024-05-28T17:02:54.220' AS DateTime), CAST(N'2024-05-31T17:02:54.243' AS DateTime), 1, N'Даниил Васильев Сергеевич', 983, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (122, CAST(N'2024-05-28T17:04:48.733' AS DateTime), CAST(N'2024-05-31T17:04:48.777' AS DateTime), 1, N'Даниил Васильев Сергеевич', 134, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (123, CAST(N'2024-05-28T20:25:50.780' AS DateTime), CAST(N'2024-05-31T20:25:50.780' AS DateTime), 1, N'Даниил Васильев Сергеевич', 289, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (124, CAST(N'2024-05-28T21:05:49.760' AS DateTime), CAST(N'2024-05-31T21:05:49.760' AS DateTime), 1, N'Даниил Васильев Сергеевич', 732, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (125, CAST(N'2024-05-28T23:12:13.123' AS DateTime), CAST(N'2024-05-31T23:12:13.123' AS DateTime), 1, N'Даниил Васильев Сергеевич', 469, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (126, CAST(N'2024-05-29T03:48:25.360' AS DateTime), CAST(N'2024-06-01T03:48:25.360' AS DateTime), 1, N'Лев Львович Ахрамов', 538, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (127, CAST(N'2024-05-30T00:55:13.020' AS DateTime), CAST(N'2024-06-02T00:55:13.020' AS DateTime), 1, N'Даниил Васильев Сергеевич', 230, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (128, CAST(N'2024-05-30T02:41:25.877' AS DateTime), CAST(N'2024-06-02T02:41:25.877' AS DateTime), 1, N'Даниил Васильев Сергеевич', 579, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (1128, CAST(N'2024-06-12T16:25:47.740' AS DateTime), CAST(N'2024-06-15T16:25:47.757' AS DateTime), 1, N'Даниил Васильев Сергеевич', 430, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (1129, CAST(N'2024-06-12T16:26:33.643' AS DateTime), CAST(N'2024-06-15T16:26:33.657' AS DateTime), 1, N'Даниил Васильев Сергеевич', 841, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (1130, CAST(N'2024-06-12T16:31:41.347' AS DateTime), CAST(N'2024-06-15T16:31:41.347' AS DateTime), 1, N'Даниил Васильев Сергеевич', 694, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (1131, CAST(N'2024-06-12T16:38:06.033' AS DateTime), CAST(N'2024-06-15T16:38:06.057' AS DateTime), 1, N'Даниил Васильев Сергеевич', 468, 1)
INSERT [dbo].[Order] ([OrderId], [DateOrder], [DateDelivery], [OrderPointId], [OrderClient], [OrderCode], [OrderStatusId]) VALUES (1132, CAST(N'2024-06-12T16:39:09.387' AS DateTime), CAST(N'2024-06-15T16:39:09.403' AS DateTime), 1, N'Даниил Васильев Сергеевич', 779, 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Point] ON 

INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (1, N'191797, Санкт-Петербург, Кирочная ул., 57')
INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (2, N'191490, Санкт-Петербург, Лиговский пр., 41')
INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (3, N'191517, Санкт-Петербург, Чернорецкий пер., 78')
INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (4, N'19131, Санкт-Петербург, набережная реки Фонтанки, 10')
INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (5, N'191325, Санкт-Петербург, Новгородская ул., 7')
INSERT [dbo].[Point] ([PointId], [PointAddress]) VALUES (6, N'191483, Санкт-Петербург, Боровая ул., 29, ')
SET IDENTITY_INSERT [dbo].[Point] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Клиент')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Сотрудник')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, N'Администратор')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (1, N'Новый ')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (2, N'Завершен')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (1, 1, N'Ефремов  Сергей Пантелеймонович', N'loginDEppn2018', N'6}i+FD')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (2, 1, N'Родионова  Тамара Валентиновна', N'loginDElqb2018', N'RNynil')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (3, 2, N'Миронова  Галина Улебовна', N'loginDEydn2018', N'34I}X9')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (4, 2, N'Сидоров  Роман Иринеевич', N'loginDEijg2018', N'4QlKJW')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (5, 2, N'Ситников  Парфений Всеволодович', N'loginDEdpy2018', N'MJ0W|f')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (6, 2, N'Никонов  Роман Геласьевич', N'loginDEwdm2018', N'&PynqU')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (7, 1, N'Щербаков  Владимир Матвеевич', N'loginDEdup2018', N'JM+2{s')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (8, 2, N'Кулаков  Мартын Михаилович', N'loginDEhbm2018', N'9aObu4')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (9, 3, N'Сазонова  Оксана Лаврентьевна', N'loginDExvq2018', N'hX0wJz')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (10, 2, N'Архипов  Варлам Мэлорович', N'loginDErks2018', N'LQNSjo')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (11, 3, N'Устинова  Ираида Мэлоровна', N'loginDErvb2018', N'ceAf&R')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (12, 3, N'Лукин  Георгий Альбертович', N'loginDEulo2018', N'#ИМЯ?')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (13, 1, N'Кононов  Эдуард Валентинович', N'loginDEgfw2018', N'3c2Ic1')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (14, 2, N'Орехова  Клавдия Альбертовна', N'loginDEmxb2018', N'ZPXcRS')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (15, 2, N'Яковлев  Яков Эдуардович', N'loginDEgeq2018', N'&&Eim0')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (16, 1, N'Воронов  Мэлс Семёнович', N'loginDEkhj2018', N'Pbc0t{')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (17, 1, N'Вишнякова  Ия Данииловна', N'loginDEliu2018', N'32FyTl')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (18, 1, N'Третьяков  Фёдор Вадимович', N'loginDEsmf2018', N'{{O2QG')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (19, 2, N'Макаров  Максим Ильяович', N'loginDEutd2018', N'GbcJvC')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (20, 2, N'Шубина  Маргарита Анатольевна', N'loginDEpgh2018', N'YV2lvh')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (21, 2, N'Блинова  Ангелина Владленовна', N'loginDEvop2018', N'pBP8rO')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (22, 1, N'Воробьёв  Владлен Фролович', N'loginDEwjo2018', N'EQaD|d')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (23, 2, N'Сорокина  Прасковья Фёдоровна', N'loginDEbur2018', N'aZKGeI')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (24, 1, N'Давыдов  Яков Антонович', N'loginDEszw2018', N'EGU{YE')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (25, 1, N'Рыбакова  Евдокия Анатольевна', N'loginDExsu2018', N'*2RMsp')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (56, 1, N'Даниил Васильев Сергеевич', N'DaniilLogin2024', N'DaniilPassword2024')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (79, 1, N'Даниил Васильев Сергеевич', N'DanyaLogin2024', N'DanyaPassword2024')
INSERT [dbo].[Users] ([UserId], [UserRoleId], [UserFullName], [UserLogin], [UserPassword]) VALUES (81, 1, N'Лев Львович Ахрамов', N'AchramovClientLogin', N'AchramovClientPassword')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Manufacturers]  WITH CHECK ADD  CONSTRAINT [FK_Manufacturers_Manufacturers] FOREIGN KEY([ManufacturerCountryId])
REFERENCES [dbo].[ManufacturerCountry] ([ManufacturerCountryId])
GO
ALTER TABLE [dbo].[Manufacturers] CHECK CONSTRAINT [FK_Manufacturers_Manufacturers]
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD  CONSTRAINT [FK_Medicine_Categories] FOREIGN KEY([MedicineCategory])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Medicine] CHECK CONSTRAINT [FK_Medicine_Categories]
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD  CONSTRAINT [FK_Medicine_Manufacturers] FOREIGN KEY([MedicineManufacturerId])
REFERENCES [dbo].[Manufacturers] ([MedicineManufacturerId])
GO
ALTER TABLE [dbo].[Medicine] CHECK CONSTRAINT [FK_Medicine_Manufacturers]
GO
ALTER TABLE [dbo].[MedicineOrder]  WITH CHECK ADD  CONSTRAINT [FK_MedicineOrder_Medicine] FOREIGN KEY([MedicineId])
REFERENCES [dbo].[Medicine] ([MedicineId])
GO
ALTER TABLE [dbo].[MedicineOrder] CHECK CONSTRAINT [FK_MedicineOrder_Medicine]
GO
ALTER TABLE [dbo].[MedicineOrder]  WITH CHECK ADD  CONSTRAINT [FK_MedicineOrder_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[MedicineOrder] CHECK CONSTRAINT [FK_MedicineOrder_Order]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Point] FOREIGN KEY([OrderPointId])
REFERENCES [dbo].[Point] ([PointId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Point]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
