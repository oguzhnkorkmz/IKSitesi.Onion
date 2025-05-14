using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IK.InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolumler",
                columns: table => new
                {
                    BolumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolumler", x => x.BolumID);
                });

            migrationBuilder.CreateTable(
                name: "Paketler",
                columns: table => new
                {
                    PaketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaketAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaketSuresi = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KapasiteSayisi = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paketler", x => x.PaketID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kurumlar",
                columns: table => new
                {
                    KurumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PaketID = table.Column<int>(type: "int", nullable: false),
                    PaketBaslangicTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PaketBitisTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PaketAktifMi = table.Column<bool>(type: "bit", nullable: false),
                    VergiNumarasi = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurumlar", x => x.KurumID);
                    table.ForeignKey(
                        name: "FK_Kurumlar_Paketler_PaketID",
                        column: x => x.PaketID,
                        principalTable: "Paketler",
                        principalColumn: "PaketID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumID = table.Column<int>(type: "int", nullable: true),
                    PersonelID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Kurumlar_KurumID",
                        column: x => x.KurumID,
                        principalTable: "Kurumlar",
                        principalColumn: "KurumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonelSoyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KurumID = table.Column<int>(type: "int", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false),
                    KullaniciHesabiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelID);
                    table.ForeignKey(
                        name: "FK_Personeller_AspNetUsers_KullaniciHesabiID",
                        column: x => x.KullaniciHesabiID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Personeller_Bolumler_BolumID",
                        column: x => x.BolumID,
                        principalTable: "Bolumler",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Kurumlar_KurumID",
                        column: x => x.KurumID,
                        principalTable: "Kurumlar",
                        principalColumn: "KurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvansTalebi",
                columns: table => new
                {
                    AvansTalebiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    TalepEdilenTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Onaylimi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvansTalebi", x => x.AvansTalebiID);
                    table.ForeignKey(
                        name: "FK_AvansTalebi_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bordrolar",
                columns: table => new
                {
                    BordroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    AylikMaaş = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kesintiler = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EkOdeme = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetMaaş = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BordroTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordrolar", x => x.BordroID);
                    table.ForeignKey(
                        name: "FK_Bordrolar_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HarcamaTalepleri",
                columns: table => new
                {
                    HarcamaTalebiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    HarcamaTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Onaylimi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarcamaTalepleri", x => x.HarcamaTalebiID);
                    table.ForeignKey(
                        name: "FK_HarcamaTalepleri_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IzinTalepleri",
                columns: table => new
                {
                    IzinTalebiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Onaylimi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SilmeTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    KayitDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzinTalepleri", x => x.IzinTalebiID);
                    table.ForeignKey(
                        name: "FK_IzinTalepleri_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "31df8fa8-f289-4269-b6da-f5f8e9c93b9c", "SiteAdmin", "SITEADMIN" },
                    { 2, "f1207dc8-3d33-4ac5-8bf5-0cdb7c4749be", "KurumAdmin", "KURUMADMIN" },
                    { 3, "cb511a63-b52b-4955-a3ec-5e59c1ffa113", "Personel", "PERSONEL" }
                });

            migrationBuilder.InsertData(
                table: "Bolumler",
                columns: new[] { "BolumID", "BolumAdi", "EklenmeTarihi", "GuncellemeTarihi", "KayitDurumu", "SilmeTarihi" },
                values: new object[,]
                {
                    { 1, "İnsan Kaynakları", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3483), null, 1, null },
                    { 2, "Bilgi Teknolojileri", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3486), null, 1, null },
                    { 3, "Muhasebe", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3488), null, 1, null },
                    { 4, "Pazarlama", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3489), null, 1, null },
                    { 5, "Hukuk", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3490), null, 1, null },
                    { 6, "Satın Alma", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3492), null, 1, null },
                    { 7, "Operasyon", new DateTime(2025, 4, 28, 12, 56, 38, 415, DateTimeKind.Utc).AddTicks(3493), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Paketler",
                columns: new[] { "PaketID", "AktifMi", "EklenmeTarihi", "Fiyat", "GuncellemeTarihi", "KapasiteSayisi", "KayitDurumu", "PaketAdi", "PaketSuresi", "SilmeTarihi" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 4, 28, 12, 56, 38, 416, DateTimeKind.Utc).AddTicks(9823), 199.99m, null, 50, 1, "Temel Paket", 0, null },
                    { 2, true, new DateTime(2025, 4, 28, 12, 56, 38, 416, DateTimeKind.Utc).AddTicks(9826), 399.99m, null, 150, 1, "Standart Paket", 0, null },
                    { 3, true, new DateTime(2025, 4, 28, 12, 56, 38, 416, DateTimeKind.Utc).AddTicks(9827), 699.99m, null, 300, 1, "Premium Paket", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Kurumlar",
                columns: new[] { "KurumID", "Adres", "EklenmeTarihi", "GuncellemeTarihi", "KayitDurumu", "KurumAdi", "PaketAktifMi", "PaketBaslangicTarihi", "PaketBitisTarihi", "PaketID", "SilmeTarihi", "VergiNumarasi" },
                values: new object[] { 1, "Henüz Belirtilmedi", new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Site Admin Kurumu", true, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "0000000000" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "KurumID", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonelID", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "caf11183-2908-4f2b-809c-0437def494fb", "siteadmin@example.com", true, 1, false, null, "SITEADMIN@EXAMPLE.COM", "SITEADMIN", "AQAAAAIAAYagAAAAEB5j7A6RVHJHQgn9e+VLT+qn94Y4DM13yFa4KyKr3csH9GbnL/HgBXhrBRpfvW4mlw==", null, null, false, "e2a66fd5-fb9b-44cb-848c-a7590c924952", false, "siteadmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KurumID",
                table: "AspNetUsers",
                column: "KurumID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AvansTalebi_PersonelID",
                table: "AvansTalebi",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Bordrolar_PersonelID",
                table: "Bordrolar",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_HarcamaTalepleri_PersonelID",
                table: "HarcamaTalepleri",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_IzinTalepleri_PersonelID",
                table: "IzinTalepleri",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Kurumlar_PaketID",
                table: "Kurumlar",
                column: "PaketID");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_BolumID",
                table: "Personeller",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_KullaniciHesabiID",
                table: "Personeller",
                column: "KullaniciHesabiID",
                unique: true,
                filter: "[KullaniciHesabiID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_KurumID",
                table: "Personeller",
                column: "KurumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AvansTalebi");

            migrationBuilder.DropTable(
                name: "Bordrolar");

            migrationBuilder.DropTable(
                name: "HarcamaTalepleri");

            migrationBuilder.DropTable(
                name: "IzinTalepleri");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bolumler");

            migrationBuilder.DropTable(
                name: "Kurumlar");

            migrationBuilder.DropTable(
                name: "Paketler");
        }
    }
}
