using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "Item ID")]
        public string ItemId { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        
        [Required, Display(Name = "Item Number")]
        public string ItemNumber { get; set; }

        [Required, Display(Name = "Description")]
        public string Description { get; set; }

        [Required, Display(Name = "Picture")]
        public string ItemPicture { get; set; }

        //[Display(Name = "Quantity In Stock")]
        //public int QuantityInStock { get; set; }

        //[Display(Name = "Customer Number")]
        //public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }

        public ICollection<WishItem> WishItems { get; set; }
        public ICollection<QuoteItem> QuoteItems { get; set; }
    }
}