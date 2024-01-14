using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMS.BlazorApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMS.BlazorApp.Server.ViewModels.Item
{
    public class ItemViewModel 
    {
        public int ItemId { get; set; }

        [Required, StringLength(100)]
        public string ItemName { get; set; } = default!;

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Weight { get; set; }

        [StringLength(50)]
        public string? Picture { get; set; } = default!;

        public bool? IsAvailable { get; set; }

        public bool CanDelete { get; set; }

        [ForeignKey("StorageType")]
        public int StorageTypeId { get; set; }
        public StorageType? StorageType { get; set; }

        public string StorageTypeName { get; set; } = default!;

        [ForeignKey("Measurement")]
        public int MeasurementId { get; set; }
        public Measurement? Measurements { get; set; }

        public string MeasurementType { get; set; } = default!;

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand? Brands { get; set; }

        public string BrandName { get; set; } = default!;

        [ForeignKey("GenericProduct")]
        public int GenericProductId { get; set; }
        public GenericProduct? GenericProducts { get; set; }

        public string GenericProductName { get; set; } = default!;

        //Don't use purchaseRule navigation to avoid cascade delete
        public int PurchaseRuleId { get; set; }

        public PurchaseRule? PurchaseRules { get; set; } 

        public string ReOrderLevel { get; set; } = default!;

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
