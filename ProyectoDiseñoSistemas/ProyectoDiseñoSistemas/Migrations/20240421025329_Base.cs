using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDiseñoSistemas.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
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
                name: "Provincias",
                columns: table => new
                {
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.IdProvincia);
                });

            migrationBuilder.CreateTable(
                name: "Cantones",
                columns: table => new
                {
                    IdCanton = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    ProvinciaIdProvincia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantones", x => x.IdCanton);
                    table.ForeignKey(
                        name: "FK_Cantones_Provincias_ProvinciaIdProvincia",
                        column: x => x.ProvinciaIdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distritos",
                columns: table => new
                {
                    IdDistrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false),
                    CantonIdCanton = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distritos", x => x.IdDistrito);
                    table.ForeignKey(
                        name: "FK_Distritos_Cantones_CantonIdCanton",
                        column: x => x.CantonIdCanton,
                        principalTable: "Cantones",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    IdBodegas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    ProvinciaIdProvincia = table.Column<int>(type: "int", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false),
                    CantonIdCanton = table.Column<int>(type: "int", nullable: false),
                    IdDistrito = table.Column<int>(type: "int", nullable: false),
                    DistritoIdDistrito = table.Column<int>(type: "int", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.IdBodegas);
                    table.ForeignKey(
                        name: "FK_Bodegas_Cantones_CantonIdCanton",
                        column: x => x.CantonIdCanton,
                        principalTable: "Cantones",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bodegas_Distritos_DistritoIdDistrito",
                        column: x => x.DistritoIdDistrito,
                        principalTable: "Distritos",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bodegas_Provincias_ProvinciaIdProvincia",
                        column: x => x.ProvinciaIdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedores = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    ProvinciaIdProvincia = table.Column<int>(type: "int", nullable: false),
                    IdCanton = table.Column<int>(type: "int", nullable: false),
                    CantonIdCanton = table.Column<int>(type: "int", nullable: false),
                    IdDistrito = table.Column<int>(type: "int", nullable: false),
                    DistritoIdDistrito = table.Column<int>(type: "int", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedores);
                    table.ForeignKey(
                        name: "FK_Proveedores_Cantones_CantonIdCanton",
                        column: x => x.CantonIdCanton,
                        principalTable: "Cantones",
                        principalColumn: "IdCanton",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedores_Distritos_DistritoIdDistrito",
                        column: x => x.DistritoIdDistrito,
                        principalTable: "Distritos",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedores_Provincias_ProvinciaIdProvincia",
                        column: x => x.ProvinciaIdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "IdProvincia",
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
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBodega = table.Column<int>(type: "int", nullable: false),
                    BodegaIdBodegas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Bodegas_BodegaIdBodegas",
                        column: x => x.BodegaIdBodegas,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodegas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    ProveedorIdProveedores = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pendiente = table.Column<bool>(type: "bit", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoDeuda = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Facturas_Proveedores_ProveedorIdProveedores",
                        column: x => x.ProveedorIdProveedores,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedores",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedidos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    ProveedorIdProveedores = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedidos);
                    table.ForeignKey(
                        name: "FK_Pedidos_Proveedores_ProveedorIdProveedores",
                        column: x => x.ProveedorIdProveedores,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedores",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosFacturas",
                columns: table => new
                {
                    IdProductoFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    FacturaIdFactura = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    ProductoIdProductos = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosFacturas", x => x.IdProductoFactura);
                    table.ForeignKey(
                        name: "FK_ProductosFacturas_Facturas_FacturaIdFactura",
                        column: x => x.FacturaIdFactura,
                        principalTable: "Facturas",
                        principalColumn: "IdFactura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosFacturas_Productos_ProductoIdProductos",
                        column: x => x.ProductoIdProductos,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosPedidos",
                columns: table => new
                {
                    IdProductosPedidos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedidos = table.Column<int>(type: "int", nullable: false),
                    PedidoIdPedidos = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    ProductoIdProductos = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPedidos", x => x.IdProductosPedidos);
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Pedidos_PedidoIdPedidos",
                        column: x => x.PedidoIdPedidos,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedidos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosPedidos_Productos_ProductoIdProductos",
                        column: x => x.ProductoIdProductos,
                        principalTable: "Productos",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_CantonIdCanton",
                table: "Bodegas",
                column: "CantonIdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_DistritoIdDistrito",
                table: "Bodegas",
                column: "DistritoIdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_ProvinciaIdProvincia",
                table: "Bodegas",
                column: "ProvinciaIdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Cantones_ProvinciaIdProvincia",
                table: "Cantones",
                column: "ProvinciaIdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Distritos_CantonIdCanton",
                table: "Distritos",
                column: "CantonIdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ProveedorIdProveedores",
                table: "Facturas",
                column: "ProveedorIdProveedores");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_UsuarioIdUsuario",
                table: "Facturas",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProveedorIdProveedores",
                table: "Pedidos",
                column: "ProveedorIdProveedores");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioIdUsuario",
                table: "Pedidos",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosFacturas_FacturaIdFactura",
                table: "ProductosFacturas",
                column: "FacturaIdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosFacturas_ProductoIdProductos",
                table: "ProductosFacturas",
                column: "ProductoIdProductos");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_PedidoIdPedidos",
                table: "ProductosPedidos",
                column: "PedidoIdPedidos");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedidos_ProductoIdProductos",
                table: "ProductosPedidos",
                column: "ProductoIdProductos");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_CantonIdCanton",
                table: "Proveedores",
                column: "CantonIdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_DistritoIdDistrito",
                table: "Proveedores",
                column: "DistritoIdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_ProvinciaIdProvincia",
                table: "Proveedores",
                column: "ProvinciaIdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_BodegaIdBodegas",
                table: "Usuarios",
                column: "BodegaIdBodegas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosFacturas");

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
                name: "Distritos");

            migrationBuilder.DropTable(
                name: "Cantones");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
