using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class QuoteItem
    {
        [Key]
        [Display(Name = "Quote Item ID")]
        public string QuoteItemId { get; set; }

        [Display(Name = "Quote")]
        public RequestedQuote RequestedQuote { get; set; }
        public string QuoteId { get; set; }

        [Display(Name = "Quoted Item")]
        public Item Item { get; set; }
        public string ItemId { get; set; }

        [Display(Name = "Quantity")]
        public int qty { get; set; }

        [Display(Name = "Actual Price")]
        public decimal ActualPrice { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}