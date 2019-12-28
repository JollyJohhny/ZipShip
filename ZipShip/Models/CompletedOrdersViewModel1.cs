using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class CompletedOrdersViewModel1
    {
        public int id { get; set; }
        public string TravellerId { get; set; }
        public string ShopperId { get; set; }
        public string ShopperName { get; set; }
        public string TravellerName { get; set; }
        public string ImagePath { get; set; }
        public string OrderName { get; set; }

        public string OrderCountry { get; set; }


    }
}