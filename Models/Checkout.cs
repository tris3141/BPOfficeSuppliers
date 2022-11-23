namespace BPOfficeSupplies.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Checkout
    {
        public int CheckoutId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Please Select A Payment Method")]
        public string Method { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Card Number")]
        public string CardNo { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public int ExpMonth { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public int ExpYear { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        public int SecCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public int Postal { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public int ContactNo { get; set; }
    }
}
