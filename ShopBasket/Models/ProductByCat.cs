using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShopBasket.Models
{

    public class ProductByCat
    {
        public string ProdName { get; set; }
        
        
        public byte[] ProdImg { get; set; }

        public string ProdDescription { get; set; }

        public string Barcode { get; set; }

        public int StoreID { get; set; }

        public ImageSource ProdImage { get; set; }


    }
}
