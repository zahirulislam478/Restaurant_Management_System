using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BlazorApp.Models 
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required, StringLength(100)]     
        public string ItemName { get; set; } = default!;

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Weight { get; set; }

        [Required, StringLength(50)]
        public string? Picture { get; set; } = default!;

        public bool? IsAvailable { get; set; }

        [ForeignKey("StorageType")]
        public int StorageTypeId { get; set; }
        public StorageType? StorageType { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand? Brands { get; set; }  

        [ForeignKey("Measurement")]
        public int MeasurementId { get; set; }
        public Measurement? Measurements { get; set; }

        [ForeignKey("GenericProduct")]
        public int GenericProductId { get; set; }
        public GenericProduct? GenericProducts { get; set; }

        //Don't use purchaseRule Navigation to avoid cascade delete
        public int PurchaseRuleId { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public virtual ICollection<ReceivedDetail> ReceivedDetails { get; set; } = new List<ReceivedDetail>(); 

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        public virtual ICollection<Requisition> Requisitions { get; set; } = new List<Requisition>(); 
    }

    public class Inventory
    {
        public int InventoryId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }

    public class Brand
    {
        public int BrandId { get; set; }
        [Required, StringLength(100)]
        public string BrandName { get; set; } = default!;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>(); 
    }

    public class Measurement
    {
        public int MeasurementId { get; set; }

        [Required, StringLength(100)]
        public string MeasurementType { get; set; } = default!;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();

        //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        //public virtual ICollection<Requisition> Requisitions { get; set; } = new List<Requisition>();
    }

    public class StorageType
    {
        public int StorageTypeId { get; set; }

        [Required, StringLength(100)]
        public string StorageTypeName { get; set; } = default!;

        [Required, StringLength(100)]
        public string SpecialRequirement { get; set; } = default!;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }

    public class GenericProduct
    {
        public int GenericProductId { get; set; }

        [Required, StringLength(100)]
        public string GenericProductName { get; set; } = default!;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }

    public enum PurchaseType
    {
        DailyPurchase = 1,
        SeasonalPurchase,
        OnReorderLevel,
    }

    public class PurchaseRule
    {
        public int PurchaseRuleId { get; set; }

        [Required, StringLength(100)]
        public string ReOrderLevel { get; set; } = default!; 

        public int MaxOrderQuantity { get; set; }

        [EnumDataType(typeof(PurchaseType))]
        public PurchaseType PurchaseType { get; set; }

        //Don't use Item collection to avoid cascade delete

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }

    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }

        public int InvoiceNumber { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime? PurchaseOrderDate { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }  
        public Supplier? Suppliers { get; set; }

        [ForeignKey("PurchaseRule")]
        public int PurchaseRuleId { get; set; }
        public PurchaseRule? PurchaseRules { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
         
        public virtual ICollection<ProductReceive> ProductReceives { get; set; } = new List<ProductReceive>();
    }

    public class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }

        public int OrderedQuantity { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime? ExpireDate { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrders { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required, StringLength(100)]
        public string SupplierName { get; set; } = default!; 

        [Required, StringLength(100)]
        public string Address { get; set; } = default!;

        [Required, StringLength(100)]
        public string ContactNo { get; set; } = default!;

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

        //public virtual ICollection<ProductReceive> ProductReceives { get; set; } = new List<ProductReceive>();

        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>(); 
    }

    public class Bill
    {
        public int BillId { get; set; }

        [Required, StringLength(100)]
        public string SupplierBillInvoiceNumber { get; set; } = default!;

        [Required, Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal BillAmount { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal PaidAmount { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public bool IsPartialPayment { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; } 
        public PurchaseOrder? PurchaseOrders { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        
        public Supplier? Suppliers { get; set; } 
    }

    public class ProductReceive 
    {
        public int ProductReceiveId { get; set; } 

        [Required, StringLength(100)]
        public string CompanyName { get; set; } = default!;

        [Required, Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime? ReceivedDate { get; set; } 

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrders { get; set; }

        public int SupplierId { get; set; }

        //[ForeignKey("Supplier")]
        //public Supplier? Suppliers { get; set; }

        public virtual ICollection<ReceivedDetail> ReceivedDetails { get; set; } = new List<ReceivedDetail>(); 
    }

    public class ReceivedDetail 
    {
        public int ReceivedDetailId { get; set; }

        public int ReceivedQuantity { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime? ExpireDate { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal UnitOfPrice { get; set; } 

        [Required, Column(TypeName = "money")]
        public decimal Vat { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("ProductReceive")]
        public int ProductReceiveId { get; set; }
        public ProductReceive? ProductReceives { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Items { get; set; }
    }

    public class Recipe
    {
        public int RecipeId { get; set; }

        [Required, StringLength(100)]
        public string RecipeName { get; set; } = default!;

        public int ServingNumber { get; set; }

        public int RecipeQuantity { get; set; }

        public int MeasurementId { get; set; }

        //[ForeignKey("Measurement")]
        //public Measurement? Measurements { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        public virtual ICollection<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();
    } 

    public class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }

        public int IngredientQuantity { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public Recipe? Recipe { get; set; }    
    }

    public class DailyMenu
    {
        public int DailyMenuId { get; set; }

        public int ServingQuantity { get; set;}

        [Required, Column(TypeName = "date")]
        public DateTime? ServingDate { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public Recipe? Recipes { get; set; } 

        public virtual ICollection<Requisition> Requisitions { get; set; } = new List<Requisition>();
    }

    public class Requisition
    {
        public int RequisitionId { get; set; }

        public int RequisitionQuantity { get; set; } 

        [ForeignKey("DailyMenu")]
        public int DailyMenuId { get; set; } 
        public DailyMenu? DailyMenus { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Items { get; set; }

        public int MeasurementId { get; set; }

        //[ForeignKey("Measurement")]
        //public Measurement? Measurements { get; set; }
    }
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }
        public DbSet<Brand> Brands { get; set; } 
        public DbSet<PurchaseRule> PurchaseRules { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<GenericProduct> GenericProducts { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ProductReceive> ProductReceives { get; set; }
        public DbSet<ReceivedDetail> ReceivedDetails { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<DailyMenu> DailyMenus { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sample data for StorageType
            modelBuilder.Entity<StorageType>().HasData(
            new StorageType
            {
                StorageTypeId = 1,
                StorageTypeName = "Dry Storage",
                SpecialRequirement = "None"
            },
            new StorageType
            {
                StorageTypeId = 2,
                StorageTypeName = "Refrigerated Storage",
                SpecialRequirement = "Temperature: 4°C"
            },
            new StorageType
            {
                StorageTypeId = 3,
                StorageTypeName = "Freezer Storage",
                SpecialRequirement = "Temperature: -18°C"
            });

            // Sample data for Measurement
            modelBuilder.Entity<Measurement>().HasData(
            new Measurement
            {
                MeasurementId = 1,
                MeasurementType = "Liter"
            },
            new Measurement
            {
                MeasurementId = 2,
                MeasurementType = "Kg"
            },
            new Measurement
            {
                MeasurementId = 3,
                MeasurementType = "Kg"
            });

            // Sample data for GenericProduct
            modelBuilder.Entity<GenericProduct>().HasData(
            new GenericProduct
            {
                GenericProductId = 1,
                GenericProductName = "5-liter Rupchanda Oil"
            },
            new GenericProduct
            {
                GenericProductId = 2,
                GenericProductName = "50 kg Bashundhara Atta"
            },
            new GenericProduct
            {
                GenericProductId = 3,
                GenericProductName = "5 Kg Deshi Onion"
            });

            // Sample data for Item
            modelBuilder.Entity<Item>().HasData(
            new Item
            {
                ItemId = 1,
                ItemName = "Oil",
                Weight = 5,
                Picture = "RupchandaOil.jpeg",
                IsAvailable = true,
                BrandId = 1,
                StorageTypeId = 1,
                MeasurementId = 1,
                GenericProductId = 1,
                PurchaseRuleId = 1
            },
            new Item
            {
                ItemId = 2,
                ItemName = "Atta",
                Weight = 50,
                Picture = "BashundharaAtta.webp",
                IsAvailable = true,
                BrandId = 2,
                StorageTypeId = 2,
                MeasurementId = 2,
                GenericProductId = 2,
                PurchaseRuleId = 2
            },
            new Item
            {
                ItemId = 3,
                ItemName = "Onion",
                Weight = 10,
                Picture = "Onion.jpg",
                IsAvailable = true,
                BrandId = 3,
                StorageTypeId = 3,
                MeasurementId = 3,
                GenericProductId = 3,
                PurchaseRuleId = 3
            });


            // Sample data for PurchaseRule
            modelBuilder.Entity<PurchaseRule>().HasData(
            new PurchaseRule
            {
                PurchaseRuleId = 1,
                ReOrderLevel = "High",
                MaxOrderQuantity = 100,
                PurchaseType = PurchaseType.DailyPurchase
            },
            new PurchaseRule
            {
                PurchaseRuleId = 2,
                ReOrderLevel = "Low",
                MaxOrderQuantity = 50,
                PurchaseType = PurchaseType.OnReorderLevel
            },
            new PurchaseRule
            {
                PurchaseRuleId = 3,
                ReOrderLevel = "Medium",
                MaxOrderQuantity = 75,
                PurchaseType = PurchaseType.SeasonalPurchase
            });

            // Sample data for PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>().HasData(
            new PurchaseOrder
            {
                PurchaseOrderId = 1,
                InvoiceNumber = 56789012,
                PurchaseOrderDate = new DateTime(2023, 11, 10),
                TotalAmount = 8725.00m,
                PurchaseRuleId = 1,
                SupplierId = 1
            },
            new PurchaseOrder
            {
                PurchaseOrderId = 2,
                InvoiceNumber = 32872280,
                PurchaseOrderDate = new DateTime(2023, 11, 09),
                TotalAmount = 13125.00m,
                PurchaseRuleId = 2,
                SupplierId = 2
            },
            new PurchaseOrder
            {
                PurchaseOrderId = 3,
                InvoiceNumber = 84938832,
                PurchaseOrderDate = new DateTime(2023, 11, 13),
                TotalAmount = 1320.00m,
                PurchaseRuleId = 3,
                SupplierId = 3
            });

            // Sample data for PurchaseDetail
            modelBuilder.Entity<PurchaseDetail>().HasData(
            new PurchaseDetail
            {
                PurchaseDetailId = 1,
                OrderedQuantity = 50,
                ExpireDate = new DateTime(2024, 12, 31),
                ItemId = 1,
                PurchaseOrderId = 1
            },
            new PurchaseDetail
            {
                PurchaseDetailId = 2,
                OrderedQuantity = 75,
                ExpireDate = new DateTime(2024, 12, 31),
                ItemId = 2,
                PurchaseOrderId = 2
            },
            new PurchaseDetail
            {
                PurchaseDetailId = 3,
                OrderedQuantity = 60,
                ExpireDate = new DateTime(2024, 12, 31),
                ItemId = 3,
                PurchaseOrderId = 3
            });

            // Sample data for Supplier
            modelBuilder.Entity<Supplier>().HasData(
            new Supplier
            {
                SupplierId = 1,
                SupplierName = "Supplier A",
                Address = "123 Main Street",
                ContactNo = "555-123-4567"
            },
            new Supplier
            {
                SupplierId = 2,
                SupplierName = "Supplier B",
                Address = "456 Elm Street",
                ContactNo = "555-987-6543"
            },
            new Supplier
            {
                SupplierId = 3,
                SupplierName = "Supplier C",
                Address = "789 Oak Street",
                ContactNo = "555-456-7890"
            });

            // Sample data for Inventory
            modelBuilder.Entity<Inventory>().HasData(
            new Inventory
            {
                InventoryId = 1,
                Quantity = 100,
                ItemId = 1
            },
            new Inventory
            {
                InventoryId = 2,
                Quantity = 150,
                ItemId = 2
            },
            new Inventory
            {
                InventoryId = 3,
                Quantity = 75,
                ItemId = 3
            });

            // Sample data for Bill
            modelBuilder.Entity<Bill>().HasData(
            new Bill
            {
                BillId = 1,
                SupplierBillInvoiceNumber = "BILL-001",
                Date = new DateTime(2023, 1, 20),
                BillAmount = 8725.00m,
                PaidAmount = 5000.00m,
                TotalAmount = 3725.00m,
                IsPartialPayment = false,
                PurchaseOrderId=1,
                SupplierId = 1
            },
            new Bill
            {
                BillId = 2,
                SupplierBillInvoiceNumber = "BILL-002",
                Date = new DateTime(2023, 2, 25),
                BillAmount = 13125.00m,
                PaidAmount = 10000.00m,
                TotalAmount = 3125.00m,
                IsPartialPayment = false,
                PurchaseOrderId = 2,
                SupplierId = 2
            },
            new Bill
            {
                BillId = 3,
                SupplierBillInvoiceNumber = "BILL-003",
                Date = new DateTime(2023, 3, 5),
                BillAmount = 2250.00m,
                PaidAmount = 1500.00m,
                TotalAmount = 750.00m,
                IsPartialPayment = false,
                PurchaseOrderId = 3,
                SupplierId = 3
            });

            // Sample data for ProductReceive
            modelBuilder.Entity<ProductReceive>().HasData(
            new ProductReceive
            {
                ProductReceiveId = 1,
                CompanyName = "Supplier X",
                TotalAmount = 5000.00m,
                ReceivedDate = new DateTime(2023, 3, 5),
                PurchaseOrderId = 1,
                SupplierId = 1
            },
            new ProductReceive
            {
                ProductReceiveId = 2,
                CompanyName = "Supplier Y",
                TotalAmount = 7500.00m,
                ReceivedDate = new DateTime(2023, 4, 4),
                PurchaseOrderId = 2,
                SupplierId = 2
            },
            new ProductReceive
            {
                ProductReceiveId = 3,
                CompanyName = "Supplier Z",
                TotalAmount = 3000.00m,
                ReceivedDate = new DateTime(2023, 5, 7),
                PurchaseOrderId = 3,
                SupplierId = 3
            });

            // Sample data for ReceivedDetail
            modelBuilder.Entity<ReceivedDetail>().HasData(
            new ReceivedDetail
            {
                ReceivedDetailId = 1,
                ReceivedQuantity = 100,
                ExpireDate = new DateTime(2024, 12, 31),
                UnitOfPrice = 50.00m,
                Vat = 5.00m,
                TotalPrice = 5500.00m,
                ProductReceiveId = 1,
                ItemId = 1
            },
            new ReceivedDetail
            {
                ReceivedDetailId = 2,
                ReceivedQuantity = 200,
                ExpireDate = new DateTime(2024, 11, 30),
                UnitOfPrice = 40.00m,
                Vat = 4.00m,
                TotalPrice = 8800.00m,
                ProductReceiveId = 2,
                ItemId = 2
            },
            new ReceivedDetail
            {
                ReceivedDetailId = 3,
                ReceivedQuantity = 150,
                ExpireDate = new DateTime(2025, 1, 31),
                UnitOfPrice = 60.00m,
                Vat = 6.00m,
                TotalPrice = 9900.00m,
                ProductReceiveId = 3,
                ItemId = 3
            });

            // Sample data for Recipe
            modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                RecipeId = 1,
                RecipeName = "Vegetable Soup",
                ServingNumber = 4,
                RecipeQuantity = 1
            },
            new Recipe
            {
                RecipeId = 2,
                RecipeName = "Chicken Pasta",
                ServingNumber = 3,
                RecipeQuantity = 1
            },
            new Recipe
            {
                RecipeId = 3,
                RecipeName = "Fruit Salad",
                ServingNumber = 5,
                RecipeQuantity = 1
            });

            // Sample data for Requisition
            modelBuilder.Entity<Requisition>().HasData(
            new Requisition
            {
                RequisitionId = 1,
                RequisitionQuantity = 10,
                DailyMenuId = 1,
                ItemId =1,
            },
            new Requisition
            {
                RequisitionId = 2,
                RequisitionQuantity = 8,
                DailyMenuId = 2,
                ItemId = 2
            },
            new Requisition
            {
                RequisitionId = 3,
                RequisitionQuantity = 12,
                DailyMenuId = 3,
                ItemId = 3
            });

            // Sample data for RecipeIngredient
            modelBuilder.Entity<RecipeIngredient>().HasData(
            new RecipeIngredient
            {
                RecipeIngredientId = 1,
                IngredientQuantity = 200,
                ItemId = 1,
                RecipeId = 1
            },
            new RecipeIngredient
            {
                RecipeIngredientId = 2,
                IngredientQuantity = 300,
                ItemId = 2,
                RecipeId = 1
            },
            new RecipeIngredient
            {
                RecipeIngredientId = 3,
                IngredientQuantity = 150,
                ItemId = 3,
                RecipeId = 2
            });

            // Sample data for DailyMenu
            modelBuilder.Entity<DailyMenu>().HasData(
            new DailyMenu
            {
                DailyMenuId = 1,
                ServingQuantity = 4,
                ServingDate = new DateTime(2023, 11, 16),
                RecipeId = 1
            },
            new DailyMenu
            {
                DailyMenuId = 2,
                ServingQuantity = 3,
                ServingDate = new DateTime(2023, 11, 17),
                RecipeId = 2
            },
            new DailyMenu
            {
                DailyMenuId = 3,
                ServingQuantity = 5,
                ServingDate = new DateTime(2023, 11, 18),
                RecipeId = 3
            });

            modelBuilder.Entity<Brand>().HasData(
            new Brand
            {
                BrandId = 1,
                BrandName = "Brand A"
            },
            new Brand
            {
                BrandId = 2,
                BrandName = "Brand B"
            },
            new Brand
            {
                BrandId = 3,
                BrandName = "Brand C"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
