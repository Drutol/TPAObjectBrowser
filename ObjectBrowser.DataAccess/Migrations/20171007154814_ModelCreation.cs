using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ObjectBrowser.DataAccess.Migrations
{
    public partial class ModelCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Assemblies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MethodModifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAbstract = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStatic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVirtual = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodModifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeModifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAbstract = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSealed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeModifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NamespaceMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamespaceName = table.Column<string>(type: "TEXT", nullable: true),
                    RootAssemblyId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamespaceMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamespaceMetadatas_Assemblies_RootAssemblyId",
                        column: x => x.RootAssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MethodMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Extension = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiersId = table.Column<long>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentTypeAId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeBId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeCId = table.Column<long>(type: "INTEGER", nullable: true),
                    ReturnTypeId = table.Column<long>(type: "INTEGER", nullable: true),
                    RootAssemblyId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodMetadatas_MethodModifiers_ModifiersId",
                        column: x => x.ModifiersId,
                        principalTable: "MethodModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MethodMetadatas_Assemblies_RootAssemblyId",
                        column: x => x.RootAssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModifiersId = table.Column<long>(type: "INTEGER", nullable: true),
                    NamespaceId = table.Column<long>(type: "INTEGER", nullable: true),
                    NamespaceName = table.Column<string>(type: "TEXT", nullable: true),
                    ParentMethodId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeAId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeBId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeCId = table.Column<long>(type: "INTEGER", nullable: true),
                    ParentTypeEId = table.Column<long>(type: "INTEGER", nullable: true),
                    RootAssemblyId = table.Column<long>(type: "INTEGER", nullable: true),
                    TypeHash = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeKind = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", nullable: true),
                    TypeReference = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_TypeModifiers_ModifiersId",
                        column: x => x.ModifiersId,
                        principalTable: "TypeModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_NamespaceMetadatas_NamespaceId",
                        column: x => x.NamespaceId,
                        principalTable: "NamespaceMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_MethodMetadatas_ParentMethodId",
                        column: x => x.ParentMethodId,
                        principalTable: "MethodMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_TypeMetadatas_ParentTypeAId",
                        column: x => x.ParentTypeAId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_TypeMetadatas_ParentTypeBId",
                        column: x => x.ParentTypeBId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_TypeMetadatas_ParentTypeCId",
                        column: x => x.ParentTypeCId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_TypeMetadatas_ParentTypeEId",
                        column: x => x.ParentTypeEId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMetadatas_Assemblies_RootAssemblyId",
                        column: x => x.RootAssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnumFieldMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentTypeId = table.Column<long>(type: "INTEGER", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumFieldMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnumFieldMetadatas_TypeMetadatas_ParentTypeId",
                        column: x => x.ParentTypeId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentTypeId = table.Column<long>(type: "INTEGER", nullable: true),
                    TypeMetadataId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldMetadatas_TypeMetadatas_ParentTypeId",
                        column: x => x.ParentTypeId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldMetadatas_TypeMetadatas_TypeMetadataId",
                        column: x => x.TypeMetadataId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParameterMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MethodMetadataId = table.Column<long>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TypeMetadataId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterMetadatas_MethodMetadatas_MethodMetadataId",
                        column: x => x.MethodMetadataId,
                        principalTable: "MethodMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterMetadatas_TypeMetadatas_TypeMetadataId",
                        column: x => x.TypeMetadataId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentTypeId = table.Column<long>(type: "INTEGER", nullable: true),
                    RootAssemblyId = table.Column<long>(type: "INTEGER", nullable: true),
                    TypeMetadataId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyMetadatas_TypeMetadatas_ParentTypeId",
                        column: x => x.ParentTypeId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyMetadatas_Assemblies_RootAssemblyId",
                        column: x => x.RootAssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyMetadatas_TypeMetadatas_TypeMetadataId",
                        column: x => x.TypeMetadataId,
                        principalTable: "TypeMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnumFieldMetadatas_ParentTypeId",
                table: "EnumFieldMetadatas",
                column: "ParentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMetadatas_ParentTypeId",
                table: "FieldMetadatas",
                column: "ParentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMetadatas_TypeMetadataId",
                table: "FieldMetadatas",
                column: "TypeMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_ModifiersId",
                table: "MethodMetadatas",
                column: "ModifiersId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_ParentTypeAId",
                table: "MethodMetadatas",
                column: "ParentTypeAId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_ParentTypeBId",
                table: "MethodMetadatas",
                column: "ParentTypeBId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_ParentTypeCId",
                table: "MethodMetadatas",
                column: "ParentTypeCId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_ReturnTypeId",
                table: "MethodMetadatas",
                column: "ReturnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMetadatas_RootAssemblyId",
                table: "MethodMetadatas",
                column: "RootAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_NamespaceMetadatas_RootAssemblyId",
                table: "NamespaceMetadatas",
                column: "RootAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterMetadatas_MethodMetadataId",
                table: "ParameterMetadatas",
                column: "MethodMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterMetadatas_TypeMetadataId",
                table: "ParameterMetadatas",
                column: "TypeMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMetadatas_ParentTypeId",
                table: "PropertyMetadatas",
                column: "ParentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMetadatas_RootAssemblyId",
                table: "PropertyMetadatas",
                column: "RootAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMetadatas_TypeMetadataId",
                table: "PropertyMetadatas",
                column: "TypeMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ModifiersId",
                table: "TypeMetadatas",
                column: "ModifiersId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_NamespaceId",
                table: "TypeMetadatas",
                column: "NamespaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ParentMethodId",
                table: "TypeMetadatas",
                column: "ParentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ParentTypeAId",
                table: "TypeMetadatas",
                column: "ParentTypeAId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ParentTypeBId",
                table: "TypeMetadatas",
                column: "ParentTypeBId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ParentTypeCId",
                table: "TypeMetadatas",
                column: "ParentTypeCId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_ParentTypeEId",
                table: "TypeMetadatas",
                column: "ParentTypeEId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMetadatas_RootAssemblyId",
                table: "TypeMetadatas",
                column: "RootAssemblyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeAId",
                table: "MethodMetadatas",
                column: "ParentTypeAId",
                principalTable: "TypeMetadatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeBId",
                table: "MethodMetadatas",
                column: "ParentTypeBId",
                principalTable: "TypeMetadatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeCId",
                table: "MethodMetadatas",
                column: "ParentTypeCId",
                principalTable: "TypeMetadatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ReturnTypeId",
                table: "MethodMetadatas",
                column: "ReturnTypeId",
                principalTable: "TypeMetadatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeAId",
                table: "MethodMetadatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeBId",
                table: "MethodMetadatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ParentTypeCId",
                table: "MethodMetadatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodMetadatas_TypeMetadatas_ReturnTypeId",
                table: "MethodMetadatas");

            migrationBuilder.DropTable(
                name: "EnumFieldMetadatas");

            migrationBuilder.DropTable(
                name: "FieldMetadatas");

            migrationBuilder.DropTable(
                name: "ParameterMetadatas");

            migrationBuilder.DropTable(
                name: "PropertyMetadatas");

            migrationBuilder.DropTable(
                name: "TypeMetadatas");

            migrationBuilder.DropTable(
                name: "TypeModifiers");

            migrationBuilder.DropTable(
                name: "NamespaceMetadatas");

            migrationBuilder.DropTable(
                name: "MethodMetadatas");

            migrationBuilder.DropTable(
                name: "MethodModifiers");

            migrationBuilder.DropTable(
                name: "Assemblies");
        }
    }
}
