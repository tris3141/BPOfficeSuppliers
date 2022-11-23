using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class RequestedQuote
    {
        [Key]
        [Required, Display(Name = "Quote ID")]
        public string QuoteId { get; set; }

        [Display(Name = "Quote Number")]
        public string QuoteNumber { get; set; }

        [Display(Name = "Quoted By")]
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        [Display(Name = "Date Quoted")]
        public DateTime DateQuoted { get; set; }

        [Display(Name = "Request Status")]
        public string RequestStatus { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public ICollection<QuoteItem> QuoteItems { get; set; }
    }
}