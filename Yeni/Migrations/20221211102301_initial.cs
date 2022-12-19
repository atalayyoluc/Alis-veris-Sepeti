using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yeni.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false),
                    KategoriResmi = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriID);
                });

            migrationBuilder.CreateTable(
                name: "Kullaniciler",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eposta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullaniciler", x => x.KullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    UrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KategoriID = table.Column<int>(type: "int", nullable: false),
                    UrunAciklamasi = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UrunResmi = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.UrunID);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategoriler",
                        column: x => x.KategoriID,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriID");
                });

            migrationBuilder.CreateTable(
                name: "Satisler",
                columns: table => new
                {
                    SatisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satisler", x => x.SatisID);
                    table.ForeignKey(
                        name: "FK_Satisler_Kullaniciler",
                        column: x => x.KullaniciID,
                        principalTable: "Kullaniciler",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "SatisDetaylari",
                columns: table => new
                {
                    SatisID = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    SatisAdi = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    UrunMiktari = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatisDetaylari", x => new { x.SatisID, x.UrunID });
                    table.ForeignKey(
                        name: "FK_SatisDetaylari_Satisler",
                        column: x => x.SatisID,
                        principalTable: "Satisler",
                        principalColumn: "SatisID");
                    table.ForeignKey(
                        name: "FK_SatisDetaylari_Urunler",
                        column: x => x.UrunID,
                        principalTable: "Urunler",
                        principalColumn: "UrunID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SatisDetaylari_UrunID",
                table: "SatisDetaylari",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_Satisler_KullaniciID",
                table: "Satisler",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriID",
                table: "Urunler",
                column: "KategoriID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SatisDetaylari");

            migrationBuilder.DropTable(
                name: "Satisler");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kullaniciler");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
