using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class BdProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProductos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presentacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadPresentacion = table.Column<int>(type: "int", nullable: false),
                    TipoEmpaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadEmpaque = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProductos);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.IdProvincia);
                });

            migrationBuilder.CreateTable(
                name: "Canton",
                columns: table => new
                {
                    IdCanton = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canton", x => x.IdCanton);
                    table.ForeignKey(
                        name: "FK_Canton_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distrito",
                columns: table => new
                {
                    IdDistrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distrito", x => x.IdDistrito);
                    table.ForeignKey(
                        name: "FK_Distrito_Canton_IdCanton",
                        column: x => x.IdCanton,
                        principalTable: "Canton",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    IdBodegas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DireccionExacta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false),
                    IdDistrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.IdBodegas);
                    table.ForeignKey(
                        name: "FK_Bodegas_Canton_IdCanton",
                        column: x => x.IdCanton,
                        principalTable: "Canton",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodegas_Distrito_IdDistrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodegas_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedores = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false),
                    IdDistrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedores);
                    table.ForeignKey(
                        name: "FK_Proveedores_Canton_IdCanton",
                        column: x => x.IdCanton,
                        principalTable: "Canton",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proveedores_Distrito_IdDistrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proveedores_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductosBodega",
                columns: table => new
                {
                    IdProductoBodega = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBodega = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosBodega", x => x.IdProductoBodega);
                    table.ForeignKey(
                        name: "FK_ProductosBodega_Bodegas_IdBodega",
                        column: x => x.IdBodega,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodegas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosBodega_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBodega = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Bodegas_IdBodega",
                        column: x => x.IdBodega,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodegas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoProveedores",
                columns: table => new
                {
                    IdProductoProveedores = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoProveedores", x => x.IdProductoProveedores);
                    table.ForeignKey(
                        name: "FK_ProductoProveedores_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoProveedores_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedores",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pendiente = table.Column<bool>(type: "bit", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoDeuda = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Facturas_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedores",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedidos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedores = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedidos);
                    table.ForeignKey(
                        name: "FK_Pedidos_Proveedores_IdProveedores",
                        column: x => x.IdProveedores,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedores",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoFactura",
                columns: table => new
                {
                    IdProductoFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoFactura", x => x.IdProductoFactura);
                    table.ForeignKey(
                        name: "FK_ProductoFactura_Facturas_IdFactura",
                        column: x => x.IdFactura,
                        principalTable: "Facturas",
                        principalColumn: "IdFactura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoFactura_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductosPedidos",
                columns: table => new
                {
                    IdProductosPedidos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedidos = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPedidos", x => x.IdProductosPedidos);
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Pedidos_IdPedidos",
                        column: x => x.IdPedidos,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedidos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_IdCanton",
                table: "Bodegas",
                column: "IdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_IdDistrito",
                table: "Bodegas",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_IdProvincia",
                table: "Bodegas",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Canton_IdProvincia",
                table: "Canton",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_IdCanton",
                table: "Distrito",
                column: "IdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdProveedor",
                table: "Facturas",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdUsuario",
                table: "Facturas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdProveedores",
                table: "Pedidos",
                column: "IdProveedores");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoFactura_IdFactura",
                table: "ProductoFactura",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoFactura_IdProducto",
                table: "ProductoFactura",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedores_IdProducto",
                table: "ProductoProveedores",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedores_IdProveedor",
                table: "ProductoProveedores",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosBodega_IdBodega",
                table: "ProductosBodega",
                column: "IdBodega");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosBodega_IdProducto",
                table: "ProductosBodega",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_IdPedidos",
                table: "ProductosPedidos",
                column: "IdPedidos");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_IdProducto",
                table: "ProductosPedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IdCanton",
                table: "Proveedores",
                column: "IdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IdDistrito",
                table: "Proveedores",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IdProvincia",
                table: "Proveedores",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdBodega",
                table: "Usuarios",
                column: "IdBodega");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoFactura");

            migrationBuilder.DropTable(
                name: "ProductoProveedores");

            migrationBuilder.DropTable(
                name: "ProductosBodega");

            migrationBuilder.DropTable(
                name: "ProductosPedidos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Bodegas");

            migrationBuilder.DropTable(
                name: "Distrito");

            migrationBuilder.DropTable(
                name: "Canton");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}
