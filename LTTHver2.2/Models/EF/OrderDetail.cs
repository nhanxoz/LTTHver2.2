using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTTHver2._2.Models.EF
{
    public class OrderDetail
    {
       
        public string Name { get; set; }
        public string Image { get; set; }
        public int? OrginPrice { get; set; }
        public int PromotionPrice { get; set; }
        public string Size { get; set; }
        public string Topping { get; set; }
        public int? BoundingPrice { get; set; }
        public int? Quantity { get; set; }


    }
}