using ShopBasket.Models;
using ShopBasket.View.DetailViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listview; } }
        public List<MasterMenuItems> Items;

        public MasterPage()
        {

            InitializeComponent();
            SetItems();
        }
        void SetItems()
        {
            Items = new List<MasterMenuItems>();
            Items.Add(new MasterMenuItems("Home", "icon.png", Color.White, typeof(Home)));
            Items.Add(new MasterMenuItems("Basket", "icon.png", Color.White, typeof(Basket)));
            listview.ItemsSource = Items;
        }

    }
}