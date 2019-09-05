using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBasket.Models
{
    public class StoreDetailModel
    {
        public string Barcode { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }

        public string CatName { get; set; }

        public float Current_Price { get; set; }

        public string ProdImageUrl { get; set; }

        public string Latitude { get; set; }

        public string longitude { get; set; }

        public string Store_Name { get; set; }

        public string StoreImageUrl { get; set; }

        public int StoreID { get; set; }

        public string Distance { get; set; }
    }
}
