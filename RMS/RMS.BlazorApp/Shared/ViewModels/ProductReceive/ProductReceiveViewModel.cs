using RMS.BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BlazorApp.Shared.ViewModels.ProductReceive
{
    public class ProductReceiveViewModel
    {
        public int ProductReceiveId { get; set; }

        [Required, StringLength(100)]
        public string CompanyName { get; set; } = default!;

        [Required, Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime ReceivedDate { get; set; }

        public bool CanDelete { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrders { get; set; }

        public int SupplierId { get; set; }

        //[ForeignKey("Supplier")]
        //public Supplier? Suppliers { get; set; }

        public ICollection<ReceivedDetail> ReceivedDetails { get; set; } = new List<ReceivedDetail>();
    }
}
