using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class Customer
    {
        [Key]
        [Display(Name ="Customer ID")]
        public string CustomerId { get; set; }

        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        [Required, Display(Name = "Email")]
        public string EmailAddress { get; set; }

        public ICollection<WishItem> WishItems { get; set; }
        public ICollection<RequestedQuote> RequestedQuotes { get; set; }
    }
}