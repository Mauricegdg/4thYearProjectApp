using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShopBasket.Models
{
    public class UserListItems
    {
        public string Barcode { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }

        public byte[] ProdImg { get; set; }

        public int Qty { get; set; }

        public string OnSale { get; set; }
        public bool OnSpecial { get; set;}

        public float Current_Price { get; set; }

        public string CurrentPrice { get; set; }


        public ImageSource ProdImage { get; set; }
    }
}
