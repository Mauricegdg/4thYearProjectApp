using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShopBasket.Models
{
    public class MasterMenuItems
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Color Background { get; set; }
        public Type TargetType { get; set; }

        public MasterMenuItems(string Title, string IconSource, Color color, Type targetType)
        {
            this.Title = Title;
            this.IconSource = IconSource;
            this.Background = color;
            this.TargetType = targetType;
        }

    }
}
