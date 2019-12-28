using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Review Should be in Alphabets")]
        [MaxLength(5000)]
        [Required]
        public string Review { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        

    }
}