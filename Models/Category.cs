using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOfficeSupplies.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category ID")]
        public string CategoryId { get; set; }

        [Required, Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime DateUpdated { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}