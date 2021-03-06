USE [master]
GO
/****** Object:  Database [concesionario]    Script Date: 29/11/2020 2:43:57 ******/
CREATE DATABASE [concesionario]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'concesionario', FILENAME = N'D:\Programas\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\concesionario.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'concesionario_log', FILENAME = N'D:\Programas\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\concesionario_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [concesionario] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [concesionario].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [concesionario] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [concesionario] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [concesionario] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [concesionario] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [concesionario] SET ARITHABORT OFF 
GO
ALTER DATABASE [concesionario] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [concesionario] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [concesionario] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [concesionario] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [concesionario] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [concesionario] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [concesionario] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [concesionario] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [concesionario] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [concesionario] SET  DISABLE_BROKER 
GO
ALTER DATABASE [concesionario] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [concesionario] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [concesionario] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [concesionario] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [concesionario] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [concesionario] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [concesionario] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [concesionario] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [concesionario] SET  MULTI_USER 
GO
ALTER DATABASE [concesionario] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [concesionario] SET DB_CHAINING OFF 
GO
ALTER DATABASE [concesionario] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [concesionario] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [concesionario] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [concesionario] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [concesionario] SET QUERY_STORE = OFF
GO
USE [concesionario]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[dni] [char](9) NOT NULL,
	[nombre] [char](15) NOT NULL,
	[apellidos] [char](20) NOT NULL,
	[direccion] [char](40) NOT NULL,
	[telefono] [int] NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[coches]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[coches](
	[matricula] [char](7) NOT NULL,
	[usado] [int] NOT NULL,
	[modelo] [nchar](25) NOT NULL,
	[marca] [nchar](15) NOT NULL,
	[color] [nchar](25) NOT NULL,
	[kilometros] [int] NULL,
	[anioMatriculacion] [date] NULL,
	[numeroUnidades] [int] NULL,
 CONSTRAINT [PK_coches] PRIMARY KEY CLUSTERED 
(
	[matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[datosReparacion]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[datosReparacion](
	[idReparacion] [int] NOT NULL,
	[dniMecanico] [char](9) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fichaReparaciones]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fichaReparaciones](
	[matricula] [char](7) NOT NULL,
	[horas] [int] NOT NULL,
	[idReparacion] [int] NOT NULL,
	[fecha] [date] NOT NULL,
 CONSTRAINT [PK_fichaReparaciones] PRIMARY KEY CLUSTERED 
(
	[idReparacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fichaVentas]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fichaVentas](
	[matricula] [char](7) NOT NULL,
	[dniCliente] [char](9) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mecanicos]    Script Date: 29/11/2020 2:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mecanicos](
	[dni] [char](9) NOT NULL,
	[nombre] [nchar](15) NOT NULL,
	[apellidos] [nchar](20) NOT NULL,
	[fechaContratacion] [date] NOT NULL,
	[salario] [numeric](6, 2) NOT NULL,
 CONSTRAINT [PK_mecanicos] PRIMARY KEY CLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[datosReparacion]  WITH CHECK ADD  CONSTRAINT [FK_datosReparacion_fichaReparaciones] FOREIGN KEY([idReparacion])
REFERENCES [dbo].[fichaReparaciones] ([idReparacion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[datosReparacion] CHECK CONSTRAINT [FK_datosReparacion_fichaReparaciones]
GO
ALTER TABLE [dbo].[datosReparacion]  WITH CHECK ADD  CONSTRAINT [FK_datosReparacion_mecanicos] FOREIGN KEY([dniMecanico])
REFERENCES [dbo].[mecanicos] ([dni])
GO
ALTER TABLE [dbo].[datosReparacion] CHECK CONSTRAINT [FK_datosReparacion_mecanicos]
GO
ALTER TABLE [dbo].[fichaReparaciones]  WITH CHECK ADD  CONSTRAINT [FK_fichaReparaciones_coches] FOREIGN KEY([matricula])
REFERENCES [dbo].[coches] ([matricula])
GO
ALTER TABLE [dbo].[fichaReparaciones] CHECK CONSTRAINT [FK_fichaReparaciones_coches]
GO
ALTER TABLE [dbo].[fichaVentas]  WITH CHECK ADD  CONSTRAINT [FK_fichaCompras_cliente] FOREIGN KEY([dniCliente])
REFERENCES [dbo].[cliente] ([dni])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[fichaVentas] CHECK CONSTRAINT [FK_fichaCompras_cliente]
GO
ALTER TABLE [dbo].[fichaVentas]  WITH CHECK ADD  CONSTRAINT [FK_fichaCompras_coches] FOREIGN KEY([matricula])
REFERENCES [dbo].[coches] ([matricula])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[fichaVentas] CHECK CONSTRAINT [FK_fichaCompras_coches]
GO
USE [master]
GO
ALTER DATABASE [concesionario] SET  READ_WRITE 
GO
