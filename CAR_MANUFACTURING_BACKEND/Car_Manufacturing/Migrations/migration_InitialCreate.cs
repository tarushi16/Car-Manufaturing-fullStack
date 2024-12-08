using Microsoft.EntityFrameworkCore.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Customers table
        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: false),
                Email = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        // Users table
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Username = table.Column<string>(nullable: false),
                Password = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        // CarModels table
        migrationBuilder.CreateTable(
            name: "CarModels",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ModelName = table.Column<string>(nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CarModels", x => x.Id);
            });

        // Orders table
        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(nullable: false),
                CarModelId = table.Column<int>(nullable: false),
                OrderDate = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
                table.ForeignKey(
                    name: "FK_Orders_CarModels_CarModelId",
                    column: x => x.CarModelId,
                    principalTable: "CarModels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Orders_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Additional tables based on your DbContext
        migrationBuilder.CreateTable(
            name: "ProductionOrders",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProductionDate = table.Column<DateTime>(nullable: false),
                Quantity = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductionOrders", x => x.Id);
            });

        
        // Inventory table
        migrationBuilder.CreateTable(
            name: "Inventory",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ItemName = table.Column<string>(nullable: false),
                Quantity = table.Column<int>(nullable: false),
                LastUpdated = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Inventory", x => x.Id);
            });

        // Suppliers table
        migrationBuilder.CreateTable(
            name: "Suppliers",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SupplierName = table.Column<string>(nullable: false),
                ContactInfo = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Suppliers", x => x.Id);
            });

        // QualityReports table
        migrationBuilder.CreateTable(
            name: "QualityReports",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReportDate = table.Column<DateTime>(nullable: false),
                Details = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_QualityReports", x => x.Id);
            });

        // SalesOrders table
        migrationBuilder.CreateTable(
            name: "SalesOrders",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(nullable: false),
                OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                OrderDate = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SalesOrders", x => x.Id);
                table.ForeignKey(
                    name: "FK_SalesOrders_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Finances table
        migrationBuilder.CreateTable(
            name: "Finances",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TransactionDate = table.Column<DateTime>(nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Description = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Finances", x => x.Id);
            });

        // Reports table
        migrationBuilder.CreateTable(
            name: "Reports",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReportName = table.Column<string>(nullable: false),
                GeneratedDate = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reports", x => x.Id);
            });

        // Notifications table
        migrationBuilder.CreateTable(
            name: "Notifications",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Message = table.Column<string>(nullable: false),
                NotificationDate = table.Column<DateTime>(nullable: false),
                IsRead = table.Column<bool>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notifications", x => x.Id);
            });
    
    
    
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Notifications");
        migrationBuilder.DropTable(name: "Reports");
        migrationBuilder.DropTable(name: "Finances");
        migrationBuilder.DropTable(name: "SalesOrders");
        migrationBuilder.DropTable(name: "QualityReports");
        migrationBuilder.DropTable(name: "Suppliers");
        migrationBuilder.DropTable(name: "Inventory");
        migrationBuilder.DropTable(name: "ProductionOrders");
        migrationBuilder.DropTable(name: "Orders");
        migrationBuilder.DropTable(name: "CarModels");
        migrationBuilder.DropTable(name: "Customers");
        migrationBuilder.DropTable(name: "Users");
    }
}

