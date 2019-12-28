using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class OrderViewModel
    {

        [Display(Name = "Add Image")]
        public HttpPostedFileBase Image { get; set; }
        [Display(Name = "Order Image")]
        public string ImagePath { get; set; }


        public string Status { get; set; }
        [Display(Name = "Order Id")]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Order Name Should be Alphabetical")]
        [Display(Name = "Name*")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Order Country Should be Alphabetical")]
        [Display(Name = "Country*")]
        public string Country { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Order Brand Should be Alphabetical")]
        [Display(Name = "Brand*")]
        public string Brand { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Quantity should be Digits")]
        [Display(Name = "Quantity*")]
        public string Quantity { get; set; }


        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Price should be Digits")]
        [Display(Name = "Price*")]
        public double Price { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "DealPrice should be Digits")]
        [Display(Name = "Deal Price*")]
        public double DealPrice { get; set; }
        [Display(Name = "Order By")]
        public string AddedByName { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        
    }
}