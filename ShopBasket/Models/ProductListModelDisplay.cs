using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ShopBasket.Models
{
    public class ProductListModelDisplay
    {
        public string ProdName { get; set; }
        public string CatName { get; set; }
        public byte[] ProdImg { get; set; }

        public ImageSource ProdImage { get; set; }
        

        public string ProdDescription { get; set; }

        public string Barcode { get; set; }
    }
}
