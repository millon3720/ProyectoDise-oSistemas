USE [InventarioSistemas]
GO
/****** Object:  Table [dbo].[Bodegas]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bodegas](
	[IdBodegas] [int] IDENTITY(1,1) NOT NULL,
	[DireccionExacta] [nvarchar](max) NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[IdCanton] [int] NOT NULL,
	[IdDistrito] [int] NOT NULL,
 CONSTRAINT [PK_Bodegas] PRIMARY KEY CLUSTERED 
(
	[IdBodegas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Canton]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Canton](
	[IdCanton] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[IdProvincia] [int] NOT NULL,
 CONSTRAINT [PK_Canton] PRIMARY KEY CLUSTERED 
(
	[IdCanton] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distrito]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distrito](
	[IdDistrito] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[IdCanton] [int] NOT NULL,
 CONSTRAINT [PK_Distrito] PRIMARY KEY CLUSTERED 
(
	[IdDistrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[ProveedorIdProveedores] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[UsuarioIdUsuario] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Pendiente] [bit] NOT NULL,
	[MontoTotal] [decimal](18, 2) NOT NULL,
	[MontoDeuda] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[IdPedidos] [int] IDENTITY(1,1) NOT NULL,
	[IdProveedores] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[IdPedidos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoFactura]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoFactura](
	[IdProductoFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [int] NOT NULL,
	[FacturaIdFactura] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[ProductoIdProductos] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_ProductoFactura] PRIMARY KEY CLUSTERED 
(
	[IdProductoFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoProveedores]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoProveedores](
	[IdProductoProveedores] [int] IDENTITY(1,1) NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductoProveedores] PRIMARY KEY CLUSTERED 
(
	[IdProductoProveedores] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdProductos] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Presentacion] [nvarchar](max) NOT NULL,
	[CantidadPresentacion] [int] NOT NULL,
	[TipoEmpaque] [nvarchar](max) NOT NULL,
	[CantidadEmpaque] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IdProductos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductosBodega]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductosBodega](
	[IdProductoBodega] [int] IDENTITY(1,1) NOT NULL,
	[IdBodega] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[FechaIngreso] [datetime2](7) NOT NULL,
	[FechaVencimiento] [datetime2](7) NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_ProductosBodega] PRIMARY KEY CLUSTERED 
(
	[IdProductoBodega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductosPedidos]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductosPedidos](
	[IdProductosPedidos] [int] IDENTITY(1,1) NOT NULL,
	[IdPedidos] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_ProductosPedidos] PRIMARY KEY CLUSTERED 
(
	[IdProductosPedidos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[IdProveedores] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Correo] [nvarchar](max) NOT NULL,
	[Telefono1] [nvarchar](max) NOT NULL,
	[Telefono2] [nvarchar](max) NOT NULL,
	[DireccionExacta] [nvarchar](max) NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[IdCanton] [int] NOT NULL,
	[IdDistrito] [int] NOT NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[IdProveedores] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[IdProvincia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED 
(
	[IdProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 17/8/2024 18:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Correo] [nvarchar](max) NOT NULL,
	[Usuario] [nvarchar](max) NOT NULL,
	[Clave] [nvarchar](max) NOT NULL,
	[Puesto] [nvarchar](max) NOT NULL,
	[IdBodega] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bodegas]  WITH CHECK ADD  CONSTRAINT [FK_Bodegas_Canton_IdCanton] FOREIGN KEY([IdCanton])
REFERENCES [dbo].[Canton] ([IdCanton])
GO
ALTER TABLE [dbo].[Bodegas] CHECK CONSTRAINT [FK_Bodegas_Canton_IdCanton]
GO
ALTER TABLE [dbo].[Bodegas]  WITH CHECK ADD  CONSTRAINT [FK_Bodegas_Distrito_IdDistrito] FOREIGN KEY([IdDistrito])
REFERENCES [dbo].[Distrito] ([IdDistrito])
GO
ALTER TABLE [dbo].[Bodegas] CHECK CONSTRAINT [FK_Bodegas_Distrito_IdDistrito]
GO
ALTER TABLE [dbo].[Bodegas]  WITH CHECK ADD  CONSTRAINT [FK_Bodegas_Provincia_IdProvincia] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincia] ([IdProvincia])
GO
ALTER TABLE [dbo].[Bodegas] CHECK CONSTRAINT [FK_Bodegas_Provincia_IdProvincia]
GO
ALTER TABLE [dbo].[Canton]  WITH CHECK ADD  CONSTRAINT [FK_Canton_Provincia_IdProvincia] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincia] ([IdProvincia])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Canton] CHECK CONSTRAINT [FK_Canton_Provincia_IdProvincia]
GO
ALTER TABLE [dbo].[Distrito]  WITH CHECK ADD  CONSTRAINT [FK_Distrito_Canton_IdCanton] FOREIGN KEY([IdCanton])
REFERENCES [dbo].[Canton] ([IdCanton])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Distrito] CHECK CONSTRAINT [FK_Distrito_Canton_IdCanton]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Proveedores_ProveedorIdProveedores] FOREIGN KEY([ProveedorIdProveedores])
REFERENCES [dbo].[Proveedores] ([IdProveedores])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Proveedores_ProveedorIdProveedores]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Usuarios_UsuarioIdUsuario] FOREIGN KEY([UsuarioIdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Usuarios_UsuarioIdUsuario]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Proveedores_IdProveedores] FOREIGN KEY([IdProveedores])
REFERENCES [dbo].[Proveedores] ([IdProveedores])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Proveedores_IdProveedores]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Usuarios_IdUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Usuarios_IdUsuario]
GO
ALTER TABLE [dbo].[ProductoFactura]  WITH CHECK ADD  CONSTRAINT [FK_ProductoFactura_Facturas_FacturaIdFactura] FOREIGN KEY([FacturaIdFactura])
REFERENCES [dbo].[Facturas] ([IdFactura])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoFactura] CHECK CONSTRAINT [FK_ProductoFactura_Facturas_FacturaIdFactura]
GO
ALTER TABLE [dbo].[ProductoFactura]  WITH CHECK ADD  CONSTRAINT [FK_ProductoFactura_Productos_ProductoIdProductos] FOREIGN KEY([ProductoIdProductos])
REFERENCES [dbo].[Productos] ([IdProductos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoFactura] CHECK CONSTRAINT [FK_ProductoFactura_Productos_ProductoIdProductos]
GO
ALTER TABLE [dbo].[ProductoProveedores]  WITH CHECK ADD  CONSTRAINT [FK_ProductoProveedores_Productos_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProductos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoProveedores] CHECK CONSTRAINT [FK_ProductoProveedores_Productos_IdProducto]
GO
ALTER TABLE [dbo].[ProductoProveedores]  WITH CHECK ADD  CONSTRAINT [FK_ProductoProveedores_Proveedores_IdProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedores] ([IdProveedores])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoProveedores] CHECK CONSTRAINT [FK_ProductoProveedores_Proveedores_IdProveedor]
GO
ALTER TABLE [dbo].[ProductosBodega]  WITH CHECK ADD  CONSTRAINT [FK_ProductosBodega_Bodegas_IdBodega] FOREIGN KEY([IdBodega])
REFERENCES [dbo].[Bodegas] ([IdBodegas])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductosBodega] CHECK CONSTRAINT [FK_ProductosBodega_Bodegas_IdBodega]
GO
ALTER TABLE [dbo].[ProductosBodega]  WITH CHECK ADD  CONSTRAINT [FK_ProductosBodega_Productos_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProductos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductosBodega] CHECK CONSTRAINT [FK_ProductosBodega_Productos_IdProducto]
GO
ALTER TABLE [dbo].[ProductosPedidos]  WITH CHECK ADD  CONSTRAINT [FK_ProductosPedidos_Pedidos_IdPedidos] FOREIGN KEY([IdPedidos])
REFERENCES [dbo].[Pedidos] ([IdPedidos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductosPedidos] CHECK CONSTRAINT [FK_ProductosPedidos_Pedidos_IdPedidos]
GO
ALTER TABLE [dbo].[ProductosPedidos]  WITH CHECK ADD  CONSTRAINT [FK_ProductosPedidos_Productos_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProductos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductosPedidos] CHECK CONSTRAINT [FK_ProductosPedidos_Productos_IdProducto]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Canton_IdCanton] FOREIGN KEY([IdCanton])
REFERENCES [dbo].[Canton] ([IdCanton])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Canton_IdCanton]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Distrito_IdDistrito] FOREIGN KEY([IdDistrito])
REFERENCES [dbo].[Distrito] ([IdDistrito])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Distrito_IdDistrito]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Provincia_IdProvincia] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincia] ([IdProvincia])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Provincia_IdProvincia]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Bodegas_IdBodega] FOREIGN KEY([IdBodega])
REFERENCES [dbo].[Bodegas] ([IdBodegas])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Bodegas_IdBodega]
GO
