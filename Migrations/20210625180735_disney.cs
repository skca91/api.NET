using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.NET.Migrations
{
    public partial class disney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imagen = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.IdPelicula);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Personajes",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imagen = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<float>(type: "float", nullable: false),
                    Historia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.IdPersonaje);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imagen = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                    table.ForeignKey(
                        name: "FK_Generos_Peliculas_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Peliculas",
                        principalColumn: "IdPelicula",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PeliculaPersonaje",
                columns: table => new
                {
                    PeliculasIdPelicula = table.Column<int>(type: "int", nullable: false),
                    PersonajesIdPersonaje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaPersonaje", x => new { x.PeliculasIdPelicula, x.PersonajesIdPersonaje });
                    table.ForeignKey(
                        name: "FK_PeliculaPersonaje_Peliculas_PeliculasIdPelicula",
                        column: x => x.PeliculasIdPelicula,
                        principalTable: "Peliculas",
                        principalColumn: "IdPelicula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaPersonaje_Personajes_PersonajesIdPersonaje",
                        column: x => x.PersonajesIdPersonaje,
                        principalTable: "Personajes",
                        principalColumn: "IdPersonaje",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_IdPelicula",
                table: "Generos",
                column: "IdPelicula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaPersonaje_PersonajesIdPersonaje",
                table: "PeliculaPersonaje",
                column: "PersonajesIdPersonaje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "PeliculaPersonaje");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Personajes");
        }
    }
}
