using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBasket.Models
{

    public class ProductByCat
    {
        public string ProdName { get; set; }
        
        
        public byte[] ProdImg { get; set; }

        public string ProdDescription { get; set; }

        public string Barcode { get; set; }
    }
}
