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
            Items = new List<MasterMenuItems>
            {
                new MasterMenuItems("Home", "Sale.png", Color.Transparent, typeof(Home)),
                new MasterMenuItems("Basket", "Basket.png", Color.Transparent, typeof(Basket)),
                new MasterMenuItems("Browse", "Browse.png", Color.Transparent, typeof(Browse)),
                new MasterMenuItems("Search Or Scan", "scan.png", Color.Transparent, typeof(MainSearch)),
                new MasterMenuItems("Settings", "Settings.png", Color.Transparent, typeof(Settings))
            };
            listview.ItemsSource = Items;
        }

        
    }
}