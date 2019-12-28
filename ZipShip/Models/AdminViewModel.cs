using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class AdminViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class AdminDashboardViewModel
    {
        public string Shopper { get; set; }
        public string Traveller { get; set; }
        public string Order { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        [Display(Name ="ZipShip Earnings")]
        public double ZipShipEarning { get; set; }

    }
}