using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBasket.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Longitude { get; set; }
        public bool AdminStatus { get; set; }
        public string Latitude { get; set; }
        public string UserName { get; set; }
        public string Surename { get; set; }
        public string Salt { get; set; }
    }
}
