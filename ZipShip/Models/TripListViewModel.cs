using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class TripListViewModel
    {
        public string filtertrip { get; set; }


        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Country Name Should be in Alphabets")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "City Name Should be in Alphabets")]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Travelers Name")]
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }

    }
}