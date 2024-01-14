using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMS.BlazorApp.Server.Migrations.RestaurantDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "GenericProducts",
                columns: table => new
                {
                    GenericProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenericProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericProducts", x => x.GenericProductId);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRules",
                columns: table => new
                {
                    PurchaseRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReOrderLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRules", x => x.PurchaseRuleId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServingNumber = table.Column<int>(type: "int", nullable: false),
                    RecipeQuantity = table.Column<int>(type: "int", nullable: false),
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "StorageTypes",
                columns: table => new
                {
                    StorageTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialRequirement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageTypes", x => x.StorageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "DailyMenus",
                columns: table => new
                {
                    DailyMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServingQuantity = table.Column<int>(type: "int", nullable: false),
                    ServingDate = table.Column<DateTime>(type: "date", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenus", x => x.DailyMenuId);
                    table.ForeignKey(
                        name: "FK_DailyMenus_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    StorageTypeId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    MeasurementId = table.Column<int>(type: "int", nullable: false),
                    GenericProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseRuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_GenericProducts_GenericProductId",
                        column: x => x.GenericProductId,
                        principalTable: "GenericProducts",
                        principalColumn: "GenericProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_StorageTypes_StorageTypeId",
                        column: x => x.StorageTypeId,
                        principalTable: "StorageTypes",
                        principalColumn: "StorageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchaseRuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_PurchaseRules_PurchaseRuleId",
                        column: x => x.PurchaseRuleId,
                        principalTable: "PurchaseRules",
                        principalColumn: "PurchaseRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientQuantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    RequisitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequisitionQuantity = table.Column<int>(type: "int", nullable: false),
                    DailyMenuId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.RequisitionId);
                    table.ForeignKey(
                        name: "FK_Requisitions_DailyMenus_DailyMenuId",
                        column: x => x.DailyMenuId,
                        principalTable: "DailyMenus",
                        principalColumn: "DailyMenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requisitions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierBillInvoiceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    BillAmount = table.Column<decimal>(type: "money", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsPartialPayment = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductReceives",
                columns: table => new
                {
                    ProductReceiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "date", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReceives", x => x.ProductReceiveId);
                    table.ForeignKey(
                        name: "FK_ProductReceives_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "date", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.PurchaseDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedDetails",
                columns: table => new
                {
                    ReceivedDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "date", nullable: false),
                    UnitOfPrice = table.Column<decimal>(type: "money", nullable: false),
                    Vat = table.Column<decimal>(type: "money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    ProductReceiveId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedDetails", x => x.ReceivedDetailId);
                    table.ForeignKey(
                        name: "FK_ReceivedDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceivedDetails_ProductReceives_ProductReceiveId",
                        column: x => x.ProductReceiveId,
                        principalTable: "ProductReceives",
                        principalColumn: "ProductReceiveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName" },
                values: new object[,]
                {
                    { 1, "Brand A" },
                    { 2, "Brand B" },
                    { 3, "Brand C" }
                });

            migrationBuilder.InsertData(
                table: "GenericProducts",
                columns: new[] { "GenericProductId", "GenericProductName" },
                values: new object[,]
                {
                    { 1, "5-liter Rupchanda Oil" },
                    { 2, "50 kg Bashundhara Atta" },
                    { 3, "5 Kg Deshi Onion" }
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "MeasurementId", "MeasurementType" },
                values: new object[,]
                {
                    { 1, "Liter" },
                    { 2, "Kg" },
                    { 3, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseRules",
                columns: new[] { "PurchaseRuleId", "MaxOrderQuantity", "PurchaseType", "ReOrderLevel" },
                values: new object[,]
                {
                    { 1, 100, 1, "High" },
                    { 2, 50, 3, "Low" },
                    { 3, 75, 2, "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "MeasurementId", "RecipeName", "RecipeQuantity", "ServingNumber" },
                values: new object[,]
                {
                    { 1, 0, "Vegetable Soup", 1, 4 },
                    { 2, 0, "Chicken Pasta", 1, 3 },
                    { 3, 0, "Fruit Salad", 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "StorageTypes",
                columns: new[] { "StorageTypeId", "SpecialRequirement", "StorageTypeName" },
                values: new object[,]
                {
                    { 1, "None", "Dry Storage" },
                    { 2, "Temperature: 4°C", "Refrigerated Storage" },
                    { 3, "Temperature: -18°C", "Freezer Storage" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Address", "ContactNo", "SupplierName" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "555-123-4567", "Supplier A" },
                    { 2, "456 Elm Street", "555-987-6543", "Supplier B" },
                    { 3, "789 Oak Street", "555-456-7890", "Supplier C" }
                });

            migrationBuilder.InsertData(
                table: "DailyMenus",
                columns: new[] { "DailyMenuId", "RecipeId", "ServingDate", "ServingQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 2, 2, new DateTime(2023, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, 3, new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "BrandId", "GenericProductId", "IsAvailable", "ItemName", "MeasurementId", "Picture", "PurchaseRuleId", "StorageTypeId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 1, true, "Oil", 1, "RupchandaOil.jpeg", 1, 1, 5m },
                    { 2, 2, 2, true, "Atta", 2, "BashundharaAtta.webp", 2, 2, 50m },
                    { 3, 3, 3, true, "Onion", 3, "Onion.jpg", 3, 3, 10m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "PurchaseOrderId", "InvoiceNumber", "PurchaseOrderDate", "PurchaseRuleId", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 56789012, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 8725.00m },
                    { 2, 32872280, new DateTime(2023, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 13125.00m },
                    { 3, 84938832, new DateTime(2023, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 1320.00m }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "BillId", "BillAmount", "Date", "IsPartialPayment", "PaidAmount", "PurchaseOrderId", "SupplierBillInvoiceNumber", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 8725.00m, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5000.00m, 1, "BILL-001", 1, 3725.00m },
                    { 2, 13125.00m, new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 10000.00m, 2, "BILL-002", 2, 3125.00m },
                    { 3, 2250.00m, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1500.00m, 3, "BILL-003", 3, 750.00m }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "ItemId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 100 },
                    { 2, 2, 150 },
                    { 3, 3, 75 }
                });

            migrationBuilder.InsertData(
                table: "ProductReceives",
                columns: new[] { "ProductReceiveId", "CompanyName", "PurchaseOrderId", "ReceivedDate", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "Supplier X", 1, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5000.00m },
                    { 2, "Supplier Y", 2, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7500.00m },
                    { 3, "Supplier Z", 3, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3000.00m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseDetails",
                columns: new[] { "PurchaseDetailId", "ExpireDate", "ItemId", "OrderedQuantity", "PurchaseOrderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 50, 1 },
                    { 2, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 75, 2 },
                    { 3, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 60, 3 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "RecipeIngredientId", "IngredientQuantity", "ItemId", "RecipeId" },
                values: new object[,]
                {
                    { 1, 200, 1, 1 },
                    { 2, 300, 2, 1 },
                    { 3, 150, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Requisitions",
                columns: new[] { "RequisitionId", "DailyMenuId", "ItemId", "MeasurementId", "RequisitionQuantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 0, 10 },
                    { 2, 2, 2, 0, 8 },
                    { 3, 3, 3, 0, 12 }
                });

            migrationBuilder.InsertData(
                table: "ReceivedDetails",
                columns: new[] { "ReceivedDetailId", "ExpireDate", "ItemId", "ProductReceiveId", "ReceivedQuantity", "TotalPrice", "UnitOfPrice", "Vat" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 100, 5500.00m, 50.00m, 5.00m },
                    { 2, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 200, 8800.00m, 40.00m, 4.00m },
                    { 3, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 150, 9900.00m, 60.00m, 6.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PurchaseOrderId",
                table: "Bills",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SupplierId",
                table: "Bills",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenus_RecipeId",
                table: "DailyMenus",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_GenericProductId",
                table: "Items",
                column: "GenericProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MeasurementId",
                table: "Items",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StorageTypeId",
                table: "Items",
                column: "StorageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReceives_PurchaseOrderId",
                table: "ProductReceives",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ItemId",
                table: "PurchaseDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseOrderId",
                table: "PurchaseDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PurchaseRuleId",
                table: "PurchaseOrders",
                column: "PurchaseRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedDetails_ItemId",
                table: "ReceivedDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedDetails_ProductReceiveId",
                table: "ReceivedDetails",
                column: "ProductReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_ItemId",
                table: "RecipeIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_DailyMenuId",
                table: "Requisitions",
                column: "DailyMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_ItemId",
                table: "Requisitions",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "ReceivedDetails");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropTable(
                name: "ProductReceives");

            migrationBuilder.DropTable(
                name: "DailyMenus");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "GenericProducts");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "StorageTypes");

            migrationBuilder.DropTable(
                name: "PurchaseRules");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
