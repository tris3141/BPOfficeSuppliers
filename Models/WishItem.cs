using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class WishItem
    {
        [Key]
        [Required, Display(Name = "Wish Item ID")]
        public string WishItemId { get; set; }

        [Display(Name = "Item ID")]
        public Item Item { get; set; }
        public string ItemId { get; set; }

        [Display(Name = "Customer")]
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        [Required, Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
    }
}