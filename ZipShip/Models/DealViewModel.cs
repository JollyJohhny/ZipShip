using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZipShip.Models
{
    public class DealViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string SelectedBy { get; set; }

    }
}