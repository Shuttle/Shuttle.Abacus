using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shuttle.Abacus.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "abacus");

            migrationBuilder.CreateTable(
                name: "Algorithm",
                schema: "abacus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaximumAlgorithmName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MinimumAlgorithmName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Argument",
                schema: "abacus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matrix",
                schema: "abacus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RowArgumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColumnArgumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matrix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                schema: "abacus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpectedResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpectedResultDataTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comparison = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlgorithmConstraint",
                schema: "abacus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArgumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comparison = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlgorithmConstraint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlgorithmConstraint_Algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalSchema: "abacus",
                        principalTable: "Algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlgorithmOperation",
                schema: "abacus",
                columns: table => new
                {
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueProviderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InputParameter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlgorithmOperation", x => new { x.AlgorithmId, x.SequenceNumber });
                    table.ForeignKey(
                        name: "FK_AlgorithmOperation_Algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalSchema: "abacus",
                        principalTable: "Algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArgumentValue",
                schema: "abacus",
                columns: table => new
                {
                    ArgumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentValue", x => new { x.ArgumentId, x.Value });
                    table.ForeignKey(
                        name: "FK_ArgumentValue_Argument_ArgumentId",
                        column: x => x.ArgumentId,
                        principalSchema: "abacus",
                        principalTable: "Argument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatrixConstraint",
                schema: "abacus",
                columns: table => new
                {
                    MatrixId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Axis = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comparison = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixConstraint", x => new { x.MatrixId, x.Axis, x.Index });
                    table.ForeignKey(
                        name: "FK_MatrixConstraint_Matrix_MatrixId",
                        column: x => x.MatrixId,
                        principalSchema: "abacus",
                        principalTable: "Matrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatrixElement",
                schema: "abacus",
                columns: table => new
                {
                    MatrixId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixElement", x => new { x.MatrixId, x.Row, x.Column });
                    table.ForeignKey(
                        name: "FK_MatrixElement_Matrix_MatrixId",
                        column: x => x.MatrixId,
                        principalSchema: "abacus",
                        principalTable: "Matrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestArgument",
                schema: "abacus",
                columns: table => new
                {
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArgumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestArgument", x => new { x.TestId, x.ArgumentId });
                    table.ForeignKey(
                        name: "FK_TestArgument_Test_TestId",
                        column: x => x.TestId,
                        principalSchema: "abacus",
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UX_Algorithm_Name",
                schema: "abacus",
                table: "Algorithm",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlgorithmConstraint_AlgorithmId",
                schema: "abacus",
                table: "AlgorithmConstraint",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "UX_AlgorithmOperation_Id",
                schema: "abacus",
                table: "AlgorithmOperation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Argument_Name",
                schema: "abacus",
                table: "Argument",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Matrix_Name",
                schema: "abacus",
                table: "Matrix",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_MatrixConstraint_Id",
                schema: "abacus",
                table: "MatrixConstraint",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_MatrixElement_Id",
                schema: "abacus",
                table: "MatrixElement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Test_Name",
                schema: "abacus",
                table: "Test",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlgorithmConstraint",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "AlgorithmOperation",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "ArgumentValue",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "MatrixConstraint",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "MatrixElement",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "TestArgument",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "Algorithm",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "Argument",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "Matrix",
                schema: "abacus");

            migrationBuilder.DropTable(
                name: "Test",
                schema: "abacus");
        }
    }
}
