USE [master]
GO

CREATE DATABASE [ControlCambios2024]
GO
USE [ControlCambios2024]
GO
/****** Object:  Table [dbo].[TablaBaseDatos]    Script Date: 2/4/2024 23:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TablaBaseDatos](
	[IdBaseDatos] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Activa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBaseDatos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TablaControlCambio]    Script Date: 2/4/2024 23:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TablaControlCambio](
	[IdControlCambio] [int] IDENTITY(1,1) NOT NULL,
	[NombreCambio] [nvarchar](50) NULL,
	[IdBaseDatos] [int] NULL,
	[NombreVersion] [nvarchar](50) NULL,
	[IdUsuario] [nvarchar](50) NULL,
	[Fecha] [datetime] NULL,
	[CambiosRealizados] [nvarchar](max) NULL,
	[Categoria] [nvarchar](50) NULL,
 CONSTRAINT [PK__TablaCon__94C60CCDE09A510C] PRIMARY KEY CLUSTERED 
(
	[IdControlCambio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TablaUsuario]    Script Date: 2/4/2024 23:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TablaUsuario](
	[IdUsuario] [nvarchar](50) NOT NULL,
	[NombreUsuario] [nvarchar](100) NULL,
	[Telefono] [nvarchar](20) NULL,
	[CorreoElectronico] [nvarchar](100) NULL,
	[Contrasenia] [nvarchar](100) NULL,
	[Rol] [nvarchar](50) NULL,
	[Autorizado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TablaVersionesBaseDatos]    Script Date: 2/4/2024 23:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TablaVersionesBaseDatos](
	[IdBaseDatos] [int] NOT NULL,
	[NombreVersion] [nvarchar](50) NOT NULL,
	[CambiosSolicitados] [nvarchar](max) NULL,
 CONSTRAINT [PK__TablaVer__8C53B80953F87FA3] PRIMARY KEY CLUSTERED 
(
	[IdBaseDatos] ASC,
	[NombreVersion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TablaControlCambio] ADD  CONSTRAINT [DF__TablaCont__Fecha__412EB0B6]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[TablaControlCambio]  WITH CHECK ADD  CONSTRAINT [FK_TablaControlCambio_TablaUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[TablaUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[TablaControlCambio] CHECK CONSTRAINT [FK_TablaControlCambio_TablaUsuario]
GO
ALTER TABLE [dbo].[TablaControlCambio]  WITH CHECK ADD  CONSTRAINT [FK_TablaControlCambio_TablaVersionesBaseDatos] FOREIGN KEY([IdBaseDatos], [NombreVersion])
REFERENCES [dbo].[TablaVersionesBaseDatos] ([IdBaseDatos], [NombreVersion])
GO
ALTER TABLE [dbo].[TablaControlCambio] CHECK CONSTRAINT [FK_TablaControlCambio_TablaVersionesBaseDatos]
GO
ALTER TABLE [dbo].[TablaVersionesBaseDatos]  WITH CHECK ADD  CONSTRAINT [FK_TablaVersionesBaseDatos_TablaBaseDatos] FOREIGN KEY([IdBaseDatos])
REFERENCES [dbo].[TablaBaseDatos] ([IdBaseDatos])
GO
ALTER TABLE [dbo].[TablaVersionesBaseDatos] CHECK CONSTRAINT [FK_TablaVersionesBaseDatos_TablaBaseDatos]
GO
USE [master]
GO
ALTER DATABASE [ControlCambios2024] SET  READ_WRITE 
GO
