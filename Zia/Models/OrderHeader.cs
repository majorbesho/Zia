using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class OrderHeader
    {
        [Key] 
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")] 
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public double OrderTotalOrginal { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]

        public double orderTotal { get; set; }

        public DateTime PickUptime { get; set; }
        [NotMapped]
        public DateTime PickupDate { get; set; }

        public string CoupinCode { get; set; }
        public double coupinDiscount { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }

        public string Comments { get; set; }
        public string TelePhone { get; set; }

        public string TransactionId { get; set; }
        public string PickupName { get; set; }









    }
}
