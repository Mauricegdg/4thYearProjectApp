using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBasket.Models
{
    public class BasketStores
    {
        public float Total_Price { get; set; }
        public string TotalPrice { get; set; }

        public string Latitude { get; set; }

        public string longitude { get; set; }

        public string Store_Name { get; set; }

        public string StoreImageUrl { get; set; }

        public int StoreID { get; set; }

        public string Distance { get; set; }
    }
}
